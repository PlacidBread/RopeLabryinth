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
        Debug.Log("Waiting for 5 seconds...");
        yield return new WaitForSeconds(5f);
        Debug.Log("5 seconds have passed! Loading next scene...");
        SceneManager.LoadScene(nextSceneName);
    }
}
