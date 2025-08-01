//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;
//using UnityEngine.UI;

//public class HouseColorChange : MonoBehaviour
//{
//    [Tooltip("The correct ingredient for this bowl/screen, e.g. Flour")]
//    public string expectedIngredientTag;

//    //public Button tickButton;   // confirm
//    //public Button crossButton;  // undo

//    public GameObject thisScreenDisable;
//    public GameObject NextScreenEnable;




//    private ColorDradDrop currentIngredient;

//    void Start()
//    {
//        //if (tickButton) tickButton.onClick.AddListener(OnTick);
//        //if (crossButton) crossButton.onClick.AddListener(OnCross);

//        //if (tickButton) tickButton.gameObject.SetActive(false);
//        //if (crossButton) crossButton.gameObject.SetActive(false);
//    }

//    private void OnTriggerEnter2D(Collider2D other)
//    {
//        var drag = other.GetComponent<ColorDradDrop>();
//        if (drag == null) return;

//        if (drag.ingredientTag == expectedIngredientTag)
//        {
//            Debug.Log($"Right ingredient: {drag.ingredientTag} entered bowl.");
//            currentIngredient = drag;

//            GetComponent<Image>().color = drag.selectedColor;


//            ///thisImage.color = drag.selectedColor;

//            //// show options
//            //if (tickButton) tickButton.gameObject.SetActive(true);
//            //if (crossButton) crossButton.gameObject.SetActive(true);

//            // temporarily hide and disable dragging (pending confirmation)
//            drag.gameObject.SetActive(false);
//            drag.DisableDrag();
//            StartCoroutine(ColorBackToOrgPosition());
//        }
//        else
//        {
//            Debug.Log($"Wrong ingredient ({drag.ingredientTag}) for this screen. Expected: {expectedIngredientTag}.");
//            // immediate undo
//            drag.ResetPositionImmediately();
//        }
//    }



//    IEnumerator ColorBackToOrgPosition()
//    {
//        yield return new WaitForSeconds(1f);
//        // restore ingredient, re-enable dragging fully
//        currentIngredient.gameObject.SetActive(true);
//        currentIngredient.ForceReset(); // <- use the new method

//        currentIngredient = null;
//    }



//    public void OnNextScreenEnable()
//    {
//        thisScreenDisable.SetActive(false);
//        NextScreenEnable.SetActive(true);
//    }




//    //private void OnTick()
//    //{
//    //    if (currentIngredient == null) return;

//    //    Debug.Log($"Ingredient {currentIngredient.ingredientTag} confirmed. Proceed."); // proceed to next step externally

//    //    // disable buttons
//    //    if (tickButton) tickButton.gameObject.SetActive(false);
//    //    if (crossButton) crossButton.gameObject.SetActive(false);
//    //    OnNextScreenEnable();

//    //    // keep ingredient hidden/used; clear reference
//    //    currentIngredient = null;
//    //}

//    //private void OnCross()
//    //{
//    //    if (currentIngredient == null) return;

//    //    Debug.Log($"Undo: returning ingredient {currentIngredient.ingredientTag} to original position.");

//    //    // restore ingredient, re-enable dragging fully
//    //    currentIngredient.gameObject.SetActive(true);
//    //    currentIngredient.ForceReset(); // <- use the new method

//    //    // hide buttons
//    //    if (tickButton) tickButton.gameObject.SetActive(false);
//    //    if (crossButton) crossButton.gameObject.SetActive(false);

//    //    currentIngredient = null;
//    //}


//}



using System.Collections;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Image))]
public class HouseColorChange : MonoBehaviour
{
    [Tooltip("The correct ingredient for this bowl/screen, e.g. Flour")]
    public string expectedIngredientTag;

    public GameObject thisScreenDisable;
    public GameObject NextScreenEnable;

    private ColorDradDrop currentIngredient;
    private bool isColored = false;
    public bool IsColored => isColored;

    private HouseColorTracker tracker;

    void Start()
    {
        tracker = GetComponentInParent<HouseColorTracker>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (isColored) return; // already done

        var drag = other.GetComponent<ColorDradDrop>();
        if (drag == null) return;

        if (drag.ingredientTag == expectedIngredientTag)
        {
            Debug.Log($"Right ingredient: {drag.ingredientTag} entered bowl.");
            currentIngredient = drag;

            // apply color
            GetComponent<Image>().color = drag.selectedColor;

            // mark done
            isColored = true;

            // notify tracker
            if (tracker != null)
                tracker.NotifySpotColored(this);

            // temporary disable drag and hide then restore
            drag.gameObject.SetActive(false);
            drag.DisableDrag();
            StartCoroutine(ColorBackToOrgPosition());
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
        if (currentIngredient != null)
        {
            currentIngredient.gameObject.SetActive(true);
            currentIngredient.ForceReset();
            currentIngredient = null;
        }
    }

    public void OnNextScreenEnable()
    {
        if (thisScreenDisable) thisScreenDisable.SetActive(false);
        if (NextScreenEnable) NextScreenEnable.SetActive(true);
    }
}
