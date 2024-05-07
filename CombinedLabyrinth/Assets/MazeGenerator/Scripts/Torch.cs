using System;
using UnityEngine;

namespace MazeGenerator.Scripts
{
    public class Torch : MonoBehaviour
    {
        public GameObject torchLightParicles;
        private GameObject player;

        private void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.CompareTag("Player"))
            {
                torchLightParicles.SetActive(true);
            }
        }
    }
}
