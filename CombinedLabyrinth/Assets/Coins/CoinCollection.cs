using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class CoinCollection : MonoBehaviour
{

    private int coinCount;
    public TextMeshProUGUI coinText;
   private void OnTriggerEnter(Collider collider)
   {
        if (collider.gameObject.CompareTag("Coin"))
        {
            coinCount++;
            CoinTracker.incrementCoinCount();
            coinText.text = "Coins: " + coinCount;
            Destroy(collider.gameObject);
        }

        if (collider.gameObject.CompareTag("Spike"))
        {
            if (coinCount > 0) {
                coinCount--;
                CoinTracker.decrementCoinCount();
                coinText.text = "Coins: " + coinCount;
            }
            Debug.Log("HIT");
        }
   }

  
}
 public static class CoinTracker 
    {
        private static int coinCount = 0;

        public static int getCoinCount()
        {
            return coinCount;
        }

        public static void setCointCount(int setCoins)
        {
            coinCount = setCoins;
        }

        public static void incrementCoinCount()
        {
            coinCount++;
        }

        public static void decrementCoinCount()
        {
            coinCount--;
        }
    }