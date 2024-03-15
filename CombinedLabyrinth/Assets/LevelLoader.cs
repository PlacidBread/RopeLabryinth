using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LevelLoader : MonoBehaviour {
    public GameObject loadingScreen;
    public Slider slider;

    public void LoadLevel (int sceneIndex)
    {
        StartCoroutine(LoadAsynchronously(sceneIndex));
    }

    IEnumerator LoadAsynchronously(int sceneIndex)
    {
        AsyncOperation operation = SceneManager.LoadSceneAsync(sceneIndex);

        loadingScreen.SetActive(true);
        slider.value = 0; 
        yield return new WaitForSeconds(0.5f);
        Debug.Log("Progress updated to 0.5");
        slider.value = 1;
        yield return new WaitForSeconds(0.05f);
        Debug.Log("Progress updated to 0.75");
        slider.value = 0.75f;

        Debug.Log("Loading started...");

        yield return new WaitForSeconds(0.5f);
        Debug.Log("Progress updated to 0.5");
        slider.value = 0.5f;
        yield return new WaitForSeconds(0.05f);
        Debug.Log("Progress updated to 0.75");
        slider.value = 0.75f;
        yield return null;

        Debug.Log("Loading complete!");
    }
}

