using System.Collections;
using UnityEngine;

public class ReelController : MonoBehaviour
{
    [SerializeField] private SymbolType[] symbols;
    public float timer;
    [HideInInspector] public int selectedSymbolIndex; 
    public void Spin()
    {
        selectedSymbolIndex = Random.Range(0, symbols.Length);
        Debug.Log("Started Spin");
        StartCoroutine(ShowSymbol());
    }
    IEnumerator ShowSymbol()
    {
        yield return new WaitForSeconds(timer);
        Debug.Log("Symbol: " + symbols[selectedSymbolIndex]);
    }
}
