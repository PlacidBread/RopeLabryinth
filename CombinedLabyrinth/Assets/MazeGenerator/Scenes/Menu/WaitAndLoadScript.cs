using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class WaitAndLoadScript : MonoBehaviour
{
    public string nextSceneName;

    void Start()
    {
        StartCoroutine(WaitAndLoadNextScene());
    }

    IEnumerator WaitAndLoadNextScene()
    {
        Debug.Log("Waiting for 10 seconds...");
        yield return new WaitForSeconds(10f);
        Debug.Log("10 seconds have passed! Loading next scene...");
        SceneManager.LoadScene(nextSceneName);
    }
}
