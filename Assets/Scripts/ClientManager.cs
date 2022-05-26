using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClientManager : MonoBehaviour
{

    public RecipeController[] RecipeControllers;
    public ClientTimerController[] ClientTimerControllers;

    private Action<int> onTimerExpired;
    public Action<int> OnTimerExpired
    {
        set { onTimerExpired = value; }
    }

    public void SpawnClient(Level level)
    {
        if (countActiveClients() >= ClientTimerControllers.Length)
        {
            return;
        }
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
        float timerAmount = UnityEngine.Random.RandomRange(level.minTime, level.maxTime);
        RecipeControllers[chosenClient.Index].EnableRecipe();
        RecipeControllers[chosenClient.Index].RandomizeRecipe(UnityEngine.Random.RandomRange(level.minNumberOfIngredients, level.maxNumberOfIngredients));
        chosenClient.ResetTimer(timerAmount, clientTimerExpired);
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
        ClientTimerControllers[index].DisableTimer();
        RecipeControllers[index].DisableRecipe();
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
