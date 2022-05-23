using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClientOrderManager : MonoBehaviour
{
    public RecipeController[] RecipeControllers;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void DisableAllRecipes()
    {
        foreach(var recipeController in RecipeControllers)
        {
            recipeController.DisableRecipe();
        }
    }
    public void DisableRecipe(int index)
    {
        if(RecipeControllers[index] != null)
        {
            RecipeControllers[index].DisableRecipe();
        }
    }

    public void EnableAllRecipes()
    {
        foreach (var recipeController in RecipeControllers)
        {
            recipeController.EnableRecipe();
        }
    }

    public void EnableRecipe(int index)
    {
        if (RecipeControllers[index] != null)
        {
            RecipeControllers[index].EnableRecipe();
        }
    }

    public void RandomizeEnabledRecipes(int maxIngredients = 4)
    {
        foreach (var recipeController in RecipeControllers)
        {
            if (recipeController.isEnabled())
            {
                recipeController.RandomizeRecipe(UnityEngine.Random.RandomRange(1, 4));
            }
        }
    }
}
