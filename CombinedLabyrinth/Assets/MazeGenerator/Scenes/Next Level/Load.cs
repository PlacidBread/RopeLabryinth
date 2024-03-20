using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class Load : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        // Start the coroutine to wait and load the next scene
        StartCoroutine(LoadNextSceneAfterDelay(5f));
    }

    IEnumerator LoadNextSceneAfterDelay(float delay)
    {
        // Wait for the specified delay
        yield return new WaitForSeconds(delay);

        // Get the current scene index
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;

        // Load the next scene by incrementing the current scene index
        SceneManager.LoadScene(currentSceneIndex + 1);
    }
}