using UnityEngine;

public class GameRespawn : MonoBehaviour
{
    private Transform spawnPoint { get; set; }

    // [SerializeField] private CharacterController characterController; 
    private MazeGenerator.Scripts.MazeGenerator _mazeGenerator; 

    [SerializeField] private int PaddingDepth;
    private void Start() 
    { 
        // characterController = GetComponent<CharacterController>(); 
        _mazeGenerator = GetComponent<MazeGenerator.Scripts.MazeGenerator>();
    } 
    //
    // private void Update() 
    // { 
    //     if (characterController.isGrounded && transform.position.y < -10) 
    //     { 
    //         Respawn(); 
    //     } 
    // } 
    public Transform SetSpawn(MazeNode[,] mazeNodes, int mazeWidth, int mazeHeight)
    {
        int x = Random.Range(0 + PaddingDepth, (mazeWidth-1) - PaddingDepth);
        int y = Random.Range(0 + PaddingDepth, (mazeHeight-1) - PaddingDepth);
        spawnPoint = mazeNodes[x, y].transform;
        
        Respawn();
        return spawnPoint;
    }
    
    private void Respawn() 
    { 
        transform.position = spawnPoint.position; 
    } 
}
