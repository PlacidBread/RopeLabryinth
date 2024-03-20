using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Rope : MonoBehaviour
{
    public Transform player;
    public LineRenderer rope;
    public LayerMask collMask;

    private float _ropeLength;
    private float _maxRopeLength;
    
    [SerializeField] private Material materialBasic;
    [SerializeField] private Material materialStretched;
    [SerializeField] private Material materialSuperStretched;

    
    // Use private field instead of auto-implemented property
    private List<Vector3> ropePositions = new List<Vector3>();

    // Method to start rendering the rope
    public bool RenderRope { get; private set; } = false;

    public void StartRenderRope(Transform spawnPos)
    {
        RenderRope = true;
        AddPosToRope(spawnPos.position);
        // AddPosToRope(spawnPos.position);
    }
    
    private void Update()
    {
        if (!RenderRope) return;
        
        UpdateRopePositions();
        LastSegmentGoToPlayerPos();
    
        DetectCollisionEnter();
        if (ropePositions.Count > 2) DetectCollisionExits();
        
        CheckRopeLength();
    }

    private void CheckRopeLength()
    {
        if (ropePositions.Count < 2) return;
        
        _ropeLength = 0.0f;
        Vector3 prevPoint = ropePositions[0];
        foreach (var ropePoint in ropePositions)
        {
            _ropeLength += Vector3.Distance(prevPoint, ropePoint);
            prevPoint = ropePoint;
        }
        
        // Debug.Log(_ropeLength);
        // turn red at 75% of max, bright red at 90%
        if (_ropeLength > (_maxRopeLength * 0.9))
        {
            rope.material = materialSuperStretched;
        }
        else if (_ropeLength > (_maxRopeLength * 0.75))
        {
            rope.material = materialStretched;
        } 
        else if (_ropeLength > _maxRopeLength)
        {
            // TODO: break rope - GAME OVER
        }
        else
        {
            rope.material = materialBasic;
        }
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
            // if (ropePositions.Contains(hit.point)) return;
            if (ContainsSimilar(hit.point)) return;
            ropePositions.RemoveAt(ropePositions.Count - 1); // remove player pos temporarily
            AddPosToRope(hit.point);
        }
    }
    
    // need changing?
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

    private bool ContainsSimilar(Vector3 newPos)
    {
        foreach (var pos in ropePositions)
        {
            Vector3 diff = new Vector3(Math.Abs(pos.x - newPos.x), Math.Abs(pos.y - newPos.y),
                Math.Abs(pos.z - newPos.z));
            if (diff.x <= 0.5 && diff.y <= 0.5 && diff.z <= 0.5)
            {
                return true;
            }
        }

        return false;
    }

    public void SetMaxRopeLength(float ropeLength)
    {
        _maxRopeLength = ropeLength;
    }
}