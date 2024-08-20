using UnityEngine;
using UnityEngine.UI;

public class FishTankUIController : ActionPoints
{
    // UI that shows fish information UI
    public GameObject fishInfoUI;

    protected void activeFishTankUi(int tankNumber)
    {
        fishInfoUI.SetActive(true);
    }

    protected void deactivateFishTankUi()
    {
        fishInfoUI.SetActive(false);
    }
}
