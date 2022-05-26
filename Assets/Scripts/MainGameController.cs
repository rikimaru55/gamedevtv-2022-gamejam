using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using DG.Tweening;

[System.Serializable]
public struct Level
{
    public float timeBetweenSpawningClients;
    public int minNumberOfIngredients;
    public int maxNumberOfIngredients;
    public float minTime;
    public float maxTime;
    public int successesRequiredForNextLevel;
}
public class MainGameController : MonoBehaviour
{
    public IngredientsController IngredientsController;
    public PlayerController PlayerController;
    public ClientManager ClientManager;
    public PlayerRecipeController PlayerRecipeController;
    public SpriteRenderer[] HeartSpriteRenderers;
    public GameObject HeartsContainer;
    public Sprite FullHeart;
    public Sprite EmptyHeart;
    public List<Level> Levels;
    private Recipe currentRecipe;
    private int currentHealth;
    private int servedDrinks;
    private int currentLevelIndex = 0;
    private Level currentLevel;
    private float clientSpawnerTimer;
    private bool clientSpawnerRunning;
    private bool isAnimationRunning = false;
    private void Awake()
    {
        ClientManager.DisableAllClients();
    }
    // Start is called before the first frame update
    void Start()
    {
        IngredientsController.RandomizeIngredients();
        ClientManager.OnTimerExpired = onClientTimerExpired;
        resetPlayerState();
        loadLevel();
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

    private void loadLevel(int index = 0)
    {
        currentLevelIndex = index;
        if (currentLevelIndex >= Levels.Count)
        {
            SceneManager.LoadScene(Constants.END_SCENE_NAME);
            return;
        }
        currentLevel = Levels[currentLevelIndex];
        ClientManager.SpawnClient(currentLevel);
        startClientSpawnerTimer();
        servedDrinks = 0;
    }

    private void onClientTimerExpired(int clientIndex)
    {
        decreaseHealth();
    }

    private void startClientSpawnerTimer()
    {
        clientSpawnerTimer = 0.0f;
        clientSpawnerRunning = true;
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
        checkHealth();
    }

    private void increaseDrinksServed(int drinksEarned = 1)
    {
        servedDrinks += drinksEarned;
        checkServedDrinks();
    }

    private void checkServedDrinks()
    {
        if (servedDrinks >= currentLevel.successesRequiredForNextLevel)
        {
            loadLevel(currentLevelIndex + 1);
        }
    }

    private void checkHealth()
    {
        if (currentHealth <= 0)
        {
            //Trigger Game Over
            SceneManager.LoadScene(Constants.GAME_OVER_SCENE_NAME);
        }
    }

    private void checkAndUpdateSpawnTimer()
    {
        if (clientSpawnerRunning)
        {
            clientSpawnerTimer += Time.deltaTime;
            if (clientSpawnerTimer > currentLevel.timeBetweenSpawningClients)
            {
                ClientManager.SpawnClient(currentLevel);
                clientSpawnerTimer = 0.0f;
            }
        }
    }

    private void resetRecipe()
    {
        currentRecipe.container = IngredientType.PintGlass;
        currentRecipe.liquids.Clear();
        PlayerRecipeController.RecipeUpdate(currentRecipe);
    }

    // Update is called once per frame
    void Update()
    {
        checkAndUpdateSpawnTimer();

        if (Helpers.checkKeyDownWithPause(KeyCode.W) && !isAnimationRunning)
        {
            isAnimationRunning = true;
            PlayerController.PlayPlayerTakingIngredientAnimation(() =>
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
                isAnimationRunning = false;
            });

        }

        if (Helpers.checkKeyDownWithPause(KeyCode.S) && !isAnimationRunning)
        {
            if (ClientManager.IsClientActive(PlayerController.PlayerPositionIndex))
            {
                if (ClientManager.IsRecipeCorrect(PlayerController.PlayerPositionIndex, currentRecipe))
                {
                    isAnimationRunning = true;
                    PlayerController.PlayPlayerServingADrinkAnimation(currentRecipe.container, () =>
                    {
                        ClientManager.FadeOutClientAndClientOrder(PlayerController.PlayerPositionIndex, () => {
                            isAnimationRunning = false;
                            increaseDrinksServed();
                            ClientManager.DisableClient(PlayerController.PlayerPositionIndex);
                            resetRecipe();
                        });
                    });
                }
                else
                {
                    isAnimationRunning = true;
                    HeartsContainer.transform.DOShakePosition(0.5f, 0.5f, 5, 45, false, true).OnComplete(() =>
                    {
                        isAnimationRunning = false;
                        decreaseHealth();
                    });
                }
            }
            else
            {
                // Play error sound and shake the player or something.
            }
        }


        if (Helpers.checkKeyDownWithPause(KeyCode.E) && !isAnimationRunning)
        {
            isAnimationRunning = true;
            PlayerRecipeController.ResetRecipeAnimation(() =>
            {
                isAnimationRunning = false;
                resetRecipe();
            });
        }
        checkHealth();

#if DEVELOPMENT_BUILD 
        if (Helpers.checkInputWithPause(KeyCode.R))
        {
            IngredientsController.RandomizeIngredients();
            ClientManager.RandomizeEnabledRecipes();
        }
#endif
    }
}
