using UnityEngine;

public class SongsManager : MonoBehaviour
{
    [SerializeField] float volume = 0.6f;
    private AudioSource audioSource;

    private AudioClip currentClip;

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
        currentClip = audioSource.clip;
    }

    public void PlaySong(AudioClip clip)
    {
        if (clip != currentClip)
        {
            audioSource.Stop();
            audioSource.PlayOneShot(clip, volume);
            currentClip = clip;
        }
    }
}
