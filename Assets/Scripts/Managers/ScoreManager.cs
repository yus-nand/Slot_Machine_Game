using UnityEngine;
using TMPro;

public class ScoreManager : MonoBehaviour
{
    public TextMeshProUGUI scoreHolderText;
    private int score = 1000;
    public int Score
    {
        get
        {
            return score;
        }
    }
    private void Start()
    {
        scoreHolderText.text = "$" + score;
    }
    public void ModifyScore(int points)
    {
        score += points;
        UpdateUI();
    }
    void UpdateUI()
    {
        scoreHolderText.text = "$" + score;
    }
}
