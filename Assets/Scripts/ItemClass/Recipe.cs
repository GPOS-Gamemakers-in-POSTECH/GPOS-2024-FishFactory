using System;

[Serializable]
public class Recipe
{
    public int recipeID;
    public string recipeName;
    public int[] ingredientID;
    public int[] machineID;
    int dayRequired;
}