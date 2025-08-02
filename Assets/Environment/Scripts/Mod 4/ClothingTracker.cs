using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections;

public class ClothingTracker : MonoBehaviour
{
    private int totalItems = 0;
    private int placedItems = 0;


    


    [Header("UI Text for Placed Count")]
    public TextMeshProUGUI placedCounterText; //  UI Text Reference

    public GameObject submitButton;
    public GameObject rewardedScreen;
    public GameObject Mod4;





    [Header(" Loading ")]
    public GameObject StartloadingScreen;
    public Slider loadingSlider;
    public TextMeshProUGUI loadingText;
    public float fakeLoadDuration = 5f; // Duration of fake loading in seconds




    private void Start()
    {
        StartCoroutine(StartLoading());

        submitButton.SetActive(false);
        // Count all ChangingClothes in scene
        totalItems = FindObjectsOfType<ChangingClothes>().Length;
        Debug.Log(" Total Required Items: " + totalItems);

        UpdateUI();
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
       
        timer = 0f;
        //messageText.text = " Game is Ready!"; //  Your final message here
        Mod4.SetActive(true);

        //yield return new WaitForSeconds(2f); // Optional: wait before hiding
        StartloadingScreen.SetActive(false);
    }







    public void ItemPlaced()
    {
        placedItems++;
        Debug.Log(" Placed Items: " + placedItems + " / " + totalItems);

        UpdateUI();

        if (placedItems >= totalItems)
        {
            Debug.Log(" All clothing items placed!");
            // Optionally call OnLevelComplete() or show panel
            submitButton.SetActive(true);

        }
    }

    private void UpdateUI()
    {
        if (placedCounterText != null)
        {
            placedCounterText.text = "Placed: " + placedItems + " / " + totalItems;
        }
    }

    public void ShowRewardedScreen()
    {
        submitButton.SetActive(false);
        Mod4.SetActive(false);
        rewardedScreen.SetActive(true);
    }

    

}
