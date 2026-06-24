using System.Collections;
using UnityEngine;

public class ReelController : MonoBehaviour
{
    public float timer;
    public Transform[] reelImages;
    [HideInInspector] public int selectedSymbolIndex; 
    [SerializeField] private SymbolType[] symbols;
    private Vector3 stopPosition = Vector3.zero;
    private bool shouldSpin = false;
    private void Awake()
    {
        Application.targetFrameRate = 60;
    }
    public void Spin()
    {
        selectedSymbolIndex = Random.Range(0, symbols.Length);
        Debug.Log("Started Spin");
        StartCoroutine(ShowSymbol());
    }
    IEnumerator ShowSymbol()
    {
        shouldSpin = true;
        yield return new WaitForSeconds(timer);
        shouldSpin = false;
        Debug.Log("Symbol: " + symbols[selectedSymbolIndex]);
    }
    void Update()
    {
        if(shouldSpin)
            StartAnimation();
    }
    void StartAnimation()
    {
        foreach(var image in  reelImages)
        {
            image.Translate(20f * Vector3.down);
            if(image.localPosition.y <= -150f)
                image.localPosition = Vector3.up * 290;
        }
    }
}
