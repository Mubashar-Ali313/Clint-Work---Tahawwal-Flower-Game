using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;



[RequireComponent(typeof(CanvasGroup))]
public class ColorDradDrop : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    [Tooltip("Name of the Colors, e.g. Red, Blue, Green, Yellow")]
    public string ingredientTag;

    [Header("Color to apply (set in Inspector)")]
    public Color selectedColor = Color.white;


    [HideInInspector] public Vector3 originalPosition;
    private RectTransform rect;
    private Canvas canvas;
    private CanvasGroup canvasGroup;
    private Vector2 pointerOffset;
    private Coroutine returnCoroutine;
    private bool isDragging = false;
    private bool isAllowedToDrag = true;

    void Awake()
    {
        rect = GetComponent<RectTransform>();
        canvasGroup = GetComponent<CanvasGroup>();
        canvas = GetComponentInParent<Canvas>();
        originalPosition = rect.localPosition;
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        if (!isAllowedToDrag) return;

        isDragging = true;
        canvasGroup.alpha = 0.6f;
        canvasGroup.blocksRaycasts = false;

        RectTransformUtility.ScreenPointToLocalPointInRectangle(
            canvas.transform as RectTransform,
            eventData.position,
            eventData.pressEventCamera,
            out Vector2 localPointerPos);

        pointerOffset = rect.localPosition - (Vector3)localPointerPos;

        if (returnCoroutine != null)
        {
            StopCoroutine(returnCoroutine);
            returnCoroutine = null;
        }
    }

    public void OnDrag(PointerEventData eventData)
    {
        if (!isDragging || !isAllowedToDrag) return;

        if (RectTransformUtility.ScreenPointToLocalPointInRectangle(
            canvas.transform as RectTransform,
            eventData.position,
            eventData.pressEventCamera,
            out Vector2 localPoint))
        {
            rect.localPosition = localPoint + pointerOffset;
        }
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        if (!isAllowedToDrag) return;

        isDragging = false;
        canvasGroup.alpha = 1f;
        canvasGroup.blocksRaycasts = true;

        if (returnCoroutine != null)
        {
            StopCoroutine(returnCoroutine);
        }
        returnCoroutine = StartCoroutine(SmoothReturnIfNeeded());
    }

    private IEnumerator SmoothReturnIfNeeded()
    {
        // Only return if not already at original (avoid jump if confirmed)
        if (Vector3.Distance(rect.localPosition, originalPosition) < 0.01f)
            yield break;

        float duration = 0.25f;
        float t = 0f;
        Vector3 start = rect.localPosition;
        while (t < duration)
        {
            t += Time.deltaTime;
            rect.localPosition = Vector3.Lerp(start, originalPosition, t / duration);
            yield return null;
        }
        rect.localPosition = originalPosition;
    }

    public void ResetPositionImmediately()
    {
        if (returnCoroutine != null) StopCoroutine(returnCoroutine);
        rect.localPosition = originalPosition;
    }

    public void DisableDrag()
    {
        isAllowedToDrag = false;
    }

    public void EnableDrag()
    {
        isAllowedToDrag = true;
    }


    // inside IngredientDragHandlerSimple

    public void ForceReset()
    {
        // stop any return coroutine
        if (returnCoroutine != null)
        {
            StopCoroutine(returnCoroutine);
            returnCoroutine = null;
        }

        // restore position instantly
        rect.localPosition = originalPosition;

        // restore visuals & dragability
        canvasGroup.alpha = 1f;
        canvasGroup.blocksRaycasts = true;
        isAllowedToDrag = true;
        isDragging = false;
    }


}
