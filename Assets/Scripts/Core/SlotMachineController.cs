using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class SlotMachineController : MonoBehaviour
{
    [Header("References")]
    public Button lever;
    public GameObject leverOff, leverOn;
    public ReelController[] reels;
    private ScoreManager scoreManager;
    private GameManger gameManager;
    private AudioManager audioManager;
    void Awake()
    {
        scoreManager = FindAnyObjectByType<ScoreManager>();
        gameManager = FindAnyObjectByType<GameManger>();
        audioManager = FindAnyObjectByType<AudioManager>();
    }
    void Start()
    {
        lever.onClick.AddListener(PlaySlotMachine);
    }
    void PlaySlotMachine()
    {
        audioManager.PlayLeverSound(audioManager.clips[5]);
        if(gameManager.currentBet <= 0)
        {
            gameManager.currentBetText.text = "You need to place a bet first!";
            return;
        }
        if(!gameManager.VerifyBet())
            return;       
        Debug.Log("Slot Machine is playing");
        StartCoroutine(PlayRoutine());
        // audioManager.PlayClip(audioManager.clips[2]);
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
        // audioManager.StopPlayingCLip();
        audioManager.PlayLeverSound(audioManager.clips[4]);
        yield return new WaitForSeconds(0.5f);
        lever.interactable = true;
    }
    string CheckWin()
    {
        string msg;
        bool didWin = false;
        float winAmount = gameManager.currentBet;
        float reel0, reel1, reel2;
        reel0 = reels[0].selectedSymbolIndex;
        reel1 = reels[1].selectedSymbolIndex;
        reel2 = reels[2].selectedSymbolIndex;

        if (reel0 == reel1 && reel0 == reel2)        // all reels match
        {
            if (reel0 == 1)      // means all are 7
            {
                winAmount *= 10;
                didWin = true;
                msg = "HUGE WINNNN!!! GRAND PRIZE!!";
            }
            else if (reel0 == 2)  // cherry
            {
                winAmount *= 2;
                didWin = true;
                msg = "Winner! 2x points!!";
            }
            else if (reel0 == 3) // bell
            {
                winAmount *= 3.5f;
                didWin = true;
                msg = "Great WIN!!! 3.5x points!!";
            }
            else if (reel0 == 0) // bar
            {
                winAmount *= 5;
                didWin = true;
                msg = "BIG WIN!!! 5x points!!";
            }
            else
            {
                return "Something went wrong....";
            }
        }
        else
        {
            didWin = false;
            winAmount *= -1;
            msg = "No Win T_T";
        }
        if(didWin)
        {
            audioManager.PlayClipOneShot(audioManager.clips[0]);
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
