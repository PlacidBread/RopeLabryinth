using System.Collections;
using System.Collections.Generic;
using MazeGenerator.Scripts;
using UnityEngine;

namespace MazeGenerator.Scripts
{
    public class ButtonPress : MonoBehaviour
    {
        [SerializeField] private MazeGenerator mazeGenerator;

        // private MazeGenerator _mazeGeneratorScript;
        // Start is called before the first frame update
        void Start()
        {
            // _mazeGeneratorScript = mazeGenerator.GetComponent<MazeGenerator.Scripts.MazeGenerator>();
        }

        // Update is called once per frame
        void Update()
        {
        
        }

        private void OnTriggerEnter(Collider collider)
        {
            if (collider.gameObject.CompareTag("Button"))
            {
                mazeGenerator.ButtonPressed();
                // _mazeGeneratorScript.ButtonPressed();
            }
        }
    }
}

