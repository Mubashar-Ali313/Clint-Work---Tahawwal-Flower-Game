using UnityEngine;

public class FlowerRewardController : MonoBehaviour
{
    public GameObject rewardPanel;
    public ZoomManager zoomManager;


    private bool rewardShown = false;

    private void OnEnable()
    {
        InventorySlot.OnFlowerCompletedEvent += CheckAllComplete;
    }

    private void OnDisable()
    {
        InventorySlot.OnFlowerCompletedEvent -= CheckAllComplete;
    }

    void Start()
    {
        if (rewardPanel != null)
            rewardPanel.SetActive(false);
    }

    void CheckAllComplete(InventorySlot completedSlot)
    {
        if (rewardShown) return; // already shown once

        InventorySlot[] allSlots = FindObjectsOfType<InventorySlot>();

        foreach (var slot in allSlots)
        {
            if (!slot.IsComplete())
                return; // koi bhi incomplete mila to exit
        }

        // agar yahan tak aya to sab complete hain
        Debug.Log(" All flower slots complete! Showing reward panel.");
        if (zoomManager.isZoomed)
        {
            zoomManager.ZoomOut();
        }
        if (rewardPanel != null)
            rewardPanel.SetActive(true);


        rewardShown = true;
    }
}
