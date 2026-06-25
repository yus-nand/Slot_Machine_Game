using System.Collections;
using UnityEngine;

public class ReelController : MonoBehaviour
{
    [Header("Settings")]
    [SerializeField] private float spinDuration = 2f;
    [SerializeField] private float maxSpinSpeed = 800f;
    [SerializeField] private float acceleration = 10f;
    [SerializeField] private float deceleration = 5f;

    [Header("Layout")]
    // The Y below which an image wraps back to the top
    [SerializeField] private float wrapBelowY = -150f;
    // The Y an image is teleported to when it wraps
    // Should be: wrapAboveY = wrapBelowY + (spacing * imageCount)
    // With 4 images at 110 spacing: -150 + 440 = 290
    [SerializeField] private float wrapAboveY = 290f;
    [SerializeField] private float spacing = 110f;
    [SerializeField] private float centerY = 0f;
    [SerializeField] private float stopTolerance = 2f;

    [Header("References")]
    [SerializeField] private Transform[] reelImages;
    [SerializeField] private SymbolType[] symbols;

    [HideInInspector]
    public int selectedSymbolIndex;
    public bool IsSpinning => shouldSpin;

    private float currentSpeed;
    private bool isStopping;
    private bool shouldSpin;

    private AudioManager audioManager;
    private AudioSource audioSource;

    private void Start()
    {
        audioManager = FindAnyObjectByType<AudioManager>();
        audioSource = GetComponent<AudioSource>();
        PlaceImagesAtRest(0); // default: symbol 0 at center on startup
    }

    // Arranges images so that targetIndex sits at centerY,
    // and all others are evenly spaced above it.
    private void PlaceImagesAtRest(int targetIndex)
    {
        int n = reelImages.Length;
        for (int i = 0; i < n; i++)
        {
            // delta: how many slots above center this image should sit
            int delta = i - targetIndex;
            // Wrap so no image ends up far below center at rest
            // e.g. for 4 images, delta can be -1, 0, 1, 2
            while (delta < -1) delta += n;   // one slot below is fine, more is not
            while (delta > n - 2) delta -= n;

            float y = centerY + delta * spacing;
            Vector3 p = reelImages[i].localPosition;
            reelImages[i].localPosition = new Vector3(p.x, y, p.z);
        }
    }

    public void Spin()
    {
        if (shouldSpin) return;
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

        // Wait until the target image is close to centerY
        Transform target = reelImages[selectedSymbolIndex];
        while (Mathf.Abs(target.localPosition.y - centerY) > stopTolerance)
        {
            yield return null;
        }

        // Hard snap: place every image on a perfect grid, no accumulated error
        currentSpeed = 0f;
        shouldSpin = false;
        PlaceImagesAtRest(selectedSymbolIndex);

        audioSource.Stop();
        audioSource.PlayOneShot(audioManager.clips[3]);
        Debug.Log($"Stopped On : {symbols[selectedSymbolIndex]}");
    }

    private void Update()
    {
        if (!shouldSpin) return;

        if (!isStopping)
        {
            currentSpeed = Mathf.Lerp(currentSpeed, maxSpinSpeed, acceleration * Time.deltaTime);
        }
        else
        {
            currentSpeed = Mathf.Lerp(currentSpeed, 0f, deceleration * Time.deltaTime);
        }

        float delta = currentSpeed * Time.deltaTime;

        foreach (Transform img in reelImages)
        {
            float newY = img.localPosition.y - delta;

            // Once an image scrolls below wrapBelowY, jump it to wrapAboveY
            if (newY <= wrapBelowY)
                newY += (wrapAboveY - wrapBelowY);

            img.localPosition = new Vector3(img.localPosition.x, newY, img.localPosition.z);
        }
    }
}