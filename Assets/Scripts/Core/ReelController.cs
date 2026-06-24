using System.Collections;
using UnityEngine;

public class ReelController : MonoBehaviour
{
    [Header("Settings")]
    [SerializeField] private float spinDuration;
    [SerializeField] private float spinSpeed = 100f;
    [SerializeField] private float centerY = 0f;
    [SerializeField] private float stopTolerance = 10f;

    [Header("References")]
    [SerializeField] private Transform[] reelImages;
    [SerializeField] private SymbolType[] symbols;

    [HideInInspector]
    public int selectedSymbolIndex;

    private bool shouldSpin;

    private void Awake()
    {
        Application.targetFrameRate = 60;
    }

    public void Spin()
    {
        if (shouldSpin)
            return;

        selectedSymbolIndex = Random.Range(0, symbols.Length);

        Debug.Log($"Target Symbol : {symbols[selectedSymbolIndex]}");

        StartCoroutine(SpinRoutine());
    }

    private IEnumerator SpinRoutine()
    {
        shouldSpin = true;

        // Minimum spin time
        yield return new WaitForSeconds(spinDuration);

        Transform targetImage = reelImages[selectedSymbolIndex];

        // Continue spinning until selected image reaches center
        while (Mathf.Abs(targetImage.localPosition.y - centerY) > stopTolerance)
        {
            yield return null;
        }

        // Snap perfectly into place
        float offset = centerY - targetImage.localPosition.y;

        foreach (Transform image in reelImages)
        {
            image.localPosition += Vector3.up * offset;
        }

        shouldSpin = false;

        Debug.Log($"Stopped On : {symbols[selectedSymbolIndex]}");
    }

    private void Update()
    {
        if (!shouldSpin)
            return;

        MoveReel();
    }

    private void MoveReel()
    {
        foreach (Transform image in reelImages)
        {
            image.Translate(Vector3.down * spinSpeed , Space.Self);

            if (image.localPosition.y <= -150f)
            {
                image.localPosition = new Vector3(image.localPosition.x, 290f, image.localPosition.z);
            }
        }
    }
}