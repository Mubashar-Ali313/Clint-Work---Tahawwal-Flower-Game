using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneButtonLoader : MonoBehaviour
{
    [Tooltip("Exact scene name as in Build Settings")]
    public string sceneName;

    [Tooltip("Optional overlay while loading")]
    public GameObject loadingOverlay;

    private bool isLoading = false;

    public void LoadScene()
    {
        if (isLoading) return;

        if (string.IsNullOrWhiteSpace(sceneName))
        {
            Debug.LogWarning("SceneButtonLoader: sceneName is empty.");
            return;
        }

        // Check if scene is in build settings
        if (!Application.CanStreamedLevelBeLoaded(sceneName))
        {
            Debug.LogWarning($"SceneButtonLoader: Scene '{sceneName}' not found in Build Settings or name is incorrect.");
            return;
        }

        isLoading = true;
        if (loadingOverlay != null)
            loadingOverlay.SetActive(true);

        SceneManager.LoadSceneAsync(sceneName).completed += op =>
        {
            if (loadingOverlay != null)
                loadingOverlay.SetActive(false);
            isLoading = false;
        };
    }
}
