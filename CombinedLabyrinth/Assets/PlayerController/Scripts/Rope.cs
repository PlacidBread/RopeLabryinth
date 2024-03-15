using System.Collections.Generic;
using UnityEngine;

public class Rope : MonoBehaviour
{
    public Transform player;
    public LineRenderer rope;
    public LayerMask collMask;
    
    // Use private field instead of auto-implemented property
    private List<Vector3> ropePositions = new List<Vector3>();

    // Method to start rendering the rope
    public bool RenderRope { get; private set; } = false;

    public void StartRenderRope(Transform spawnPos)
    {
        RenderRope = true;
        AddPosToRope(spawnPos.position);
        AddPosToRope(spawnPos.position);
    }
    
    private void Update()
    {
        if (!RenderRope) return;
        
        UpdateRopePositions();
        LastSegmentGoToPlayerPos();
    
        DetectCollisionEnter();
        if (ropePositions.Count > 2) DetectCollisionExits();
    }
    // Method to log rope positions
    public void LogRopePos()
    {
        // foreach (var ropePos in ropePositions)
        // {
        //     Debug.Log(ropePos);
        // }
        Debug.Log(ropePositions.Count);
    }
    
    private void DetectCollisionEnter()
    {
        RaycastHit hit;
        if (Physics.Linecast(player.position, rope.GetPosition(ropePositions.Count - 2), out hit, collMask))
        {
            if (ropePositions.Contains(hit.point)) return;
            ropePositions.RemoveAt(ropePositions.Count - 1);
            AddPosToRope(hit.point);
        }
    }
    
    private void DetectCollisionExits()
    {
        RaycastHit hit;
        if (!Physics.Linecast(player.position, rope.GetPosition(ropePositions.Count - 3), out hit, collMask))
        {
            ropePositions.RemoveAt(ropePositions.Count - 2);
        }
    }
    
    private void AddPosToRope(Vector3 _pos)
    {
        ropePositions.Add(_pos);
        ropePositions.Add(player.position); // Always the last pos must be the player
    }
    
    private void UpdateRopePositions()
    {
        rope.positionCount = ropePositions.Count;
        rope.SetPositions(ropePositions.ToArray());
    }

    private void LastSegmentGoToPlayerPos()
    {
        rope.SetPosition(rope.positionCount - 1, player.position);
    }
}