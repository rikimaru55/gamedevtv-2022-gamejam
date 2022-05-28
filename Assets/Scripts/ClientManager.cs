using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ClientManager : MonoBehaviour
{

    public RecipeController[] RecipeControllers;
    public ClientTimerController[] ClientTimerControllers;
    public List<TextMeshProUGUI> CountersText;

    private Action<int> onTimerExpired;
    public Action<int> OnTimerExpired
    {
        set { onTimerExpired = value; }
    }

    private Dictionary<int,int> drinksServedPerClient;
    private Dictionary<int, int> drinksNeededPerClient;

    private void Awake()
    {
        drinksServedPerClient = new Dictionary<int, int>();
        drinksNeededPerClient = new Dictionary<int, int>();
    }

    public void SpawnClient(Level level)
    {
        if (countActiveClients() >= ClientTimerControllers.Length)
        {
            return;
        }

        // Try to find available client
        ClientTimerController chosenClient = (ClientTimerController)ClientTimerControllers.GetValue(UnityEngine.Random.RandomRange(0, ClientTimerControllers.Length));
        const int MAX_TRIES = 10;
        int tryCounter = 0;
        while (chosenClient.gameObject.activeSelf && tryCounter < MAX_TRIES)
        {
            chosenClient = (ClientTimerController)ClientTimerControllers.GetValue(UnityEngine.Random.RandomRange(0, ClientTimerControllers.Length));
            tryCounter++;
        }
        if (tryCounter >= MAX_TRIES)
        {
            throw new Exception("Couldn't find appropriate client to spawn.");
        }

        // Found client
        float timerAmount = UnityEngine.Random.RandomRange(level.minTime, level.maxTime);
        RecipeControllers[chosenClient.Index].EnableRecipe();
        RecipeControllers[chosenClient.Index].RandomizeRecipe(UnityEngine.Random.RandomRange(level.minNumberOfIngredients, level.maxNumberOfIngredients));
        chosenClient.ResetTimer(timerAmount, clientTimerExpired);
        drinksServedPerClient[chosenClient.Index] = 0;
        SetCounterValue(chosenClient.Index, drinksServedPerClient[chosenClient.Index]);
        drinksNeededPerClient[chosenClient.Index] = UnityEngine.Random.RandomRange(level.minNumberOfDrinks, level.maxNumberOfDrinks);
        CountersText[chosenClient.Index].gameObject.SetActive(true);
    }

    public bool ServeDrinkToClient(int clientIndex)
    {
        drinksServedPerClient[clientIndex] += 1;
        SetCounterValue(clientIndex, drinksServedPerClient[clientIndex]);
        if (drinksServedPerClient[clientIndex] >= drinksNeededPerClient[clientIndex])
        {
            return true;
        }
        return false;
    }

    public void ServeWrongDrinkToClient(int clientIndex)
    {
        ClientTimerControllers[clientIndex].PlayAngerAnimation();
    }

    private int countActiveClients()
    {
        int count = 0;
        foreach (var clientTimerController in ClientTimerControllers)
        {
            count += clientTimerController.gameObject.activeSelf ? 1 : 0;
        }
        return count;
    }

    private void clientTimerExpired(int index)
    {
        DisableClient(index);
        onTimerExpired(index);
    }

    public void StartTimersForActiveClients(float seconds, Action<int> onTimerEnd)
    {
        foreach (var clientTimerController in ClientTimerControllers)
        {
            if (clientTimerController.gameObject.activeSelf)
            {
                clientTimerController.ResetTimer(seconds, onTimerEnd);
            }
        }
    }

    public void DisableAllClients()
    {
        foreach (var clientTimerController in ClientTimerControllers)
        {
            clientTimerController.DisableTimer();
        }
        foreach (var recipeController in RecipeControllers)
        {
            recipeController.DisableRecipe();
        }
        foreach (var counterText in CountersText)
        {
            counterText.gameObject.SetActive(false);
        }
    }
    public void DisableClient(int index)
    {
        if (RecipeControllers[index] != null)
        {
            RecipeControllers[index].DisableRecipe();
        }
        if (ClientTimerControllers[index] != null)
        {
            ClientTimerControllers[index].DisableTimer();
        }
        if (CountersText[index] != null)
        {
            CountersText[index].gameObject.SetActive(false);
        }
    }

    public void ResetClientRecipeAndTimer(int index, Level level)
    {
        if (RecipeControllers[index] != null)
        {
            RecipeControllers[index].RandomizeRecipe(UnityEngine.Random.RandomRange(level.minNumberOfIngredients, level.maxNumberOfIngredients));
        }
        if (ClientTimerControllers[index] != null)
        {
            float timerAmount = UnityEngine.Random.RandomRange(level.minTime, level.maxTime);
            ClientTimerControllers[index].ResetTimer(timerAmount, clientTimerExpired);
        }
    }

    public void SetCounterValue(int index, int value)
    {
        if (CountersText[index])
        {
            CountersText[index].text = value.ToString();
        }
    }
    public void FadeOutClientAndClientOrder(int index, Action onFadeOutClientAndClientOrder)
    {
        bool clientFaded = false;
        bool clientOrderFaded = false;

        void checkFades()
        {
            if (clientFaded && clientOrderFaded)
            {
                onFadeOutClientAndClientOrder();
            }
        }

        if (RecipeControllers[index] != null)
        {
            RecipeControllers[index].FadeOutRecipe(() =>
            {
                clientOrderFaded = true;
                checkFades();
            });
        }

        if (ClientTimerControllers[index] != null)
        {
            ClientTimerControllers[index].FadeOutClient(() =>
            {
                clientFaded = true;
                checkFades();
            });
        }


    }

    public bool IsClientActive(int clientIndex)
    {
        if (ClientTimerControllers[clientIndex] != null)
        {
            return ClientTimerControllers[clientIndex].IsTimerRunning();
        }
        return false;
    }

    public bool IsRecipeCorrect(int clientIndex, Recipe recipe)
    {
        if (RecipeControllers[clientIndex] != null)
        {
            return RecipeControllers[clientIndex].isRecipeCorrect(recipe);
        }
        return false;
    }
}
