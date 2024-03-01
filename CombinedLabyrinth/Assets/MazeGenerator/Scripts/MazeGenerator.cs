using System.Collections;
using System.Collections.Generic;
using System.Linq;
using MazeGenerator.Scripts.Enums;
using StarterAssets;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.InputSystem;
using Random = UnityEngine.Random;

namespace MazeGenerator.Scripts
{
    public class MazeGenerator : MonoBehaviour
    {
        [SerializeField] private MazeNode mazeNodePrefab;
        [SerializeField] private MazeNodeExit mazeNodeExitPrefab;
        [SerializeField] private GameObject player;
        // [SerializeField] private Camera cameraController;
        private GameRespawn gameRespawn;
        private Rope rope;

        [SerializeField] private int mazeWidth;
        [SerializeField] private int mazeHeight;
        [SerializeField] private int mazeNodeScale = 2;

        private StarterAssetsInputs _input;
        [SerializeField] private InputActionReference debugInput;
        private MazeNode[,] _mazeNodes;

        private void DebugFunction(InputAction.CallbackContext obj)
        {
            if (!rope.RenderRope) return;
            rope.LogRopePos();
        }

        // Start is called before the first frame update
        void Start()
        {
            _input = GetComponent<StarterAssetsInputs>();
            gameRespawn = player.GetComponent<GameRespawn>();
            if (gameRespawn is null)
            {
                return;
            }
            
            _mazeNodes = new MazeNode[mazeWidth, mazeHeight];

            for (int i = 0; i < mazeWidth; i++)
            {
                for (int j = 0; j < mazeHeight; j++)
                {
                    _mazeNodes[i, j] = Instantiate(mazeNodePrefab, new Vector3(i*mazeNodeScale, 0, j*mazeNodeScale), Quaternion.identity);
                    _mazeNodes[i, j].SetIndex(i, j);
                }
            }

            GenerateMaze(null, _mazeNodes[0, 0]);
            LoopClearDuplicateWalls();
            GenerateExit();

            
            var spawnPos = gameRespawn.SetSpawn(_mazeNodes, mazeWidth, mazeHeight);
        
            rope = player.GetComponent<Rope>();
            rope.StartRenderRope(spawnPos);
            // rope.RenderRope();
            
            SmoothCameraController.Activate();
            ThirdPersonController.CanMove = true;
        
            debugInput.action.performed += DebugFunction;
            debugInput.action.Enable();
        }

        private void GenerateExit()
        {
            ExitSide exitSide = (ExitSide)Random.Range(0, 5);
            int random;
            Vector2Int index = new Vector2Int();
        
            switch (exitSide)
            {
                case ExitSide.Top:
                    random = Random.Range(1, mazeWidth-1);
                    index = new Vector2Int(random, mazeHeight-1);
                    // _mazeNodes[random, mazeHeight-1].ClearFrontWall();
                    // _mazeNodes[random, mazeHeight-1].ClearAll();
                    break;
                case ExitSide.Right:
                    random = Random.Range(1, mazeHeight-1);
                    index = new Vector2Int(mazeWidth-1, random);
                    // _mazeNodes[mazeWidth-1, random].ClearRightWall();
                    // _mazeNodes[mazeWidth-1, random].ClearAll();
                    break;
                case ExitSide.Bottom:
                    random = Random.Range(1, mazeWidth-1);
                    index = new Vector2Int(random, 0);
                    // _mazeNodes[random, 0].ClearBackWall();
                    // _mazeNodes[random, 0].ClearAll();
                    break;
                case ExitSide.Left:
                    random = Random.Range(1, mazeHeight-1);
                    index = new Vector2Int(0, random);
                    // _mazeNodes[0, random].ClearLeftWall();
                    // _mazeNodes[0, random].ClearAll();
                    break;
            }
        
            _mazeNodes[index.x, index.y].ClearAll();
            Instantiate(mazeNodeExitPrefab, new Vector3(index.x * mazeNodeScale, 0, index.y * mazeNodeScale), quaternion.identity);
            // TODO: Generate collider (& Door?) at exit position (var random range...)
        }

        private void GenerateMaze(MazeNode prevNode, MazeNode currNode)
        {
            currNode.Visit();
            ClearWalls(prevNode, currNode);

            // yield return new WaitForSeconds(0.05f);

            MazeNode nextNode;

            do
            {
                nextNode = GetNextUnvisitedNode(currNode);

                if (nextNode != null)
                {
                     GenerateMaze(currNode, nextNode);
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
            // int x = (int)currNode.transform.position.x;
            // int z = (int)currNode.transform.position.z;

            int x = (int)currNode.Index.x;
            int z = (int)currNode.Index.y;
        
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
        }
    }
}
