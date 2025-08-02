using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Loadingscreen : MonoBehaviour
{

    [Header(" Loading ")]
    public GameObject StartloadingScreen;
    public Slider loadingSlider;
    public TextMeshProUGUI loadingText;
    public float fakeLoadDuration = 5f; // Duration of fake loading in seconds
    public GameObject mainScreenEnable;


    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(StartLoading());
    }

    // Update is called once per frame
    void Update()
    {
        
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
        if(mainScreenEnable != null)
        {
            mainScreenEnable.SetActive(true);
        }
        
        timer = 0f;
        //messageText.text = " Game is Ready!"; //  Your final message here

        //yield return new WaitForSeconds(2f); // Optional: wait before hiding
        StartloadingScreen.SetActive(false);
    }



}
