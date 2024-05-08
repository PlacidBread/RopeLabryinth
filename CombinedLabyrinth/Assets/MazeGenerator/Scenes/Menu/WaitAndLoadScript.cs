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
        
    public static Level ChosenLevel;
    // public void PlayGame () {       
    
    public string nextSceneName;
    
    void Start()
    {
        StartCoroutine(WaitAndLoadNextScene());
        // WaitAndLoadNextScene();
    }

    IEnumerator WaitAndLoadNextScene()
    {
        yield return null;
        
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        AsyncOperation sceneLoader = SceneManager.LoadSceneAsync(currentSceneIndex+1);
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
}
