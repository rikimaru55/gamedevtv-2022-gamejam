using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainGameController : MonoBehaviour
{
    public IngredientsController IngredientsController;
    public PlayerController PlayerController;
    public ClientOrderManager ClientOrderManager;
    public PlayerRecipeController PlayerRecipeController;
    public ClientController ClientController;
    public SpriteRenderer[] HeartSpriteRenderers;
    public Sprite FullHeart;
    public Sprite EmptyHeart;
    private Recipe currentRecipe;
    private int currentHealth;

    private void Awake()
    {

    }
    // Start is called before the first frame update
    void Start()
    {
        IngredientsController.RandomizeIngredients();
        ClientOrderManager.EnableAllRecipes();
        ClientOrderManager.RandomizeEnabledRecipes();
        resetPlayerState();
        ClientController.StartTimersForActiveClients(30, (expiredClientIndex) =>
        {
            decreaseHealth();
        });
    }

    private void resetPlayerState()
    {
        currentRecipe.liquids = new List<IngredientType>();
        currentRecipe.container = IngredientType.PintGlass;
        currentHealth = Constants.PlayerMaxHealth;
        updateHealthIndicator();
        PlayerRecipeController.ResetPlayerRecipe();
        PlayerRecipeController.RecipeUpdate(currentRecipe);
    }

    private void updateHealthIndicator()
    {
        foreach (var heartSpriteRenderer in HeartSpriteRenderers)
        {
            heartSpriteRenderer.sprite = EmptyHeart;
        }
        for (int i = 0; i < currentHealth; i++)
        {
            HeartSpriteRenderers[i].sprite = FullHeart;
        }
    }

    private void decreaseHealth(int healthLoss = 1)
    {
        currentHealth -= healthLoss;
        updateHealthIndicator();
        if (currentHealth <= 0)
        {
            //Trigger Game Over
            Debug.Log("Game Over");
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            IngredientsController.RandomizeIngredients();
            ClientOrderManager.EnableAllRecipes();
            ClientOrderManager.RandomizeEnabledRecipes();
        }

        if (Input.GetKeyDown(KeyCode.W))
        {
            var chosenIngredient = IngredientsController.GetIngredientTypeByIndex(PlayerController.PlayerPositionIndex);
            if (Constants.isContainer(chosenIngredient))
            {
                currentRecipe.container = chosenIngredient;
            }
            else if (Constants.isLiquid(chosenIngredient))
            {
                if (currentRecipe.liquids.Count >= Constants.MaxLiquids)
                {
                    // Recipe is full play error sound and shake the UI?
                }
                else
                {
                    currentRecipe.liquids.Add(chosenIngredient);
                }
            }
            PlayerRecipeController.RecipeUpdate(currentRecipe);
        }

        if (Input.GetKeyDown(KeyCode.S))
        {
            //TODO: Add case for when there is no client.
            if (ClientOrderManager.IsRecipeCorrect(PlayerController.PlayerPositionIndex, currentRecipe))
            {
                Debug.Log("Correct Recipe");
            }
            else
            {
                decreaseHealth();
            }
        }

        if (Input.GetKeyDown(KeyCode.E))
        {
            currentRecipe.container = IngredientType.PintGlass;
            currentRecipe.liquids.Clear();
            PlayerRecipeController.RecipeUpdate(currentRecipe);
        }

#if DEVELOPMENT_BUILD 
        if (Input.GetKeyDown(KeyCode.R))
        {
            PlayerStatusController.ResetPlayerRecipe();
        }
#endif
    }
}
