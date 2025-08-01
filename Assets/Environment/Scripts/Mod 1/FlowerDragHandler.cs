using UnityEngine;
using UnityEngine.EventSystems;
using System.Collections;
using System.Collections.Generic;

public class FlowerDragHandler : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    private RectTransform rectTransform;
    private Canvas canvas;
    private CanvasGroup canvasGroup;
    private Vector2 offset;
    private Vector3 originalPosition;
    private Coroutine returnCoroutine;

    private InventorySlot detectedSlot;

    void Awake()
    {
        rectTransform = GetComponent<RectTransform>();
        canvasGroup = GetComponent<CanvasGroup>();
        canvas = GetComponentInParent<Canvas>();
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        canvasGroup.alpha = 0.6f;
        canvasGroup.blocksRaycasts = false;
        originalPosition = rectTransform.localPosition;

        RectTransformUtility.ScreenPointToLocalPointInRectangle(
            canvas.transform as RectTransform,
            eventData.position,
            eventData.pressEventCamera,
            out offset);

        offset = rectTransform.localPosition - (Vector3)offset;

        if (returnCoroutine != null)
        {
            StopCoroutine(returnCoroutine);
            returnCoroutine = null;
        }
    }

    public void OnDrag(PointerEventData eventData)
    {
        Vector2 localPoint;
        if (RectTransformUtility.ScreenPointToLocalPointInRectangle(
            canvas.transform as RectTransform,
            eventData.position,
            eventData.pressEventCamera,
            out localPoint))
        {
            rectTransform.localPosition = localPoint + offset;
        }
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        canvasGroup.alpha = 1f;
        canvasGroup.blocksRaycasts = true;

        List<RaycastResult> results = new List<RaycastResult>();
        EventSystem.current.RaycastAll(eventData, results);

        bool addedToSlot = false;

        foreach (RaycastResult result in results)
        {
            InventorySlot slot = result.gameObject.GetComponent<InventorySlot>();
            if (slot != null && slot.flowerID == this.gameObject.name)
            {
                slot.AddFlower();              // +1 to count
                Destroy(gameObject);          // destroy flower
                addedToSlot = true;
                Debug.Log("Flower matched and added via raycast.");
                return;
            }
        }

        // Fallback: Trigger-based detection
        if (!addedToSlot && detectedSlot != null)
        {
            if (detectedSlot.flowerID == this.gameObject.name)
            {
                detectedSlot.AddFlower();
                Destroy(gameObject);
                Debug.Log("Flower matched and added via trigger.");
                return;
            }
        }

        // If nothing matched, return to original position
        Debug.Log("No match found. Returning to original position.");
        returnCoroutine = StartCoroutine(ReturnToOriginalPosition());
    }

    private IEnumerator ReturnToOriginalPosition()
    {
        float duration = 0.3f;
        float time = 0f;
        Vector3 startPos = rectTransform.localPosition;

        while (time < duration)
        {
            time += Time.deltaTime;
            rectTransform.localPosition = Vector3.Lerp(startPos, originalPosition, time / duration);
            yield return null;
        }

        rectTransform.localPosition = originalPosition;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        InventorySlot slot = other.GetComponent<InventorySlot>();
        if (slot != null)
        {
            detectedSlot = slot;
            Debug.Log("Trigger Enter: " + slot.name);
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        InventorySlot slot = other.GetComponent<InventorySlot>();
        if (slot != null && slot == detectedSlot)
        {
            detectedSlot = null;
            Debug.Log("Trigger Exit: " + slot.name);
        }
    }
}


