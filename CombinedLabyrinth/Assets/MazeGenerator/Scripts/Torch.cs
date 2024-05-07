using System;
using UnityEngine;
using Random = UnityEngine.Random;

namespace MazeGenerator.Scripts
{
    public class Torch : MonoBehaviour
    {
        public GameObject torchLightParticles;
        public ParticleSystem flames;
        public ParticleSystem secondaryFlames;
        public ParticleSystem lights;
        private GameObject player;

        private void Start()
        {
            var colour = Random.ColorHSV(0f, 1f, 1f, 1f, 1f, 1f);
            var flamesMain = flames.main;
            var secondaryFlamesMain = secondaryFlames.main;
            var lightsMain = lights.main;
            
            flamesMain.startColor = colour;
            secondaryFlamesMain.startColor = colour;
            lightsMain.startColor = colour;
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.CompareTag("Player"))
            {
                torchLightParticles.SetActive(true);
            }
        }
    }
}
