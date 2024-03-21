using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverScreen : MonoBehaviour
{
    GameObject player;
    public TextMeshProUGUI coinText;
    public void Setup() {
        gameObject.SetActive(true);
        
        //Deactivate player
        player = GameObject.FindGameObjectWithTag("Player");
        player.SetActive(false);

        //Enable cursor
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;

        //Display coins
        coinText.text = "COINS COLLECTED: " + CoinTracker.getCoinCount();
        CoinTracker.setCointCount(0);
    }

    public void MenuButton()
    {
        SceneManager.LoadScene("Menu");
    }

    public void RestartButton()
    {
        // SceneManager.UnloadSceneAsync("GeneratorScene");
        StartCoroutine(RestartLoad());
    }
    
    private IEnumerator RestartLoad() 
    {
        yield return new WaitForSeconds(0.1f);
        // Reset the player here
        SceneManager.LoadScene("GeneratorScene");
    }
}
