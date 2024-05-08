using System;
using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using StarterAssets;
using Unity.VisualScripting;
using UnityEngine;

public class Water : MonoBehaviour
{
    public float waterIncrement = 0.01f;
    public GameObject player;

    private ThirdPersonController _playerController;
    private float _baseMoveSpeed = 2f;
    private float _baseSprintSpeed = 5.335f;

    private void Start()
    {
        _playerController = player.GetComponent<ThirdPersonController>();
    }

    private void FixedUpdate()
    {
        if (transform.position.y > 1 || _playerController.IsUnityNull())  return;
        transform.position = new Vector3(transform.position.x, transform.position.y + waterIncrement, transform.position.z);
        var yPos = transform.position.y;
        float multiplier = 0f;
        if (yPos == 0)
        {
            multiplier = 1f;
        }
        else
        {
            multiplier = (1 - (yPos/2));
        }

        _playerController.MoveSpeed = _baseMoveSpeed * multiplier;
        _playerController.SprintSpeed = _baseSprintSpeed * multiplier;
    }
    
}
