using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class NameInputManager : MonoBehaviour
{
    [SerializeField] UiManager uiManager;
    

    public TMP_InputField nameInput; // Drag your UI InputField here
   

    void Start()
    {
        // Agar name already saved hai to directly next scene pe chale jao
        
    }

    public void OnSubmitName()
    {
        string playerName = nameInput.text.Trim();

        if (!string.IsNullOrEmpty(playerName))
        {
            PlayerPrefs.SetString("playerName", playerName); // Save name
            PlayerPrefs.Save(); // Save to disk

            uiManager.NameinputScreen.SetActive(false);
            uiManager.homeScreen.SetActive(true);
            

        }
        else
        {
            Debug.Log("Name is empty!");
        }
    }
}
