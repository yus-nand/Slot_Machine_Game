
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
        lever.interactable = true;
    }
}
