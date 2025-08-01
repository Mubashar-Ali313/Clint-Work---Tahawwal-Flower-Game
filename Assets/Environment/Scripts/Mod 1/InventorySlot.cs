using UnityEngine;
using UnityEngine.EventSystems;
using TMPro;
using UnityEngine.UI;

public class InventorySlot : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public string flowerID;               // e.g., "Rose"
    public TextMeshProUGUI countText;     // Shows how many collected
    public Slider progressSlider;         // UI slider for visual fill
    public GameObject tickIcon;           // GameObject to show when full
    [Min(1)]
    public int requiredCount = 5;         // Set in inspector (max slider value)

    private int currentCount = 0;

    public static InventorySlot currentTargetSlot;

    public delegate void OnFlowerCompleted(InventorySlot slot);
    public static event OnFlowerCompleted OnFlowerCompletedEvent;

    void Start()
    {
        if (progressSlider != null)
        {
            progressSlider.maxValue = Mathf.Max(1, requiredCount);
            progressSlider.value = 0;
        }

        if (tickIcon != null)
            tickIcon.SetActive(false);

        UpdateVisuals();
    }

    public void AddFlower()
    {
        if (currentCount >= requiredCount)
        {
            Debug.Log($" {flowerID} already full");
            return;
        }

        currentCount++;
        UpdateVisuals();

        if (currentCount >= requiredCount)
        {
            Debug.Log($" {flowerID} collection complete.");
            if (tickIcon != null) tickIcon.SetActive(true);

            OnFlowerCompletedEvent?.Invoke(this);
        }
    }

    private void UpdateVisuals()
    {
        if (countText != null)
            countText.text = $"{currentCount} / {requiredCount}";

        if (progressSlider != null)
            progressSlider.value = currentCount;
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        currentTargetSlot = this;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        if (currentTargetSlot == this)
            currentTargetSlot = null;
    }

    public bool IsComplete()
    {
        return currentCount >= requiredCount;
    }
}
