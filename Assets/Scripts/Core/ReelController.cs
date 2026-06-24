using System.Collections;
using UnityEngine;

public class ReelController : MonoBehaviour
{
    [Header("Settings")]
    [SerializeField] private float spinDuration;
    [SerializeField] private float maxSpinSpeed = 800f;
    [SerializeField] private float acceleration = 10f;
    [SerializeField] private float deceleration = 5f;

    private float currentSpeed;
    private bool isStopping;
    [SerializeField] private float centerY = 0f;
    [SerializeField] private float stopTolerance = 10f;

    [Header("References")]
    [SerializeField] private Transform[] reelImages;
    [SerializeField] private SymbolType[] symbols;

    [HideInInspector]
    public int selectedSymbolIndex;
    public bool IsSpinning => shouldSpin;
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
        isStopping = false;

        currentSpeed = 0f;

        yield return new WaitForSeconds(spinDuration);

        isStopping = true;

        Transform targetImage = reelImages[selectedSymbolIndex];

        while (Mathf.Abs(targetImage.localPosition.y - centerY) > stopTolerance)
        {
            yield return null;
        }

        float offset = centerY - targetImage.localPosition.y;

        foreach (Transform image in reelImages)
        {
            image.localPosition += Vector3.up * offset;
        }

        currentSpeed = 0f;
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
        if (!isStopping)
        {
            currentSpeed = Mathf.Lerp(
                currentSpeed,
                maxSpinSpeed,
                acceleration * Time.deltaTime);
        }
        else
        {
            currentSpeed = Mathf.Lerp(
                currentSpeed,
                100f,
                deceleration * Time.deltaTime);
        }

        foreach (Transform image in reelImages)
        {
            image.Translate(
                Vector3.down * currentSpeed * Time.deltaTime,
                Space.Self);

            if (image.localPosition.y <= -150f)
            {
                image.localPosition =
                    new Vector3(
                        image.localPosition.x,
                        290f,
                        image.localPosition.z);
            }
        }
    }
}