using MazeGenerator.Scripts.Enums;
using UnityEngine;

namespace MazeGenerator.Scripts
{
    public class MazeNodeExit : MonoBehaviour
    {
        [SerializeField] private GameObject leftDoorL;
        [SerializeField] private GameObject leftDoorR;
        [SerializeField] private GameObject rightDoorL;
        [SerializeField] private GameObject rightDoorR;
        [SerializeField] private GameObject frontDoorL;
        [SerializeField] private GameObject frontDoorR;
        [SerializeField] private GameObject backDoorL;
        [SerializeField] private GameObject backDoorR;
        // Start is called before the first frame update
        void Start()
        {
        
        }

        // Update is called once per frame
        void Update()
        {
        
        }

        public void DecideDoor(ExitSide exitSide)
        {
            
        }
        

        public void OpenDoors(GameObject left, GameObject right)
        {
        
        }
    }
}
