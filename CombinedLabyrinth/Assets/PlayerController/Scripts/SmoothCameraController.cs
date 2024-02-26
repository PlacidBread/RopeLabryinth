using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SmoothCameraController : MonoBehaviour
{
    public Transform target;
    public float smoothSpeed = 0.125f;
    public Vector3 locationOffset;
    public Vector3 rotationOffset;

    private static bool _isActive = false;

    public static void Activate()
    {
        _isActive = true;
    }

    private void Start()
    {
        transform.position = new Vector3(9, 32.5f, 2.5f);
        transform.rotation = Quaternion.LookRotation(new Vector3(90f, 0, 0), new Vector3(0, 90, 0));
    }

    void FixedUpdate()
    {
        if (!_isActive) return;
        Vector3 desiredPosition = target.position + target.rotation * locationOffset;
        Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed);
        transform.position = smoothedPosition;

        Quaternion desiredRotation = target.rotation * Quaternion.Euler(rotationOffset);
        Quaternion smoothedRotation = Quaternion.Lerp(transform.rotation, desiredRotation, smoothSpeed);
        transform.rotation = smoothedRotation;
    }
}
