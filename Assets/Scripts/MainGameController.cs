using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainGameController : MonoBehaviour
{
    public IngredientsController IngredientsController;
    public PlayerController PlayerController;
    public ClientOrderManager ClientOrderManager;
    // Start is called before the first frame update
    void Start()
    {
        IngredientsController.RandomizeIngredients();
        ClientOrderManager.EnableAllRecipes();
        ClientOrderManager.RandomizeEnabledRecipes();
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
    }
}
