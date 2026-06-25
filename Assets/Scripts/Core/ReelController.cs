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
    private AudioManager audioManager;
    private AudioSource audioSource;


    private void Awake()
    {
        Application.targetFrameRate = 60;           // doing this helped with image drift but did not
                                                    // completly fix it
    }
    private void Start()
    {
        audioManager = FindAnyObjectByType<AudioManager>();
        audioSource = GetComponent<AudioSource>(); 
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
        audioSource.Play();
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
        NormalizeImagePositions();
        audioSource.Stop();
        audioSource.PlayOneShot(audioManager.clips[3]);
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
            currentSpeed = Mathf.Lerp(currentSpeed, maxSpinSpeed, acceleration * Time.deltaTime);
        }
        else
        {
            currentSpeed = Mathf.Lerp(currentSpeed, 100f, deceleration * Time.deltaTime);
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
    private void NormalizeImagePositions()      // this still doesnt fix the drift but keeps it limited
    {
        if (reelImages.Length == 0)
            return;

        float spacing = 110f;

        // Find the topmost image and its index
        int topImageIndex = 0;
        float topImageY = reelImages[0].localPosition.y;
        
        for (int i = 1; i < reelImages.Length; i++)
        {
            if (reelImages[i].localPosition.y > topImageY)
            {
                topImageY = reelImages[i].localPosition.y;
                topImageIndex = i;
            }
        }

        // setting all images position with even spacing in a cyclic order
        for (int i = 0; i < reelImages.Length; i++)
        {
            int imageIndex = (topImageIndex + i) % reelImages.Length;
            float targetY = topImageY - (i * spacing);
            Vector3 pos = reelImages[imageIndex].localPosition;
            reelImages[imageIndex].localPosition = new Vector3(pos.x, targetY, pos.z);
        }
    }
}