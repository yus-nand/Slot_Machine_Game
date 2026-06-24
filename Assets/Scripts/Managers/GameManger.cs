using UnityEngine;
using TMPro;

public class GameManger : MonoBehaviour
{
    public int currentBet;
    [SerializeField] private TextMeshProUGUI currentBetText;
    public void SetBet(int bet)
    {
        currentBet = bet;
        currentBetText.text = "Current bet: $" + currentBet;
    }
}
