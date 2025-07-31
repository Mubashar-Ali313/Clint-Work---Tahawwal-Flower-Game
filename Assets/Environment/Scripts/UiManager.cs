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


    [SerializeField] GameObject nextScreenAfterLoading;
    public GameObject startGame;
    public GameObject inventry;


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
        nextScreenAfterLoading.SetActive(true);
        timer = 0f;
        //messageText.text = " Game is Ready!"; //  Your final message here

        //yield return new WaitForSeconds(2f); // Optional: wait before hiding
        StartloadingScreen.SetActive(false);
    }

    public void GameStart()
    {
        nextScreenAfterLoading.SetActive(false);
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
        startGame.SetActive(true);
        inventry.SetActive(true);
        timer = 0f;
        //messageText.text = " Game is Ready!"; //  Your final message here

        //yield return new WaitForSeconds(2f); // Optional: wait before hiding
        StartloadingScreen.SetActive(false);
    }


}
