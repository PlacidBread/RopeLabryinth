using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerTorch : MonoBehaviour
{
    public Transform playerHand;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        // Transform tempTransform = this.gameObject.GetComponent<Transform>();
        // tempTransform.position = playerHand.position + /*assign a public Vector3 variable so that you can change the offset of the hand item from the player's hand*/;
        // tempTransform.rotation = playerHand.rotation +/*insert a public Quaternion variable to create an offset like the line of code above*/;
    }
}
