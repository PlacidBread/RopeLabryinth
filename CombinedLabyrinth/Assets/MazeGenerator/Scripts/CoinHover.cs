using UnityEngine;
using Random = UnityEngine.Random;

namespace MazeGenerator.Scripts
{
    public class CoinHover : MonoBehaviour
    {
        [SerializeField] private float bounds = 0.5f;
        [SerializeField] private float jump = -0.008f;

        private float _count;
        // Update is called once per frame
        void FixedUpdate()
        {
            // add y offset with random float
            transform.position = new Vector3(transform.position.x, transform.position.y + jump, transform.position.z);
            _count += jump;
            if (Mathf.Abs(_count) >= bounds)
            {
                _count = 0;
                jump *= -1;
            }
        }
    }
}
