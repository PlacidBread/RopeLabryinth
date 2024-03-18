using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Timer : MonoBehaviour
{
    public GameOverScreen GameOverScreen;
    [SerializeField] TextMeshProUGUI timerText;
    [SerializeField] float remainingTime;
    public AudioClip gameOverMusic; 
    private AudioSource audioSource; 

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    void Update()
    {
        if (remainingTime > 0)
        {
            remainingTime -= Time.deltaTime;
        }

        if (remainingTime <= 0)
        {
            remainingTime = 0;
            TimerEnd();
        }

        int minutes = Mathf.FloorToInt(remainingTime / 60);
        int seconds = Mathf.FloorToInt(remainingTime % 60);
        timerText.text = string.Format("{0:00}:{1:00}", minutes, seconds);
    }

    void TimerEnd()
    {
        
        if (gameOverMusic != null && audioSource != null)
        {
            audioSource.PlayOneShot(gameOverMusic);
        }

        GameOverScreen.Setup();
    }
}
