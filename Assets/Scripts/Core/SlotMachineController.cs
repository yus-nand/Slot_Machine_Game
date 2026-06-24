
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class SlotMachineController : MonoBehaviour
{
    public Button lever;
    public GameObject leverOff, leverOn;
    public ReelController[] reels;
    void Start()
    {
        lever.onClick.AddListener(PlaySlotMachine);
    }
    void PlaySlotMachine()
    {
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
        while (!AreAllReelsStopped())
        {
            yield return null;
        }
        leverOff.SetActive(true);
        leverOn.SetActive(false);
        string msg = CheckWin();
        yield return new WaitForSeconds(0.5f);
        Debug.Log(msg);
        lever.interactable = true;
    }
    string CheckWin()
    {
        float reel0, reel1, reel2;
        reel0 = reels[0].selectedSymbolIndex;
        reel1 = reels[1].selectedSymbolIndex;
        reel2 = reels[2].selectedSymbolIndex;

        if(reel0 == reel1 && reel0 == reel2)
        {
            if(reel0 == 1)      // means all are 7
                return "HUGE WINNNN!!! +1000 points!!!";
            else if(reel0 == 2)  // cherry
                return "Winner +100 points!";
            else if(reel0 == 3) // bell
                return "Great WIN +250 points!!";
            else if(reel0 == 0) // bar
                return "BIG WIN +500 points!!!";
            else
                return "Something went wrong....";
        }
        else
            return "No Win T_T";
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
