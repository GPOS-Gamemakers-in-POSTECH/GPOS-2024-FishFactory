using UnityEngine;
using UnityEngine.UI;

public class FishTankUIController : ActionPoints
{
    // UI that shows fish information UI
    public GameObject fishInfoUI;
    
    // button to insert or get fishes
    public Button fishButton;

    // button to feed the fishes
    public Button feedButton;

    // Start is called before the first frame update
    void Start()
    {
        fishButton.onClick.AddListener(insertFish);
        feedButton.onClick.AddListener(feedFish);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    protected void activeFishTankUi(int tankNumber)
    {
        fishInfoUI.SetActive(true);
    }

    protected void deactivateFishTankUi()
    {
        fishInfoUI.SetActive(false);
    }

    void insertFish()
    {

        Debug.Log("inserting fish");
    }

    void feedFish()
    {
        Debug.Log("feeding fish");
    }
}
