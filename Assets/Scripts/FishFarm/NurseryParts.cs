using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NurseryParts
{
    public string PartsName { get; set; }
    public int PartID { get; set; }

    public NurseryParts(string partsName, int partID)
    {
        PartsName = partsName;
        PartID = partID;
    }

    public virtual void PartsFunction()
    {
        Debug.Log($"Part Function: {PartsName} is operating.");
    }
}

public class OxygenController : NurseryParts
{
    public OxygenController(int partID) : base("OxygenController", partID)
    {
    }

    public override void PartsFunction()
    {
        base.PartsFunction();
        //용존 산소 조절 기능
    }
}

public class TemperatureController : NurseryParts
{
    public TemperatureController(int partID) : base("TemperatureController", partID)
    {
    }

    public override void PartsFunction()
    {
        base.PartsFunction();
        //수온 조절 기능
    }
}

public class AutoCollector : NurseryParts
{
    public AutoCollector(int partID) : base("AutoCollector", partID)
    {
    }

    public override void PartsFunction()
    {
        base.PartsFunction();
        //자동 수확 기능
    }
}

public class FoodDispenser : NurseryParts
{
    public FoodDispenser(int partID) : base("FoodDispenser", partID)
    {
    }

    public override void PartsFunction()
    {
        base.PartsFunction();
        //사료 분배 기능
    }
}