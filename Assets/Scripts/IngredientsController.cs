using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum IngredientType
{
    Red,
    Green,
    Blue,
    Yellow,
    Tumbler,
    WineGlass,
    PintGlass
}

public class IngredientsController : MonoBehaviour
{
    public Sprite Bottles;
    public Sprite Tumblers;
    public Sprite WineGlasses;
    public Sprite PintGlasses;

    public GameObject[] IngredientPositions;

    private List<IngredientType> ingredients;
    // Start is called before the first frame update
    void Awake()
    {
        ingredients = new List<IngredientType>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void RandomizeIngredients()
    {
        ingredients.Clear();
        foreach (IngredientType i in Enum.GetValues(typeof(IngredientType)))
        {
            ingredients.Add(i);
        }
        Shuffle(ingredients);
        updateUI();
    }

    private void updateUI()
    {
        int i = 0;
        foreach (IngredientType ingredientType in ingredients)
        {
            var spriteRenderer = IngredientPositions[i].GetComponent<SpriteRenderer>();
            Debug.Log(spriteRenderer.gameObject.name);
            Debug.Log(Color.red);
            switch (ingredientType)
            {
                case IngredientType.Red:
                    spriteRenderer.sprite = Bottles;
                    spriteRenderer.color = Constants.RedIngredientColor;
                    break;
                case IngredientType.Green:
                    spriteRenderer.sprite = Bottles;
                    spriteRenderer.color = Constants.GreenIngredientColor;
                    break;
                case IngredientType.Blue:
                    spriteRenderer.sprite = Bottles;
                    spriteRenderer.color = Constants.BlueIngredientColor;
                    break;
                case IngredientType.Yellow:
                    spriteRenderer.sprite = Bottles;
                    spriteRenderer.color = Constants.YellowIngredientColor;
                    break;
                case IngredientType.Tumbler:
                    spriteRenderer.sprite = Tumblers;
                    break;
                case IngredientType.WineGlass:
                    spriteRenderer.sprite = WineGlasses;
                    break;
                case IngredientType.PintGlass:
                    spriteRenderer.sprite = PintGlasses;
                    break;
                default:
                    throw new Exception("Unknown Ingredient Type");
            }
            i++;
        }
    }

    /// <summary>
    /// This code is a literal copy-paste from https://forum.unity.com/threads/clever-way-to-shuffle-a-list-t-in-one-line-of-c-code.241052/
    /// NEEDS TO BE REVIEWED
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="ts"></param>
    private void Shuffle<T>(IList<T> ts)
    {
        var count = ts.Count;
        var last = count - 1;
        for (var i = 0; i < last; ++i)
        {
            var r = UnityEngine.Random.Range(i, count);
            var tmp = ts[i];
            ts[i] = ts[r];
            ts[r] = tmp;
        }
    }
}
