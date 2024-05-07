using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightF : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }
    public GameObject thePlayer;
    // Update is called once per frame
    void Update()
    {
        transform.LookAt(thePlayer.transform.position);
        //transform.position = thePlayer.transform.position;
    }
}
