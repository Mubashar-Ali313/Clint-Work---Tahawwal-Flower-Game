using UnityEngine;
using UnityEngine.Events;

public class HouseColorTracker : MonoBehaviour
{
    [Header("UI / Reward")]
    public GameObject rewardPanel; // assign your reward window here
    public UnityEvent onAllColored; // optional hook

    private HouseColorChange[] spots;
    private bool completed = false;

    void Start()
    {
        spots = GetComponentsInChildren<HouseColorChange>(true);
        if (rewardPanel != null)
            rewardPanel.SetActive(false);
    }

    public void NotifySpotColored(HouseColorChange spot)
    {
        if (completed) return;
        // check if all are now colored
        foreach (var s in spots)
        {
            if (!s.IsColored)
                return; // some still uncolored
        }

        // all colored
        completed = true;
        Debug.Log(" Whole house is colored!");
        if (rewardPanel != null)
            rewardPanel.SetActive(true);
        onAllColored?.Invoke();
    }
}
