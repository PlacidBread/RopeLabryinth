using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MazeNode : MonoBehaviour
{
    [SerializeField]
    private GameObject leftWall;
    [SerializeField]
    private GameObject rightWall;
    [SerializeField]
    private GameObject frontWall;
    [SerializeField]
    private GameObject backWall;
    [SerializeField]
    private GameObject unvisitedBlock;
    
    public bool IsVisited { get; private set; }
    [SerializeField] public Vector2Int Index;

    public void SetIndex(int i, int j)
    {
        Index = new Vector2Int(i, j);
    }
    public void Visit()
    {
        IsVisited = true;
        unvisitedBlock.SetActive(false);
    }
    public void ClearLeftWall()
    {
        leftWall.SetActive(false);
    }
    public void ClearRightWall()
    {
        rightWall.SetActive(false);
    }
    public void ClearFrontWall()
    {
        frontWall.SetActive(false);
    }
    public void ClearBackWall()
    {
        backWall.SetActive(false);
    }
    
    public bool GetActiveLW()
    {
        return leftWall.activeSelf;
    }
    public bool GetActiveRW()
    {
        return rightWall.activeSelf;
    }
    public bool GetActiveFW()
    {
        return frontWall.activeSelf;
    }
    public bool GetActiveBW()
    {
        return backWall.activeSelf;
    }
}
