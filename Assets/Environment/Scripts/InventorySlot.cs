using UnityEngine;
using UnityEngine.EventSystems;
using TMPro;

public class InventorySlot : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public string flowerID; // e.g., "Rose", "Tulip"
    public TextMeshProUGUI countText;
    private int count = 0;

    public static InventorySlot currentTargetSlot;

    public void AddFlower()
    {
        count++;
        UpdateText();
    }

    private void UpdateText()
    {
        if (countText != null)
            countText.text = count.ToString();
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
}
