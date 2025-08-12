using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class LevelCompleteSound : MonoBehaviour
{
    public AudioClip completeSound; // Sound to play on level complete
    private AudioSource audioSource;

    void Awake()
    {
        audioSource = GetComponent<AudioSource>();
        audioSource.playOnAwake = false;
    }

    public void PlayLevelCompleteSound()
    {
        if (completeSound != null)
        {
            audioSource.PlayOneShot(completeSound);
        }
        else
        {
            Debug.LogWarning("Complete sound not assigned on " + gameObject.name);
        }
    }
}
