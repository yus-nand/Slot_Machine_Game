using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class GameManger : MonoBehaviour
{
    [Header("References")]
    public GameObject BlockingOverlayPanel;
    public TextMeshProUGUI currentBetText;
    [SerializeField] TextMeshProUGUI messaegeText;
    [SerializeField] private Button retryButton;
    
    [Header("Game State")]
    public int currentBet;
    private ScoreManager scoreManager;
    void Awake()
    {
        scoreManager = FindAnyObjectByType<ScoreManager>();
    }
    private void Start()
    {
        BlockingOverlayPanel.SetActive(false);
        retryButton.onClick.AddListener(RetryGame);
    }
    void Update()
    {
        if(scoreManager.Score < 100)
        {
            retryButton.gameObject.SetActive(true);
        }
    }
    public void SetBet(int bet)
    {
        currentBet = bet;
        currentBetText.text = "Current bet: $" + currentBet;
    }
    public bool VerifyBet()
    {
        if(currentBet > scoreManager.Score)
        {
            currentBetText.text = "You can't bet what you dont have..";
            return false;
        }
        else if(currentBet <= 0)
        {
            currentBetText.text = "You need to place a bet first!";
            return false;
        }
        else
        {
            return true;
        }
    }
    public void UpdateMessage(string message)
    {
        messaegeText.text = message;
    }
    private void RetryGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
