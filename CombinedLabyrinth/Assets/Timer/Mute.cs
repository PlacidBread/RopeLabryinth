using UnityEngine;

public class Mute : MonoBehaviour
{
    public AudioSource audioSource;

    private void OnEnable()
    {
        // Check if an audio source is assigned
        if (audioSource != null)
        {
            // Stop playing the audio source when the canvas is enabled
            audioSource.Stop();
        }
        else
        {
            Debug.LogWarning("Audio source is not assigned!");
        }
    }
}
