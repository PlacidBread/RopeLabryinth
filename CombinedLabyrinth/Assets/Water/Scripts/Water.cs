using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Water : MonoBehaviour
{
    public float waterIncrement = 0.01f;
    
    // // Start is called before the first frame update
    // void Start()
    // {
    //     
    // }

    private void FixedUpdate()
    {
        transform.position = new Vector3(transform.position.x, transform.position.y + waterIncrement, transform.position.z);
    }
}
