using System;

[Serializable]
public class Recipe
{
    public int itemID;
    public string recipeName;
    public int sellPrice;
}

[Serializable]
public class RecipeData
{
    public Recipe[] recipes;
}
