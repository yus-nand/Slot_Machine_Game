using UnityEngine;
using TMPro;

public class GameManger : MonoBehaviour
{
    public GameObject BlockingOverlayPanel;
    public int currentBet;
    public TextMeshProUGUI currentBetText;
    [SerializeField] TextMeshProUGUI messaegeText;
    private ScoreManager scoreManager;
    void Awake()
    {
        scoreManager = FindAnyObjectByType<ScoreManager>();
    }
    private void Start()
    {
        BlockingOverlayPanel.SetActive(false);
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
}
