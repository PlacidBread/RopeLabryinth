using System.Collections;
using UnityEngine;

public class GameRespawn : MonoBehaviour
{
    [SerializeField] private CharacterController playerController;
    private Transform spawnPoint { get; set; }

    // [SerializeField] private CharacterController characterController; 
    // private MazeGenerator.Scripts.MazeGenerator _mazeGenerator; 

    [SerializeField] private int PaddingDepth;
    private void Start() 
    { 
        // characterController = GetComponent<CharacterController>(); 
        // _mazeGenerator = GetComponent<MazeGenerator.Scripts.MazeGenerator>();
    } 
    //
    // private void Update() 
    // { 
    //     if (characterController.isGrounded && transform.position.y < -10) 
    //     { 
    //         Respawn(); 
    //     } 
    // } 
    public (Transform, int, int) SetSpawn(MazeNode[,] mazeNodes, int mazeWidth, int mazeHeight)
    {
        int x = Random.Range(0 + PaddingDepth, (mazeWidth-1) - PaddingDepth);
        int y = Random.Range(0 + PaddingDepth, (mazeHeight-1) - PaddingDepth);
        spawnPoint = mazeNodes[x, y].transform;
        
        StartCoroutine(Respawn());
        return (spawnPoint, x, y);
    }
    
    private IEnumerator Respawn()
    {
        yield return new WaitForSeconds(0.01f);
        Debug.Log("-" + transform.position);
        playerController.enabled = false;
        playerController.transform.position = new Vector3(spawnPoint.position.x+0.5f, spawnPoint.position.y, spawnPoint.position.z+0.5f);
        playerController.enabled = true;
    } 
}
