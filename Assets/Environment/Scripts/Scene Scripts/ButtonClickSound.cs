using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(AudioSource))]
public class ButtonClickSound : MonoBehaviour
{
    public AudioClip clickSound; // Sound to play when button is clicked
    private AudioSource audio1;
    public GameObject audioSource;

    void Awake()
    {
        audio1 = audioSource.GetComponent<AudioSource>();
        audio1.playOnAwake = false;
    }

    void Start()
    {
        
    }

    public void PlayClickSound()
    {
        if (clickSound != null)
        {
            audio1.PlayOneShot(clickSound);
        }
        else
        {
            Debug.LogWarning("Click sound not assigned on " + gameObject.name);
        }
    }
}
