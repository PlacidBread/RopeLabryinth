using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class WaitAndLoadScript : MonoBehaviour
{
    public enum Level
    {
        Normal,
        Ghost,
        Water
    }
        
    public static Level ChosenLevel = Level.Normal;
    // public string nextSceneName;

    private int _sceneIndex = 1;
    void Start()
    {
        StartCoroutine(WaitAndLoadNextScene());
        // WaitAndLoadNextScene();
    }

    IEnumerator WaitAndLoadNextScene()
    {
        yield return null;

        var nextSceneName = MakeSceneName();
        Debug.Log(nextSceneName);
        
        // int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        // AsyncOperation sceneLoader = SceneManager.LoadSceneAsync(currentSceneIndex+1);
        AsyncOperation sceneLoader = SceneManager.LoadSceneAsync(nextSceneName);
        if (sceneLoader is null)
        {
            Debug.Log("NO NEXT SCENE");
            yield return null;
        }
        else
        {
            sceneLoader.allowSceneActivation = false;
        
            while (sceneLoader.progress < 0.9f) {
                yield return null;
            }
        
            yield return new WaitForSeconds(0.75f);
            Debug.Log("LOADED");
            sceneLoader.allowSceneActivation = true;
            Cursor.visible = false;
        }
    }

    private string MakeSceneName()
    {
        var newSceneName = ChosenLevel.ToString() + _sceneIndex.ToString();
        _sceneIndex++;
        return newSceneName;
    }
}
