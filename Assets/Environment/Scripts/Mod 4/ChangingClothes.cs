using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChangingClothes : MonoBehaviour
{

    [Tooltip("The correct ingredient for this bowl/screen, e.g. Flour")]
    public string expectedIngredientTag;

    //public Button tickButton;   // confirm
    //public Button crossButton;  // undo

    public GameObject thisScreenDisable;
    public GameObject NextScreenEnable;
    public Sprite thisImage;



    private ItemDragDrop currentIngredient;
    CanvasGroup changeImageTransperancy;

    [Header("Tracker")]
    public ClothingTracker tracker; //  Drag your manager here

    private bool alreadyPlaced = false; //  prevent multiple counts


    void Start()
    {

        changeImageTransperancy = this.gameObject.GetComponent<CanvasGroup>();
        changeImageTransperancy.alpha = 0.6f;
        changeImageTransperancy.blocksRaycasts = false;
        tracker = FindObjectOfType<ClothingTracker>();

        //if (tickButton) tickButton.onClick.AddListener(OnTick);
        //if (crossButton) crossButton.onClick.AddListener(OnCross);

        //if (tickButton) tickButton.gameObject.SetActive(false);
        //if (crossButton) crossButton.gameObject.SetActive(false);
    }

    //private void OnTriggerEnter2D(Collider2D other)
    //{
    //    var drag = other.GetComponent<ItemDragDrop>();
    //    if (drag == null) return;

    //    if (drag.ingredientTag == expectedIngredientTag)
    //    {
    //        Debug.Log($"Right ingredient: {drag.ingredientTag} entered bowl.");
    //        currentIngredient = drag;
    //        var thisImage = this.gameObject.GetComponent<Image>();
    //        thisImage.sprite = drag.selectedColor;




    //        changeImageTransperancy.alpha = 1f;
    //        changeImageTransperancy.blocksRaycasts = true;
    //        //// show options
    //        //if (tickButton) tickButton.gameObject.SetActive(true);
    //        //if (crossButton) crossButton.gameObject.SetActive(true);

    //        // temporarily hide and disable dragging (pending confirmation)
    //        drag.gameObject.SetActive(false);
    //        drag.DisableDrag();
    //       // StartCoroutine(ColorBackToOrgPosition());
    //    }
    //    else
    //    {
    //        Debug.Log($"Wrong ingredient ({drag.ingredientTag}) for this screen. Expected: {expectedIngredientTag}.");
    //        // immediate undo
    //        drag.ResetPositionImmediately();
    //    }
    //}


    private void OnTriggerEnter2D(Collider2D other)
    {
        if (alreadyPlaced) return; // prevent multiple triggers

        var drag = other.GetComponent<ItemDragDrop>();
        if (drag == null) return;

        if (drag.ingredientTag == expectedIngredientTag)
        {
            Debug.Log($"Right ingredient: {drag.ingredientTag} entered bowl.");
            currentIngredient = drag;

            GetComponent<Image>().sprite = drag.selectedColor;

            changeImageTransperancy.alpha = 1f;
            changeImageTransperancy.blocksRaycasts = true;

            drag.gameObject.SetActive(false);
            drag.DisableDrag();

            alreadyPlaced = true;

            // Notify tracker
            if (tracker != null)
            {
                tracker.ItemPlaced();
            }
        }
        else
        {
            Debug.Log($"Wrong ingredient ({drag.ingredientTag}) for this screen. Expected: {expectedIngredientTag}.");
            drag.ResetPositionImmediately();
        }
    }



    IEnumerator ColorBackToOrgPosition()
    {
        yield return new WaitForSeconds(1f);
        // restore ingredient, re-enable dragging fully
        currentIngredient.gameObject.SetActive(true);
        currentIngredient.ForceReset(); // <- use the new method

        currentIngredient = null;
    }



    public void OnNextScreenEnable()
    {
        thisScreenDisable.SetActive(false);
        NextScreenEnable.SetActive(true);
    }




    //private void OnTick()
    //{
    //    if (currentIngredient == null) return;

    //    Debug.Log($"Ingredient {currentIngredient.ingredientTag} confirmed. Proceed."); // proceed to next step externally

    //    // disable buttons
    //    if (tickButton) tickButton.gameObject.SetActive(false);
    //    if (crossButton) crossButton.gameObject.SetActive(false);
    //    OnNextScreenEnable();

    //    // keep ingredient hidden/used; clear reference
    //    currentIngredient = null;
    //}

    //private void OnCross()
    //{
    //    if (currentIngredient == null) return;

    //    Debug.Log($"Undo: returning ingredient {currentIngredient.ingredientTag} to original position.");

    //    // restore ingredient, re-enable dragging fully
    //    currentIngredient.gameObject.SetActive(true);
    //    currentIngredient.ForceReset(); // <- use the new method

    //    // hide buttons
    //    if (tickButton) tickButton.gameObject.SetActive(false);
    //    if (crossButton) crossButton.gameObject.SetActive(false);

    //    currentIngredient = null;
    //}
}
