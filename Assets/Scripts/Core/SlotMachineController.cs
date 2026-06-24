
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class SlotMachineController : MonoBehaviour
{
    private static WaitForSeconds _waitForSeconds = new WaitForSeconds(2f);
    public Button lever;
    public GameObject leverOff, leverOn;
    public ReelController[] reels;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        lever.onClick.AddListener(PlaySlotMachine);
    }
    void PlaySlotMachine()
    {
        Debug.Log("Slot Machine is playing");
        foreach(var reel in reels)
        {
            reel.Spin();
        }
        StartCoroutine(PlayRoutine());
        StartCoroutine(SwapSprites());
        Debug.Log("Slot Machine done playing");
    }

    IEnumerator SwapSprites()
    {
        lever.interactable = false;
        leverOff.SetActive(false);
        leverOn.SetActive(true);
        yield return _waitForSeconds;
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
            if(reel0 == 0)      // means all are 7
                return "HUGE WINNNN!!! +1000 points!!!";
            else if(reel0 == 1)  // cherry
                return "Winner +100 points!";
            else if(reel0 == 2) // bell
                return "Great WIN +250 points!!";
            else if(reel0 == 3) // bar
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
}
