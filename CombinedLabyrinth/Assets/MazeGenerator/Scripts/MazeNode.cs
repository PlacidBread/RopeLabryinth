using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class MazeNode : MonoBehaviour
{
    [SerializeField] private GameObject leftWall;
    [SerializeField] private GameObject rightWall;
    [SerializeField] private GameObject frontWall;
    [SerializeField] private GameObject backWall;
    [SerializeField] private GameObject torchLW;
    [SerializeField] private GameObject torchRW;
    [SerializeField] private GameObject torchFW;
    [SerializeField] private GameObject torchBW;
    [SerializeField] private GameObject unvisitedBlock;
    [SerializeField] private GameObject coin;
    [SerializeField] private GameObject spike;
    [SerializeField] private GameObject button;

    [SerializeField] private GameObject buttonPressed;
    
    [SerializeField] private GameObject node;
    
    public bool IsVisited { get; private set; }
    [SerializeField] public Vector2Int Index;
    private int chanceOfCoinSpawning = 20;
    private int chanceOfSpikeSpawning = 40;

    public void ButtonPressed()
    {
        var srcPosition = button.transform.position;
        Destroy(button);
        Instantiate(buttonPressed, srcPosition, Quaternion.identity);
    }
    
    public void SetButton()
    {
        button.SetActive(true);
    }
    
    public void SetTorch()
    {
        List<GameObject> activeWalls = new List<GameObject>();
        if (GetActiveLW()) activeWalls.Add(leftWall);
        if (GetActiveRW()) activeWalls.Add(rightWall);
        if (GetActiveFW()) activeWalls.Add(frontWall);
        if (GetActiveBW()) activeWalls.Add(backWall);

        int random = Random.Range(0, activeWalls.Count);
        if (activeWalls[random] == leftWall) torchLW.SetActive(true);
        if (activeWalls[random] == rightWall) torchRW.SetActive(true);
        if (activeWalls[random] == frontWall) torchFW.SetActive(true);
        if (activeWalls[random] == backWall) torchBW.SetActive(true);
    }

    public bool SetCoin()
    {
        int random = Random.Range(1, 100);
        if (random <= chanceOfCoinSpawning)
        {
            coin.SetActive(true); 
            return true;
        }
        return false;
    }

    public bool SetSpike()
    {
        int random = Random.Range(1, 100);
        if (random <= chanceOfSpikeSpawning)
        {
            spike.SetActive(true); 
            return true;
        }
        return false;
    }
    
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

    public void ClearAll()
    {
        node.SetActive(false);
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
