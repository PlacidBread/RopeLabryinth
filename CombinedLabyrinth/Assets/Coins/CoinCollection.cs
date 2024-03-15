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
            coinText.text = "Coins: " + coinCount;
            Destroy(collider.gameObject);
        }
   }
}
