using UnityEngine;
using TMPro;

public class ScoreManager : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI scoreHolderText;
    private int score = 1000;
    private void Start()
    {
        scoreHolderText.text = "$" + score;
    }
    public void AddScore(int points)
    {
        score += points;
        UpdateUI();
    }
    public void SubtractScore(int points)
    {
        score -= points;
        UpdateUI();
    }
    void UpdateUI()
    {
        scoreHolderText.text = "$" + score;
    }
}
