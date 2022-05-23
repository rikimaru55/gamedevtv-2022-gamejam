using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerRecipeController : MonoBehaviour
{
    public Sprite RedLiquid;
    public Sprite YellowLiquid;
    public Sprite BlueLiquid;
    public Sprite GreenLiquid;

    public Sprite Tumbler;
    public Sprite WineGlass;
    public Sprite PintGlass;

    public SpriteRenderer[] LiquidSprites;
    public SpriteRenderer ContainerSpriteRenderer;

    public void ResetPlayerRecipe()
    {
        foreach(var liquidSprite in LiquidSprites)
        {
            liquidSprite.gameObject.SetActive(false);
        }
    }

    public void setContainer(IngredientType containerType)
    {
        switch (containerType)
        {
            case IngredientType.Tumbler:
                ContainerSpriteRenderer.sprite = Tumbler;
                break;
            case IngredientType.WineGlass:
                ContainerSpriteRenderer.sprite = WineGlass;
                break;
            case IngredientType.PintGlass:
                ContainerSpriteRenderer.sprite = PintGlass;
                break;
            default:
                throw new Exception("Unknown Container Type");
        }
        ContainerSpriteRenderer.gameObject.SetActive(true);
    }

    public void setLiquids(List<IngredientType> liquids)
    {
        int i = 0;
        foreach (var liquid in liquids)
        {
            var liquidSpriteRenderer = LiquidSprites[i];
            liquidSpriteRenderer.gameObject.SetActive(true);
            switch (liquid)
            {
                case IngredientType.Red:
                    liquidSpriteRenderer.sprite = RedLiquid;
                    break;
                case IngredientType.Green:
                    liquidSpriteRenderer.sprite = GreenLiquid;
                    break;
                case IngredientType.Blue:
                    liquidSpriteRenderer.sprite = BlueLiquid;
                    break;
                case IngredientType.Yellow:
                    liquidSpriteRenderer.sprite = YellowLiquid;
                    break;
                default:
                    throw new Exception("Unknown Liquid Type");
            }
            i++;
        }
    }

    public void RecipeUpdate(Recipe recipe)
    {
        ResetPlayerRecipe();
        setContainer(recipe.container);
        setLiquids(recipe.liquids);
    }

    public void ResetRecipeAnimation()
    {
        //Do animations for recipe reset 
    }
}
