using System.Collections;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class DrumHitAnimatorOnly : MonoBehaviour
{
    [Header("Manual Movement")]
    public float upOffset = 20f;
    public float hitDuration = 0.15f;

    [Header("Sound")]
    public AudioClip hitSound;
    private AudioSource audioSource;

    [Header("Rate limiting")]
    public float cooldown = 0.3f;
    private float lastHitTime = -999f;

    private RectTransform rect;
    private Vector3 originalPos;

    void Awake()
    {
        audioSource = GetComponent<AudioSource>();
        rect = GetComponent<RectTransform>();
        if (rect != null)
            originalPos = rect.localPosition;
        if (audioSource != null)
            audioSource.playOnAwake = false;
    }

    void OnMouseDown()
    {
        TryHit();
    }

    public void TryHit()
    {
        if (Time.time - lastHitTime < cooldown) return;
        lastHitTime = Time.time;

        PlaySound();
        if (rect != null)
            StartCoroutine(HitAnimation());
    }

    private IEnumerator HitAnimation()
    {
        Vector3 upPos = originalPos + new Vector3(0, upOffset, 0);
        float half = hitDuration / 2f;
        float t = 0f;

        // move up
        while (t < half)
        {
            t += Time.deltaTime;
            rect.localPosition = Vector3.Lerp(originalPos, upPos, t / half);
            yield return null;
        }

        // move down
        t = 0f;
        while (t < half)
        {
            t += Time.deltaTime;
            rect.localPosition = Vector3.Lerp(upPos, originalPos, t / half);
            yield return null;
        }

        rect.localPosition = originalPos;
    }

    private void PlaySound()
    {
        if (audioSource == null || hitSound == null) return;
        audioSource.Stop();
        audioSource.clip = hitSound;
        audioSource.Play();
    }
}
