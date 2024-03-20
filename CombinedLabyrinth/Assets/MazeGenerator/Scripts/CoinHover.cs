using System;
using UnityEngine;
using Random = UnityEngine.Random;

namespace MazeGenerator.Scripts
{
    public class CoinHover : MonoBehaviour
    {
        [SerializeField] private float bounds = 0.5f;
        [SerializeField] private float jump = -0.008f;

        private float count;
        // Update is called once per frame
        void FixedUpdate()
        {
            transform.position = new Vector3(transform.position.x, transform.position.y + jump + Random.Range(0,10)/10000f, transform.position.z);
            count += jump;
            if (Mathf.Abs(count) >= bounds)
            {
                count = 0;
                jump *= -1;
            }
        }
    }
}
