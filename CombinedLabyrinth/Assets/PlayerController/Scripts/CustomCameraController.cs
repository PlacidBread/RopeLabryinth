using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CustomCameraController : MonoBehaviour
{
    [SerializeField] private GameObject player;

    [SerializeField] private int xOffset;
    [SerializeField] private int yOffset;
    [SerializeField] private int zOffset;

    private bool IsActive { get; set; } = false;

    public void Activate()
    {
        IsActive = true;
    }
    private void FixedUpdate()
    {
        if (!IsActive) return;
        var pos = player.transform.position;
        transform.position = new Vector3(pos.x + xOffset, pos.y + yOffset, pos.z + zOffset);
    }
}
