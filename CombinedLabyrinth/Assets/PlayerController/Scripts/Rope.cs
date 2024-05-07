using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Rope : MonoBehaviour
{
    public Transform player;
    public LineRenderer rope;
    public LayerMask collMask;
    public GameOverScreen gameOverScreen;
    public AudioClip ropeSnapSound;

    private float _ropeLength;
    private float _maxRopeLength;
    
    [SerializeField] private Material materialBasic;
    [SerializeField] private Material materialStretched;
    [SerializeField] private Material materialSuperStretched;
    public TextMeshProUGUI deathText;

    
    // Use private field instead of auto-implemented property
    private List<Vector3> _ropePositions = new List<Vector3>();
    private bool _breakRopeParts;
    private AudioSource _audioSource;

    // Method to start rendering the rope
    public bool RenderRope { get; private set; } = false;

    public void StartRenderRope(Transform spawnPos)
    {
        RenderRope = true;
        AddPosToRope(spawnPos.position);
    }

    public void StopRendering()
    {
        RenderRope = false;
        // Additional cleanup or logic if needed
    }

    private void Start()
    {
        _audioSource = GetComponent<AudioSource>();
    }

    private void Update()
    {
        if (_breakRopeParts) BreakRopeParts();
        if (!RenderRope) return;
        
        UpdateRopePositions();
        LastSegmentGoToPlayerPos();
    
        DetectCollisionEnter();
        if (_ropePositions.Count > 2) DetectCollisionExits();
        
        CheckRopeLength();
    }
    
    private void BreakRopeParts()
    {
        var newParts = new List<Vector3>();
        foreach (var ropePos in _ropePositions)
        {   
            newParts.Add(Vector3.MoveTowards(ropePos, player.position, 0.1f));
        }

        _ropePositions = newParts;
        UpdateRopePositions();
    }
    
    private void CheckRopeLength()
    {
        if (_ropePositions.Count < 2) return;
        
        _ropeLength = 0.0f;
        Vector3 prevPoint = _ropePositions[0];
        foreach (var ropePoint in _ropePositions)
        {
            _ropeLength += Vector3.Distance(prevPoint, ropePoint);
            prevPoint = ropePoint;
        }
        
        // Debug.Log(_ropeLength);
        // turn red at 65% of max, bright red at 85%
        if (_ropeLength > _maxRopeLength)
        {
            deathText.text = "ROPE SNAPPED";

            _breakRopeParts = true;
            RenderRope = false;
            rope.material = materialBasic;
            _audioSource.PlayOneShot(ropeSnapSound);

            StartCoroutine(WaitGameOver());
        }
        else if (_ropeLength > (_maxRopeLength * 0.85))
        {
            rope.material = materialSuperStretched;
        }
        else if (_ropeLength > (_maxRopeLength * 0.65))
        {
            rope.material = materialStretched;
        } 
        else
        {
            rope.material = materialBasic;
        }
    }

    private IEnumerator WaitGameOver()
    {
        yield return new WaitForSeconds(1.0f);
        gameOverScreen.Setup();
    }
    // Method to log rope positions
    public void LogRopePos()
    {
        // foreach (var ropePos in ropePositions)
        // {
        //     Debug.Log(ropePos);
        // }
        Debug.Log(_ropePositions.Count);
    }
    
    private void DetectCollisionEnter()
    {
        RaycastHit hit;
        if (Physics.Linecast(player.position, rope.GetPosition(_ropePositions.Count - 2), out hit, collMask))
        {
            if (ContainsSimilar(hit.point)) return;
            if (hit.collider.CompareTag("Spike") || hit.collider.CompareTag("Coin") || hit.collider.CompareTag("Torch") || hit.collider.CompareTag("Player")) return;
            _ropePositions.RemoveAt(_ropePositions.Count - 1); // remove player pos temporarily
            AddPosToRope(hit.point);
        }
    }
    
    private void DetectCollisionExits()
    {
        RaycastHit hit;
        if (Vector3.Distance(player.position, rope.GetPosition(_ropePositions.Count - 3)) <= 2f)
        {
            _ropePositions.RemoveAt(_ropePositions.Count - 2);
        }
    }

    private void AddPosToRope(Vector3 _pos)
    {
        _ropePositions.Add(_pos);
        _ropePositions.Add(player.position); // Always the last pos must be the player
    }
    
    private void UpdateRopePositions()
    {
        rope.positionCount = _ropePositions.Count;
        rope.SetPositions(_ropePositions.ToArray());
    }

    private void LastSegmentGoToPlayerPos()
    {
        rope.SetPosition(rope.positionCount - 1, player.position);
    }

    private bool ContainsSimilar(Vector3 newPos)
    {
        foreach (var pos in _ropePositions)
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

    public void ClearOldRope()
    {
        _ropePositions.Clear();
        UpdateRopePositions();
    }

    public float GetRopeLength()
    {
        return _ropeLength;
    }
}