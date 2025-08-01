using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    [Header("List of scene names in order (mode 1 = index 0, mode 2 = index 1, etc.)")]
    public string[] modeSceneNames;

    [Header("Optional: UI or overlay to show during load")]
    public GameObject loadingOverlay;

    private bool isLoading = false;

    /// <summary>
    /// Call this from a button, passing 1-based mode number (e.g., 1..5)
    /// </summary>
    public void LoadModeByNumber(int modeNumber)
    {
        int idx = modeNumber - 1;
        LoadModeByIndex(idx);
    }

    /// <summary>
    /// Loads the mode at the given zero-based index.
    /// </summary>
    public void LoadModeByIndex(int index)
    {
        if (isLoading)
            return;

        if (index < 0 || index >= modeSceneNames.Length)
        {
            Debug.LogWarning($"ModeSceneLoader: invalid mode index {index}");
            return;
        }

        string sceneName = modeSceneNames[index];
        StartCoroutine(LoadSceneAsync(sceneName));
    }

    private IEnumerator LoadSceneAsync(string sceneName)
    {
        isLoading = true;
        if (loadingOverlay != null)
            loadingOverlay.SetActive(true);

        AsyncOperation op = SceneManager.LoadSceneAsync(sceneName);
        op.allowSceneActivation = true;

        while (!op.isDone)
        {
            // could expose progress via op.progress (note: goes to ~0.9 before activation)
            yield return null;
        }

        if (loadingOverlay != null)
            loadingOverlay.SetActive(false);

        isLoading = false;
    }
}
