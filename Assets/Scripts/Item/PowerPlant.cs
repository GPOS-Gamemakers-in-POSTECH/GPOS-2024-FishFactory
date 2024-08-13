using UnityEngine;

// Abstract Class for Power Plants
public class PowerPlant
{
    public bool isSolved; // Check if Power Plant is solved
    public int power; // Power Value of Power Plant
    public int plantName; // Name of Power Plant

    public PowerPlant(bool isSolved, int power, int plantName)
    {
        this.isSolved = isSolved;
        this.power = power;
        this.plantName = plantName;
    }

    public void ShowPowerPlantInfo() { Debug.Log("Fire Power Plant"); } // Show the information of Each Power Plant
    public void GeneratePower(int increasePower) { power += increasePower; } // Increase Power Value
    public void DecreasePower(int decreasePower) { power -= decreasePower; } // Decrease Power Value
}