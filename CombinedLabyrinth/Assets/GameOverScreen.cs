using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameOverScreen : MonoBehaviour
{
    GameObject player;
    public TextMeshProUGUI coinText;
    public void Setup() {
        gameObject.SetActive(true);
        
        //Deactivate player
        player = GameObject.FindGameObjectWithTag("Player");
        player.SetActive(false);

        //Enable curson
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;

        coinText.text = "COINS COLLECTED: " + CoinTracker.getCoinCount();
        CoinTracker.setCointCount(0);
    }

    public void MenuButton()
    {
        SceneManager.LoadScene("Menu");
    }

    public void RestartButton()
    {
        SceneManager.LoadScene("GeneratorScene");
    }
}
