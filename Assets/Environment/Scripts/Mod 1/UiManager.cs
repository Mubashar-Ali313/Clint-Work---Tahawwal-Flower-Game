using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UiManager : MonoBehaviour
{
    [Header ( " Loading ")]
    public GameObject StartloadingScreen;
    public Slider loadingSlider;
    public TextMeshProUGUI loadingText;
    // public Text messageText; //  New Text field for end message


    [SerializeField] GameObject homeScreen;
    [SerializeField] GameObject modeSelectionScreen;
    public GameObject mode1;
    public GameObject inventry;
    public GameObject gameInfoScreen;


    public float fakeLoadDuration = 5f; // Duration of fake loading in seconds

    void Start()
    {
        StartCoroutine(StartLoading());
    }


    IEnumerator StartLoading()
    {
        StartloadingScreen.SetActive(true);
       // messageText.text = ""; // Clear any old message

        float timer = 0f;
        while (timer < fakeLoadDuration)
        {
            timer += Time.deltaTime;
            float progress = Mathf.Clamp01(timer / fakeLoadDuration);
            loadingSlider.value = progress;
            loadingText.text = "Loading... " + (int)(progress * 100f) + "%";

            yield return null;
        }

        loadingText.text = "Loading Complete!";
        homeScreen.SetActive(true);
        timer = 0f;
        //messageText.text = " Game is Ready!"; //  Your final message here

        //yield return new WaitForSeconds(2f); // Optional: wait before hiding
        StartloadingScreen.SetActive(false);
    }

    public void SelectionScreenEnable()
    {
        homeScreen.SetActive(false);
        modeSelectionScreen.SetActive(true);
    }

    public void Mode1Start()
    {
        modeSelectionScreen.SetActive(false);
        StartCoroutine(GameLoading());
    }


    IEnumerator GameLoading()
    {
        StartloadingScreen.SetActive(true);
        // messageText.text = ""; // Clear any old message

        float timer = 0f;
        while (timer < fakeLoadDuration)
        {
            timer += Time.deltaTime;
            float progress = Mathf.Clamp01(timer / fakeLoadDuration);
            loadingSlider.value = progress;
            loadingText.text = "Loading... " + (int)(progress * 100f) + "%";

            yield return null;
        }

        loadingText.text = "Loading Complete!";
        mode1.SetActive(true);
        inventry.SetActive(true);
        gameInfoScreen.SetActive(true);
        timer = 0f;
        //messageText.text = " Game is Ready!"; //  Your final message here

        //yield return new WaitForSeconds(2f); // Optional: wait before hiding
        StartloadingScreen.SetActive(false);
    }


    void PlayModeGames()
    {

    }


}
