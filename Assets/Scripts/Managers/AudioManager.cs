using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public AudioClip[] clips;
    [SerializeField] private AudioSource audioSource;
    [SerializeField] private AudioSource sfxSource;
    [SerializeField] private AudioSource leverAudioSource;
    [SerializeField] private AudioClip bgMusicClip;

    public void PlayClip(AudioClip clip)
    {
        sfxSource.clip = clip;
        sfxSource.Play();
        // StartCoroutine(PlayTimedClip(clip, time));
    }
    public void StopPlayingCLip()
    {
        sfxSource.Stop();
    }
    public void PlayClipOneShot(AudioClip clip)
    {
        Debug.Log($"Playing Clip : {clip.name}");
        audioSource.PlayOneShot(clip);
    }
    // private IEnumerator PlayTimedClip(AudioClip clip, float time)
    // {
    //     sfxSource.PlayOneShot(clip);
    //     yield return new WaitForSeconds(time);
    //     sfxSource.Stop();
    // }
    public void StopBGM()
    {
        audioSource.mute = true;
    }
    public void PlayBGM()
    {
        audioSource.mute = false;
    }
    public void PlayLeverSound(AudioClip clip)
    {
        leverAudioSource.PlayOneShot(clip);
    }
}
