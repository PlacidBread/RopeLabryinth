using UnityEngine;

namespace MazeGenerator.Scripts
{
    public class ButtonPress : MonoBehaviour
    {
        [SerializeField] private MazeGenerator mazeGenerator;
        
        private void OnTriggerEnter(Collider collider)
        {
            if (collider.gameObject.CompareTag("Button"))
            {
                mazeGenerator.ButtonPressed();
            }
        }
    }
}

