using UnityEngine;
using TMPro;
using System.Collections;

public class ObjectSwitcher : MonoBehaviour
{
    [Header("Dadge name dispaly")]
    public TMP_Text badgename;


    [Header("Showing screens")]
    public GameObject rewardScreen;
    public GameObject modsScreen;

    [Header("UI References")]
    public TMP_InputField nameInputTMP;
    public TMP_Text displayTextTMP;

    [Header("Options")]
    public string prefix = "Good, ";


    [Header("Drum Sticks")]
    public GameObject obj1;
    public GameObject obj2;
    public GameObject obj3;

    [Header("Timer Settings")]
    [Tooltip("Duration in minutes")]
    public float durationSeconds = 60f;

    [Tooltip("Text to show remaining time (regular UI)")]
    public TMP_Text countdownText;
    private float remaining;
    private bool running = false;

    private void Start()
    {
        if (rewardScreen != null)
            rewardScreen.SetActive(false);
        StartTimer();
    }



    // Call this when Button 1 is clicked
    public void FirstSecond()
    {
        obj1.SetActive(true);
        obj2.SetActive(false);
        obj3.SetActive(false);
    }

    // Call this when Button 1 is clicked
    public void ShowSecond()
    {
        obj1.SetActive(false);
        obj2.SetActive(true);
        obj3.SetActive(false);
    }

    // Call this when Button 2 is clicked
    public void ShowThird()
    {
        obj1.SetActive(false);
        obj2.SetActive(false);
        obj3.SetActive(true);
    }




    // time duration setup
    public void StartTimer()
    {
        remaining = Mathf.Max(0.01f, durationSeconds);
        if (!running)
            StartCoroutine(Countdown());
    }

    private IEnumerator Countdown()
    {
        running = true;
        while (remaining > 0f)
        {
            UpdateDisplay(remaining);
            yield return null;
            remaining -= Time.deltaTime;
        }

        UpdateDisplay(0f);
        running = false;
        OnTimeUp();
    }

    private void UpdateDisplay(float seconds)
    {
        int mins = Mathf.FloorToInt(seconds / 60f);
        int secs = Mathf.FloorToInt(seconds % 60f);
        string formatted = $"{mins:00}:{secs:00}";

        if (countdownText != null)
            countdownText.text = formatted;

#if TMP_PRESENT
        if (countdownTextTMP != null)
            countdownTextTMP.text = formatted;
#endif
    }

    private void OnTimeUp()
    {
        Debug.Log(" Time completed. Showing reward panel.");
        if (rewardScreen != null)
        {
            rewardScreen.SetActive(true);
            modsScreen.SetActive(false);
        }
            
        
    }





    public void ApplyName()
    {
        if (nameInputTMP == null || displayTextTMP == null) return;

        string name = nameInputTMP.text.Trim();
        if (string.IsNullOrEmpty(name)) return;

        displayTextTMP.text = prefix + name;
        badgename.text =  name;
    }


    




}