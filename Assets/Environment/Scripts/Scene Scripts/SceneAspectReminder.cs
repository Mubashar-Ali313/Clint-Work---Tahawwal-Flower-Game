using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(SceneAspectSetting))]
public class SceneAspectReminder : MonoBehaviour
{
    private SceneAspectSetting setting;
    private float targetAspect;
    private const float tolerance = 0.05f; // 5% difference allowed

    private string message;
    private bool showWarning;

    void Awake()
    {
        setting = GetComponent<SceneAspectSetting>();
        targetAspect = (float)setting.width / setting.height;
    }

    void Update()
    {
        float currentAspect = (float)Screen.width / Screen.height;
        showWarning = setting.applyInEditor && Application.isEditor && Mathf.Abs(currentAspect - targetAspect) > tolerance;

        if (showWarning)
        {
            message = $"[Reminder] Game View aspect is ~{currentAspect:F2}. Please switch to {setting.width}x{setting.height} in the Game View dropdown (top-left) or use Device Simulator.";
        }
    }

    void OnGUI()
    {
        if (!showWarning) return;

        GUI.color = Color.yellow;
        GUI.backgroundColor = new Color(0, 0, 0, 0.6f);
        GUIStyle style = new GUIStyle(GUI.skin.box);
        style.fontSize = 14;
        style.normal.textColor = Color.white;
        style.wordWrap = true;
        GUILayout.BeginArea(new Rect(10, 10, 500, 60));
        GUILayout.Box(message, style);
        GUILayout.EndArea();
    }
}
