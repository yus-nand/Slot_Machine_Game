
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class SlotMachineController : MonoBehaviour
{
    public Button lever;
    public GameObject leverOff, leverOn;
    public ReelController[] reels;
    private ScoreManager scoreManager;
    private GameManger gameManager;
    void Awake()
    {
        scoreManager = FindAnyObjectByType<ScoreManager>();
        gameManager = FindAnyObjectByType<GameManger>();
    }
    void Start()
    {
        lever.onClick.AddListener(PlaySlotMachine);
    }
    void PlaySlotMachine()
    {
        if(gameManager.currentBet <= 0)
        {
            gameManager.currentBetText.text = "You need to place a bet first!";
            return;
        }
        if(!gameManager.VerifyBet())
            return;       
        Debug.Log("Slot Machine is playing");
        StartCoroutine(PlayRoutine());
        StartCoroutine(HandleSpinUI());
        Debug.Log("Slot Machine done playing");
    }

    IEnumerator HandleSpinUI()
    {
        lever.interactable = false;
        leverOff.SetActive(false);
        leverOn.SetActive(true);
        gameManager.BlockingOverlayPanel.SetActive(true);
        gameManager.UpdateMessage("Spinning...");
        while (!AreAllReelsStopped())
        {
            yield return null;
        }
        leverOff.SetActive(true);
        leverOn.SetActive(false);
        gameManager.BlockingOverlayPanel.SetActive(false); 
        gameManager.UpdateMessage(CheckWin());
        yield return new WaitForSeconds(0.5f);
        lever.interactable = true;
    }
    string CheckWin()
    {
        string msg;
        int winAmount = gameManager.currentBet;
        float reel0, reel1, reel2;
        reel0 = reels[0].selectedSymbolIndex;
        reel1 = reels[1].selectedSymbolIndex;
        reel2 = reels[2].selectedSymbolIndex;

        if (reel0 == reel1 && reel0 == reel2)        // all reels match
        {
            if (reel0 == 1)      // means all are 7
            {
                winAmount += 1000;
                msg = "HUGE WINNNN!!! +1000 points!!!";
            }
            else if (reel0 == 2)  // cherry
            {
                winAmount += 100;
                msg = "Winner +100 points!";
            }
            else if (reel0 == 3) // bell
            {
                winAmount += 250;
                msg = "Great WIN +250 points!!";
            }
            else if (reel0 == 0) // bar
            {
                winAmount += 500;
                msg = "BIG WIN +500 points!!!";
            }
            else
            {
                return "Something went wrong....";
            }
        }
        else
        {
            winAmount *= -1;
            msg = "No Win T_T";
        }

        scoreManager.ModifyScore(winAmount);
        return msg;

    }
    IEnumerator PlayRoutine()
    {
        reels[0].Spin();
        yield return new WaitForSeconds(0.2f);

        reels[1].Spin();
        yield return new WaitForSeconds(0.2f);

        reels[2].Spin();
    }
    private bool AreAllReelsStopped()
    {
        foreach (var reel in reels)
        {
            if (reel.IsSpinning)
                return false;
        }

        return true;
    }
}
