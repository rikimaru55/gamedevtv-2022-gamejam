﻿using System;
using UnityEngine;

public class Constants
{
    public static readonly Color RedIngredientColor = new Color(1, 0.1843137f, 0);
    public static readonly Color BlueIngredientColor = new Color(0.2901961f, 0.05098039f, 1);
    public static readonly Color GreenIngredientColor = new Color(0.09803922f, 1, 0.6745098f);
    public static readonly Color YellowIngredientColor = new Color(1, 0.9215686f, 0.09803922f);
    public static readonly IngredientType[] Containers = { IngredientType.Tumbler, IngredientType.WineGlass, IngredientType.PintGlass };
    public static readonly IngredientType[] Liquids = { IngredientType.Red, IngredientType.Green, IngredientType.Blue, IngredientType.Yellow };
}