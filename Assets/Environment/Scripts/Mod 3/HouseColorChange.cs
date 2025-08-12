



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
