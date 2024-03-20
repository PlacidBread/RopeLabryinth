using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PlayerCollisions : MonoBehaviour
{
    private int coinCount;
    public TextMeshProUGUI coinText;
    public TextMeshProUGUI deathText;
    public AudioClip coinCollectSound;
    private AudioSource audioSource;
    public GameOverScreen GameOverScreen;
    [SerializeField] float invincibilityTimer;
    private bool invincible;

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
        invincible = true;
        coinCount = CoinTracker.getCoinCount();
        coinText.text = "Coins: " + coinCount;
    }

    void Update()
    {
        if (invincibilityTimer > 0)
        {
            invincibilityTimer -= Time.deltaTime;
        }

        if (invincibilityTimer <= 0)
        {
            invincibilityTimer = 0;
            invincible = false;
        }
    }

    private void OnTriggerEnter(Collider collider)
    {
        if (collider.gameObject.CompareTag("Coin") && !invincible)
        {
            coinCount++;
            CoinTracker.setCointCount(coinCount);
            coinText.text = "Coins: " + coinCount;
            Destroy(collider.gameObject);


            if (coinCollectSound != null && audioSource != null)
            {
                audioSource.PlayOneShot(coinCollectSound);
            }
        }

        if (collider.gameObject.CompareTag("Spike"))
        {

            if (coinCount <= 0 && !invincible)
            {   
                deathText.text = "HIT SPIKE";
                GameOverScreen.Setup();
                Debug.Log("GAMEOVER");
            }

            if (coinCount > 0)
            {
                coinCount--;
                CoinTracker.setCointCount(coinCount);
                coinText.text = "Coins: " + coinCount;
                Debug.Log("HIT");


            } 
            
        }
    }
}

// public static class CoinTracker
// {
//     private static int coinCount = 0;

//     public static int getCoinCount()
//     {
//         return coinCount;
//     }

//     public static void setCointCount(int setCoins)
//     {
//         coinCount = setCoins;
//     }

//     public static void incrementCoinCount()
//     {
//         coinCount++;
//     }

//     public static void decrementCoinCount()
//     {
//         coinCount--;
//     }
// }
