using System;
using MazeGenerator.Scripts.Enums;
//using UnityEditor.UIElements;
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

        public float speed = 0.1f;
        public float distanceThreshold = 3.0f;

        private ExitSide _exitSide;
        private bool _doOpenDoor = false;
        
        // Handling door animation
        private Vector3 _initialPosL = Vector3.zero;
        // private Vector3 _initialPosR = Vector3.zero;
        private Vector3 _left = Vector3.zero;
        private Vector3 _right = Vector3.zero;

        private void FixedUpdate()
        {
            if (_doOpenDoor) DecideOpenDoor();
        }

        public void ActivateDoor(ExitSide exitSide)
        {
            _exitSide = exitSide;
            switch (exitSide)
            {
                case ExitSide.Bottom:
                    backDoorL.SetActive(true);
                    backDoorR.SetActive(true);
                    _left = Vector3.back;
                    _right = Vector3.forward;
                    break;
                case ExitSide.Top:
                    frontDoorL.SetActive(true);
                    frontDoorR.SetActive(true);
                    _left = Vector3.forward;
                    _right = Vector3.back;
                    break;
                case ExitSide.Left:
                    leftDoorL.SetActive(true);
                    leftDoorR.SetActive(true);
                    _left = Vector3.forward;
                    _right = Vector3.back;
                    break;
                case ExitSide.Right:
                    rightDoorL.SetActive(true);
                    rightDoorR.SetActive(true);
                    _left = Vector3.back;
                    _right = Vector3.forward;
                    break;
            }
            Debug.Log((ExitSide)exitSide);
        }

        private void DecideOpenDoor()
        {
            switch (_exitSide)
            {
                case ExitSide.Bottom:
                    OpenDoors(backDoorL, backDoorR);
                    break;
                case ExitSide.Top:
                    OpenDoors(frontDoorL, frontDoorR);
                    break;
                case ExitSide.Left:
                    OpenDoors(leftDoorL, leftDoorR);
                    break;
                case ExitSide.Right:
                    OpenDoors(rightDoorL, rightDoorR);
                    break;
            }
        }
        
        
        private void OpenDoors(GameObject left, GameObject right)
        {
            if (_initialPosL == Vector3.zero)
            {
                _initialPosL = left.transform.position;
            }
            
            left.transform.Translate(_left*speed*Time.deltaTime);
            right.transform.Translate(_right*speed*Time.deltaTime);

            var distanceL = Vector3.Distance(left.transform.position, _initialPosL);
            if (distanceL > distanceThreshold)
            {
                _doOpenDoor = false;
                left.SetActive(false);
                right.SetActive(false);
            }
        }

        public void SetDoOpenDoor(bool val)
        {
            _doOpenDoor = val;
        }
    }
}
