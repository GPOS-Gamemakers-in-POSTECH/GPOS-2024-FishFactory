using UnityEngine;

public class FactoryMachine
{
    public int machineType;
    public string machineName;
    public int machineTime;
    public bool isSolved;

    public FactoryMachine(int machineType, string machineName, int machineTime, bool isSolved)
    {
        this.machineType = machineType;
        this.machineName = machineName;
        this.machineTime = machineTime;
        this.isSolved = isSolved;
    }

    public void ShowFactoryMachineInfo() { Debug.Log("Factory Machine"); } // Show the information of Each Power Plant
}

public class MachineProcess
{
    public FactoryMachine[] machines;

    public MachineProcess(FactoryMachine[] machines)
    {
        this.machines = machines;
    }
}