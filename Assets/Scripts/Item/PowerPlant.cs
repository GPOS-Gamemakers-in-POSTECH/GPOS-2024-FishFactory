using UnityEngine;

// Abstract Class for Power Plants
public class PowerPlant
{
    public int plantType; // Type of Power Plant
    public string plantName; // Name of Power Plant
    public bool isSolved; // Check if Power Plant is solved
    public int power; // Power Value of Power Plant

    public PowerPlant(int plantType, string plantName, bool isSolved, int power)
    {
        this.plantType = plantType;
        this.plantName = plantName;
        this.isSolved = isSolved;
        this.power = power;
    }

    public void ShowPowerPlantInfo() { Debug.Log("Power Plant"); } // Show the information of Each Power Plant
    public void GeneratePower(int increasePower) { power += increasePower; } // Increase Power Value
    public void DecreasePower(int decreasePower) { power -= decreasePower; } // Decrease Power Value
}