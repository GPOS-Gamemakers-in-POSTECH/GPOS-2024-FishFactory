using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using UnityEngine.UI;

public class UIController : MonoBehaviour
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
    public Button goToBedButton;

    // update UI when starting the scene
    void Start()
    {
        actionPoint = GameManager.Instance.actionPoint;
        totalDate = GameManager.Instance.totalDate;

        UpdateActionPointsUI();
        UpdateDateUI();
        //inventoryButton.onClick.AddListener(openInventory);
        goToBedButton.onClick.AddListener(goToBed);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.P))
            ReduceActionPoints(20); 

        Vector2 mousePosition = Input.mousePosition;

        if (RectTransformUtility.RectangleContainsScreenPoint(seasonTransform, mousePosition, null))
            DateText.gameObject.SetActive(true);

        else
            DateText.gameObject.SetActive(false);

        if (actionPoint <= 20)
            goToBedButton.gameObject.SetActive(true);
        else
            goToBedButton.gameObject.SetActive(false);
    }

    // function to update AP
    public bool ReduceActionPoints(float amount)
    {
        int intAmount = Mathf.RoundToInt(amount);
        // if require more than remaining AP, action is not happens

        if (actionPoint < intAmount)
        {
            Debug.Log("Running out of Action Points!");
            return false;
        }

        // if AP is enough, reduce AP and update UI
        else
        {

            actionPoint -= intAmount;
            UpdateActionPointsUI();

            /* if AP becomes zero, move to scene where bed is located
            if (actionPoints == 0)

            {
                bedButton.SetActive(true);
            }*/

            return true;

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


    /*void openinventory()
    {
        inventory.SetActivate(true);
    }*/

    void goToBed()
    {
        SceneManager.LoadScene("MinMul");
    }

}
