using System.Collections;
using System.Collections.Generic;
using MazeGenerator.Scripts;
using UnityEngine;

namespace MazeGenerator.Scripts
{
    public class EndReached : MonoBehaviour
    {
        [SerializeField] private MazeGenerator mazeGenerator;
        
        private void OnTriggerEnter(Collider collider)
        {
            if (collider.gameObject.CompareTag("Finish"))
            {
                mazeGenerator.EndReached();
            }
        }
    }
}