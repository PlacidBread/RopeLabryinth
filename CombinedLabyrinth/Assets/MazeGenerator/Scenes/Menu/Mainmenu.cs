using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Mainmenu : MonoBehaviour
{

    public void PlayGame () {       

        CoinTracker.setCointCount(0);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        
    }

    public void QuitGame () {

        Application.Quit();
        Debug.Log("Quit");

    }
}
