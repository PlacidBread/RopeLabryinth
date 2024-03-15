using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameOverScreen : MonoBehaviour
{
    GameObject player;
    public void Setup() {
        gameObject.SetActive(true);
        player = GameObject.FindGameObjectWithTag("Player");
        player.SetActive(false);
    }
}
