using UnityEngine;

public class GameRespawn : MonoBehaviour
{
    private Transform spawnPoint { get; set; }

    // [SerializeField] private CharacterController characterController; 
    private MazeGenerator _mazeGenerator; 

    [SerializeField] private int PaddingDepth;
    private void Start() 
    { 
        // characterController = GetComponent<CharacterController>(); 
        _mazeGenerator = GetComponent<MazeGenerator>();
    } 
    //
    // private void Update() 
    // { 
    //     if (characterController.isGrounded && transform.position.y < -10) 
    //     { 
    //         Respawn(); 
    //     } 
    // } 
    public Vector2Int SetSpawn(MazeNode[,] mazeNodes, int mazeWidth, int mazeHeight)
    {
        int x = Random.Range(0 + PaddingDepth, (mazeWidth-1) - PaddingDepth);
        int y = Random.Range(0 + PaddingDepth, (mazeHeight-1) - PaddingDepth);
        spawnPoint = mazeNodes[x, y].transform;
        
        Respawn();
        return new Vector2Int(x, y);
    }
    
    private void Respawn() 
    { 
        transform.position = spawnPoint.position; 
    } 
}
