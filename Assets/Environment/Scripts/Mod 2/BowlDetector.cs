using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Collider2D))]
public class BowlDetector : MonoBehaviour
{
    [Tooltip("The correct ingredient for this bowl/screen, e.g. Flour")]
    public string expectedIngredientTag;

    public Button tickButton;   // confirm
    public Button crossButton;  // undo

    public GameObject thisScreenDisable;
    public GameObject NextScreenEnable;



    private Mod2Ui currentIngredient;

    void Start()
    {
        if (tickButton) tickButton.onClick.AddListener(OnTick);
        if (crossButton) crossButton.onClick.AddListener(OnCross);

        if (tickButton) tickButton.gameObject.SetActive(false);
        if (crossButton) crossButton.gameObject.SetActive(false);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        var drag = other.GetComponent<Mod2Ui>();
        if (drag == null) return;

        if (drag.ingredientTag == expectedIngredientTag)
        {
            Debug.Log($"Right ingredient: {drag.ingredientTag} entered bowl.");
            currentIngredient = drag;

            // show options
            if (tickButton) tickButton.gameObject.SetActive(true);
            if (crossButton) crossButton.gameObject.SetActive(true);

            // temporarily hide and disable dragging (pending confirmation)
            drag.gameObject.SetActive(false);
            drag.DisableDrag();
        }
        else
        {
            Debug.Log($"Wrong ingredient ({drag.ingredientTag}) for this screen. Expected: {expectedIngredientTag}.");
            // immediate undo
            drag.ResetPositionImmediately();
        }
    }



    public void OnNextScreenEnable()
    {
        thisScreenDisable.SetActive(false);
        NextScreenEnable.SetActive(true);
    }




    private void OnTick()
    {
        if (currentIngredient == null) return;

        Debug.Log($"Ingredient {currentIngredient.ingredientTag} confirmed. Proceed."); // proceed to next step externally

        // disable buttons
        if (tickButton) tickButton.gameObject.SetActive(false);
        if (crossButton) crossButton.gameObject.SetActive(false);
        OnNextScreenEnable();

        // keep ingredient hidden/used; clear reference
        currentIngredient = null;
    }

    private void OnCross()
    {
        if (currentIngredient == null) return;

        Debug.Log($"Undo: returning ingredient {currentIngredient.ingredientTag} to original position.");

        // restore ingredient, re-enable dragging fully
        currentIngredient.gameObject.SetActive(true);
        currentIngredient.ForceReset(); // <- use the new method

        // hide buttons
        if (tickButton) tickButton.gameObject.SetActive(false);
        if (crossButton) crossButton.gameObject.SetActive(false);

        currentIngredient = null;
    }






}
