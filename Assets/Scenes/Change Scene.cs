using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections; // Required for IEnumerator

public class ChangeScene : MonoBehaviour
{
    // Duration of the delay in seconds
    public float delay = 0.5f;

    public void changeScene(string sceneTarget)
    {
        StartCoroutine(ChangeSceneWithDelay(sceneTarget));
    }

    private IEnumerator ChangeSceneWithDelay(string sceneTarget)
    {
        // Wait for the specified delay
        yield return new WaitForSeconds(delay);

        // Unload unused assets
        Resources.UnloadUnusedAssets();

        // Force garbage collection
        System.GC.Collect();

        // Load the target scene
        SceneManager.LoadScene(sceneTarget);
    }
}
