using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public struct Recipe
{
    public List<IngredientType> liquids;
    public IngredientType container;
}

public class RecipeController : MonoBehaviour
{
    public SpriteRenderer[] LiquidSprites;
    public SpriteRenderer ContainerSprite;
    public Sprite Tumbler;
    public Sprite WineGlass;
    public Sprite PintGlass;
    public Sprite RedLiquid;
    public Sprite YellowLiquid;
    public Sprite BlueLiquid;
    public Sprite GreenLiquid;
    private Recipe setRecipe;
    public Recipe SetRecipe
    {
        get
        {
            return setRecipe;
        }
    }

    private void Awake()
    {
        setRecipe.liquids = new List<IngredientType>();
        setRecipe.container = IngredientType.PintGlass;
    }

    public void RandomizeRecipe(int ingredientCount)
    {
        setRecipe.liquids.Clear();
        setRecipe.container = (IngredientType)Constants.Containers.GetValue(UnityEngine.Random.RandomRange(0, Constants.Containers.Length));
        for (int i = 0; i < ingredientCount; i++)
        {
            IngredientType chosenLiquid = (IngredientType)Constants.Liquids.GetValue(UnityEngine.Random.RandomRange(0, Constants.Liquids.Length));
            setRecipe.liquids.Add(chosenLiquid);
        }
        updateGraphics();
    }

    public void DisableRecipe()
    {
        this.gameObject.SetActive(false);
    }

    public void EnableRecipe()
    {
        this.gameObject.SetActive(true);
    }

    public bool isEnabled()
    {
        return this.gameObject.activeSelf;
    }

    private void updateGraphics()
    {
        resetGraphics();
        var liquids = setRecipe.liquids;
        var container = setRecipe.container;
        switch (container)
        {
            case IngredientType.Tumbler:
                ContainerSprite.sprite = Tumbler;
                break;
            case IngredientType.WineGlass:
                ContainerSprite.sprite = WineGlass;
                break;
            case IngredientType.PintGlass:
                ContainerSprite.sprite = PintGlass;
                break;
            default:
                throw new Exception("Unknown Container Type");
        }

        int i = 0;
        foreach (IngredientType liquid in liquids)
        {
            LiquidSprites[i].gameObject.SetActive(true);
            switch (liquid)
            {
                case IngredientType.Red:
                    LiquidSprites[i].sprite = RedLiquid;
                    break;
                case IngredientType.Green:
                    LiquidSprites[i].sprite = GreenLiquid;
                    break;
                case IngredientType.Blue:
                    LiquidSprites[i].sprite = BlueLiquid;
                    break;
                case IngredientType.Yellow:
                    LiquidSprites[i].sprite = YellowLiquid;
                    break;
                default:
                    throw new Exception("Unknown Liquid Type");
            }
            i++;
        }
    }

    private void resetGraphics()
    {
        foreach (SpriteRenderer spriteRenderer in LiquidSprites)
        {
            spriteRenderer.gameObject.SetActive(false);
        }
    }
}
