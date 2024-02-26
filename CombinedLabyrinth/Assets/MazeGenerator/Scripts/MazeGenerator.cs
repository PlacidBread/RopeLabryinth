using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
    
public class MazeGenerator : MonoBehaviour
{
    [SerializeField] private MazeNode mazeNodePrefab;
    [SerializeField] private GameObject player;
    private GameRespawn gameRespawn;
    private Rope rope;

    [SerializeField] private int mazeWidth;

    [SerializeField] private int mazeHeight;

    private MazeNode[,] _mazeNodes;
    
    private enum ExitSide
    {
        Top,
        Right,
        Bottom,
        Left
    }
    
    // Start is called before the first frame update
    IEnumerator Start()
    {
        _mazeNodes = new MazeNode[mazeWidth, mazeHeight];

        for (int i = 0; i < mazeWidth; i++)
        {
            for (int j = 0; j < mazeHeight; j++)
            {
                _mazeNodes[i, j] = Instantiate(mazeNodePrefab, new Vector3(i, 0, j), Quaternion.identity);
                _mazeNodes[i, j].SetIndex(i, j);
            }
        }

        yield return GenerateMaze(null, _mazeNodes[0, 0]);
        LoopClearDuplicateWalls();
        GenerateExit();
        
        gameRespawn = player.GetComponent<GameRespawn>();
        var spawnPos = gameRespawn.SetSpawn(_mazeNodes, mazeWidth, mazeHeight);
        
        rope = player.GetComponent<Rope>();
        rope.StartRenderRope(spawnPos);
    }

    private void GenerateExit()
    {
        ExitSide exitSide = (ExitSide)Random.Range(0, 5);
        
        switch (exitSide)
        {
            case ExitSide.Top:
                _mazeNodes[Random.Range(0, mazeWidth), mazeHeight-1].ClearFrontWall();
                break;
            case ExitSide.Right:
                _mazeNodes[mazeWidth-1, Random.Range(0, mazeHeight)].ClearRightWall();
                break;
            case ExitSide.Bottom:
                _mazeNodes[Random.Range(0, mazeWidth), 0].ClearBackWall();
                break;
            case ExitSide.Left:
                _mazeNodes[0, Random.Range(0, mazeHeight + 1)].ClearLeftWall();
                break;
        }
        // TODO: Generate collider (& Door?) at exit position (var random range...)
    }

    private IEnumerator GenerateMaze(MazeNode prevNode, MazeNode currNode)
    {
        currNode.Visit();
        ClearWalls(prevNode, currNode);

        yield return new WaitForSeconds(0.05f);

        MazeNode nextNode;

        do
        {
            nextNode = GetNextUnvisitedNode(currNode);

            if (nextNode != null)
            {
                yield return GenerateMaze(currNode, nextNode);
            }
        } while (nextNode != null);
    }

    private MazeNode GetNextUnvisitedNode(MazeNode currNode)
    {
        var unvisitedNodes = GetUnvisitedNodes(currNode);

        return unvisitedNodes.OrderBy(_ => Random.Range(1, 10)).FirstOrDefault();
    }

    // check surrounding cells
    private IEnumerable<MazeNode> GetUnvisitedNodes(MazeNode currNode)
    {
        int x = (int)currNode.transform.position.x;
        int z = (int)currNode.transform.position.z;

        if (x + 1 < mazeWidth)
        {
            var cellToRight = _mazeNodes[x + 1, z];

            if (!cellToRight.IsVisited)
            {
                yield return cellToRight;
            }
        }

        if (x - 1 >= 0)
        {
            var cellToLeft = _mazeNodes[x - 1, z];

            if (!cellToLeft.IsVisited)
            {
                yield return cellToLeft;
            }
        }
        
        if (z + 1 < mazeHeight)
        {
            var cellToFront = _mazeNodes[x, z + 1];

            if (!cellToFront.IsVisited)
            {
                yield return cellToFront;
            }
        }
        
        if (z - 1 >= 0)
        {
            var cellToBack = _mazeNodes[x, z - 1];

            if (!cellToBack.IsVisited)
            {
                yield return cellToBack;
            }
        } 
    }

    // private void ClearDuplicateWalls(bool isX, MazeNode currNode)
    // {
    //     var nodeIndex = currNode.Index;
    //     if (isX)
    //     {
    //         if (nodeIndex.y > 0 && nodeIndex.y < mazeHeight - 1)
    //         {
    //             if (_mazeNodes[nodeIndex.x, nodeIndex.y + 1].GetActiveFW())
    //             {
    //                 currNode.ClearBackWall();
    //             }
    //             if (_mazeNodes[nodeIndex.x, nodeIndex.y - 1].GetActiveBW())
    //             {
    //                 currNode.ClearFrontWall();
    //             }
    //         }
    //     }
    //     else
    //     {
    //         if (nodeIndex.x > 0 && nodeIndex.x < mazeWidth - 1)
    //         {
    //             if (_mazeNodes[nodeIndex.x + 1, nodeIndex.y].GetActiveLW())
    //             {
    //                 currNode.ClearRightWall();
    //             }
    //             if (_mazeNodes[nodeIndex.x - 1, nodeIndex.y].GetActiveRW())
    //             {
    //                 currNode.ClearLeftWall();
    //             }
    //         }
    //     }
    // }
    
    private void LoopClearDuplicateWalls()
    {
        for (int i = 1; i < mazeWidth - 1; i++)
        {
            for (int j = 1; j < mazeHeight - 1; j++)
            {
                if (_mazeNodes[i + 1, j].GetActiveLW())
                {
                    _mazeNodes[i, j].ClearRightWall();
                }
                if (_mazeNodes[i - 1, j].GetActiveRW())
                {
                    _mazeNodes[i, j].ClearLeftWall();
                }
                if (_mazeNodes[i, j + 1].GetActiveFW())
                {
                    _mazeNodes[i, j].ClearBackWall();
                }
                if (_mazeNodes[i, j - 1].GetActiveBW())
                {
                    _mazeNodes[i, j].ClearFrontWall();
                }
            }
        }
    }
    private void ClearWalls(MazeNode prevNode, MazeNode currNode)
    {
        // check start node
        if (prevNode == null) return;
        
        Vector3 direction = (currNode.transform.position - prevNode.transform.position).normalized;
        Vector3Int prevPosition = Vector3Int.RoundToInt(prevNode.transform.position);
        Vector3Int currPosition = Vector3Int.RoundToInt(currNode.transform.position);
        
        // Calculate the position difference between the nodes
        Vector3Int diff = currPosition - prevPosition;
        
        if (prevNode.transform.position.x < currNode.transform.position.x)
        {
            prevNode.ClearRightWall();
            currNode.ClearLeftWall();
            // ClearDuplicateWalls(true, currNode);
            return;
        }
        if (prevNode.transform.position.x > currNode.transform.position.x)
        {
            prevNode.ClearLeftWall();
            currNode.ClearRightWall();
            // ClearDuplicateWalls(true, currNode);
            return;
        }
        if (prevNode.transform.position.z < currNode.transform.position.z)
        {
            prevNode.ClearFrontWall();
            currNode.ClearBackWall();
            // ClearDuplicateWalls(false, currNode);
            return;
        }
        if (prevNode.transform.position.z > currNode.transform.position.z)
        {
            prevNode.ClearBackWall();
            currNode.ClearFrontWall();
            // ClearDuplicateWalls(false, currNode);
            return;
        }
        // Clear walls based on direction
        // if (direction.x > 0)
        // {
        //     prevNode.ClearRightWall();
        //     currNode.ClearLeftWall();
        //     Debug.Log("Clearing right wall of previous node and left wall of current node");
        // }
        // else if (direction.x < 0)
        // {
        //     prevNode.ClearLeftWall();
        //     currNode.ClearRightWall();
        //     Debug.Log("Clearing left wall of previous node and right wall of current node");
        // }
        // else if (direction.z > 0)
        // {
        //     prevNode.ClearFrontWall();
        //     currNode.ClearBackWall();
        //     Debug.Log("Clearing front wall of previous node and back wall of current node");
        // }
        // else if (direction.z < 0)
        // {
        //     prevNode.ClearBackWall();
        //     currNode.ClearFrontWall();
        //     Debug.Log("Clearing back wall of previous node and front wall of current node");
        // }
        
        // Check if the neighboring nodes have been visited
        // if (Mathf.Abs(diff.x) > 0)
        // {
        //     if (_mazeNodes[prevPosition.x + Mathf.RoundToInt(direction.x), prevPosition.z].IsVisited)
        //     {
        //         // Clear shared wall
        //         currNode.ClearLeftWall();
        //         Debug.Log("Clearing left wall of current node");
        //     }
        //     if (_mazeNodes[currPosition.x - Mathf.RoundToInt(direction.x), currPosition.z].IsVisited)
        //     {
        //         // Clear shared wall
        //         prevNode.ClearRightWall();
        //         Debug.Log("Clearing right wall of previous node");
        //     }
        // }
        // else if (Mathf.Abs(diff.z) > 0)
        // {
        //     if (_mazeNodes[prevPosition.x, prevPosition.z + Mathf.RoundToInt(direction.z)].IsVisited)
        //     {
        //         // Clear shared wall
        //         currNode.ClearBackWall();
        //         Debug.Log("Clearing back wall of current node");
        //     }
        //     if (_mazeNodes[currPosition.x, currPosition.z - Mathf.RoundToInt(direction.z)].IsVisited)
        //     {
        //         // Clear shared wall
        //         prevNode.ClearFrontWall();
        //         Debug.Log("Clearing front wall of previous node");
        //     }
        // }
        
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
