using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameOverScreen : MonoBehaviour
{
    GameObject player;
    public void Setup() {
        gameObject.SetActive(true);
        
        //Deactivate player
        player = GameObject.FindGameObjectWithTag("Player");
        player.SetActive(false);

        //Enable curson
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
    }

    public void RestartButton()
    {
        SceneManager.LoadScene("Menu");
    }
}
