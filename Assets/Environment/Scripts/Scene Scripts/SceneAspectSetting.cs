using UnityEngine;

[DisallowMultipleComponent]
public class SceneAspectSetting : MonoBehaviour
{
    [Tooltip("Target Game View resolution for this scene in the Editor (only affects Play Mode in Editor).")]
    public int width = 1920;
    public int height = 1080;

    [Tooltip("If true, the editor will switch Game View to this resolution when Play starts.")]
    public bool applyInEditor = true;




    
}
