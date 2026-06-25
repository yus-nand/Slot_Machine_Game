using UnityEngine;
using TMPro;

public class ScoreManager : MonoBehaviour
{
    [Header("References")]
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
    public void ModifyScore(float points)
    {
        score += (int)points;
        UpdateUI();
    }
    void UpdateUI()
    {
        scoreHolderText.text = "$" + score;
    }
}
