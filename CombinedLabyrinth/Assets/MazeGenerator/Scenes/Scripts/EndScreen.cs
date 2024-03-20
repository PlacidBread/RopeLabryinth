using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EndScreen : MonoBehaviour
{

    public TextMeshProUGUI coinText;
    // Start is called before the first frame update
    void Start()
    {
        coinText.text = "COINS COLLECTED: " + CoinTracker.getCoinCount();
        
        //Enable curson
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
    }

    public void MenuButton()
    {
        SceneManager.LoadScene("Menu");
    }
}
