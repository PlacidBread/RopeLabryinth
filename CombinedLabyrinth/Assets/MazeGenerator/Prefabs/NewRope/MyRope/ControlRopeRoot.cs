using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControlRopeRoot : MonoBehaviour
{
    public Transform player;
    public Camera camera;

    public float followPointAdjust = 0.001f;

    private Vector3 _followpoint;
    void Awake()
    {
        // Physics.IgnoreCollision(player.parent.GetComponent<Collider>(), GetComponent<Collider>());
        Physics.IgnoreLayerCollision(8, 8);
        transform.position = player.position;

        // AddChildren(transform);
    }

    private void Update()
    {
        UpdateFollowPoint();
    }

    private void FixedUpdate()
    {
        transform.position = _followpoint;
    }

    private void UpdateFollowPoint()
    {
        // var ab = player.position - camera.transform.position;
        // var normalAB = ab.normalized;
        var x = 0.1f;
        Vector3 P = x * Vector3.Normalize(camera.transform.position - player.position) + player.position;
        _followpoint = P;
    }

    private void AddChildren(Transform parent)
    {
        for (int i = 0; i < parent.childCount; i++)
        {
            var child = parent.GetChild(i);
            var representative = new GameObject(child.gameObject.name);
            // representative.transform.parent = RigidBodyContainer.transform;
            // //rigidbody
            // var childRigidbody = representative.gameObject.AddComponent<Rigidbody>();
            // childRigidbody.useGravity = true;
            // childRigidbody.isKinematic = false;
            // childRigidbody.freezeRotation = true;
            // childRigidbody.mass = RigidbodyMass;    
            //
            // //collider
            // var collider = representative.gameObject.AddComponent<SphereCollider>();
            // collider.center = Vector3.zero;
            // collider.radius = ColliderRadius;
            //
            // //DistanceJoint
            // var joint = representative.gameObject.AddComponent<DistanceJoint3D>();
            // joint.ConnectedRigidbody = parent;
            // joint.DetermineDistanceOnStart = true;
            // joint.Spring = JointSpring;
            // joint.Damper = JointDamper;
            // joint.DetermineDistancevb nOnStart = false;
            // joint.Distance = Vector3.Distance(parent.position, child.position);
            //
            // //add copy source
            // CopySource.Add(representative.transform);
            // CopyDestination.Add(child);

            // var joint = representative.GetComponent<HingeJoint>();
            // var rigidBody = parent.GetComponent<Rigidbody>();
            // // try connectedAnchor
            // joint.connectedBody = rigidBody;
            AddChildren(child);
        }
    }
}
