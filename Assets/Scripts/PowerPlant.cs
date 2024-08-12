using System;
using UnityEngine;

// Abstract Class for Power Plants
abstract class PowerPlant
{
    public bool isSolved; // Check if Power Plant is solved
    public int power; // Power Value of Power Plant

    public abstract void ShowPowerPlantInfo(); // Show the information of Each Power Plant
    public abstract void GeneratePower(); // Add Power Value
}

// Fire Power Plant
class FirePowerPlant : PowerPlant
{
    public bool isSolved = false;
    public int power = 0;

    public override void ShowPowerPlantInfo() { Debug.Log("Fire Power Plant"); }
    public override void GeneratePower() { power += 10; }
}

// wind power plant
class WindPowerPlant : PowerPlant
{
    public bool isSolved = false;
    public int power = 0;

    public override void ShowPowerPlantInfo() { Debug.Log("Wind Power Plant"); }
    public override void GeneratePower() { power += 15; }
}

// Tidal power station
class TidalPowerPlant : PowerPlant
{
    public bool isSolved = false;
    public int power = 0;

    public override void ShowPowerPlantInfo() { Debug.Log("Tidal Power Plant"); }
    public override void GeneratePower() { power += 30; }
}
