using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using UnityEngine.UI;
using System.Collections.Generic;

public class UIController : MonoBehaviour
{
    public static UIController Instance;

    // UI texts
    public TextMeshProUGUI ActionPointsText;
    public Image seasonCoverImage;
    public TextMeshProUGUI DateText;

    // mask for hide a AP bar
    public Image APmask;

    int maxActionPoint = 100;
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
    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else { Destroy(gameObject); }
    }

    void Start()
    {
        UpdateActionPointsUI();
        UpdateDateUI();
        goToBedButton.onClick.AddListener(goToBed);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.P))
            ReduceActionPoints(20); 

        Vector2 mousePosition = Input.mousePosition;

        if (RectTransformUtility.RectangleContainsScreenPoint(seasonTransform, mousePosition, null))
        {
            seasonCoverImage.gameObject.SetActive(true);
            DateText.gameObject.SetActive(true);
        }
        else
        {
            seasonCoverImage.gameObject.SetActive(false);
            DateText.gameObject.SetActive(false);
        }
            
        if (GameManager.Instance.actionPoint <= 20)
            goToBedButton.gameObject.SetActive(true);
        else
            goToBedButton.gameObject.SetActive(false);
    }

    // function to update AP
    public bool ReduceActionPoints(float amount)
    {
        int intAmount = Mathf.RoundToInt(amount);
        // if require more than remaining AP, action is not happens

        if (GameManager.Instance.actionPoint < intAmount)
        {
            Debug.Log("Running out of Action Points!");
            return false;
        }

        // if AP is enough, reduce AP and update UI
        else
        {
            GameManager.Instance.actionPoint -= intAmount;
            UpdateActionPointsUI();
            return true;
        }
    }

    // function to update AP UI
    void UpdateActionPointsUI()
    {
        // express AP as "amount / max amount"
        if (ActionPointsText != null)
        {
            ActionPointsText.text = GameManager.Instance.actionPoint.ToString() + " / " + maxActionPoint.ToString();
            APmask.fillAmount = (float)GameManager.Instance.actionPoint / maxActionPoint;
        }
    }

    // function to update Date UI
    void UpdateDateUI()
    {
        // express Date as "season - day"
        if (DateText != null)
        {
            switch (GameManager.Instance.totalDate / seasonDate)
            {
                case 0: 
                    DateText.text = "SP-" + ((GameManager.Instance.totalDate % seasonDate) + 1).ToString();
                    seasonImage.sprite = springImage;
                    break;
                case 1: 
                    DateText.text = "SM-" + ((GameManager.Instance.totalDate % seasonDate) + 1).ToString();
                    seasonImage.sprite = summerImage;
                    break;
                case 2: 
                    DateText.text = "FL-" + ((GameManager.Instance.totalDate % seasonDate) + 1).ToString();
                    seasonImage.sprite = fallImage;
                    break;
                case 3:
                    DateText.text = "WT-" + ((GameManager.Instance.totalDate % seasonDate) + 1).ToString();
                    seasonImage.sprite = winterImage;
                    break;
                default: DateText.text = "Game End!"; break;
            }
        }
    }

    void goToBed()
    {
        if(SceneManager.GetActiveScene().name != "MinMul")
            SceneManager.LoadScene("MinMul");
    }

}
