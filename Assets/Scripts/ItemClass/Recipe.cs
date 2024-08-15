using System;

[Serializable]
public class ItemHandler
{
    public int ID;
    public int num;
}
[Serializable]
public class Recipe
{
    public int recipeID;
    public string recipeName;
    public ItemHandler[] ingredient;
    public int[] machineID;
    int dayRequired;
    public ItemHandler result;
}