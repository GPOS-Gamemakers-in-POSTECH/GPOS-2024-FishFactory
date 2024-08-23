using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using UnityEngine.UI;

public class controlUiElements : MonoBehaviour
{
    // UI texts
    public TextMeshProUGUI ActionPointsText;
    public TextMeshProUGUI DateText;

    // mask for hide a AP bar
    public Image APmask;
    public int actionPoint;
    int maxActionPoint = 100;

    public int totalDate;
    int seasonDate = 1;

    // sprites for various season
    public Image seasonImage;
    public Sprite springImage;
    public Sprite summerImage;
    public Sprite fallImage;
    public Sprite winterImage;

    public RectTransform APmaskTransform;
    public RectTransform seasonTransform;
    
    // inventory
    public GameObject inventory;

    public Button inventoryButton;

    // update UI when starting the scene
    void Start()
    {
        actionPoint = GameManager.Instance.actionPoint;
        totalDate = GameManager.Instance.totalDate;

        UpdateActionPointsUI();
        UpdateDateUI();
        //inventoryButton.onClick.AddListener(openInventory);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q))
            ReduceActionPoints(10);

        if (Input.GetKeyDown(KeyCode.T))
            ReduceActionPoints(20);

        Vector2 mousePosition = Input.mousePosition;

        if (RectTransformUtility.RectangleContainsScreenPoint(seasonTransform, mousePosition, null))
            DateText.gameObject.SetActive(true);

        else
            DateText.gameObject.SetActive(false);
    }

    // function to update AP
    void ReduceActionPoints(int amount)
    {
        // if require more than remaining AP, action is not happens
        if (actionPoint < amount)
            Debug.Log("Running out of Action Points!");

        // if AP is enough, reduce AP and update UI
        else
        {
            actionPoint -= amount;
            UpdateActionPointsUI();

            // if AP becomes zero, move to scene where bed is located
            if (actionPoint == 0)
            {
                SceneManager.LoadScene("MinMul");
            }
        }
    }

    // function to update AP UI
    void UpdateActionPointsUI()
    {
        // express AP as "amount / max amount"
        if (ActionPointsText != null)
        {
            ActionPointsText.text = actionPoint.ToString() + " / " + maxActionPoint.ToString();
            APmask.fillAmount = (float)actionPoint / maxActionPoint;
        }
    }

    // function to update Date UI
    void UpdateDateUI()
    {
        // express Date as "season - day"
        if (DateText != null)
        {
            switch (totalDate / seasonDate)
            {
                case 0: 
                    DateText.text = "SP-" + ((totalDate % seasonDate) + 1).ToString();
                    seasonImage.sprite = springImage;
                    break;
                case 1: 
                    DateText.text = "SM-" + ((totalDate % seasonDate) + 1).ToString();
                    seasonImage.sprite = summerImage;
                    break;
                case 2: 
                    DateText.text = "FL-" + ((totalDate % seasonDate) + 1).ToString();
                    seasonImage.sprite = fallImage;
                    break;
                case 3:
                    DateText.text = "WT-" + ((totalDate % seasonDate) + 1).ToString();
                    seasonImage.sprite = winterImage;
                    break;
                default: DateText.text = "Game End!"; break;
            }
        }
    }
}
