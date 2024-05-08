using UnityEngine;
using UnityEngine.SceneManagement;

namespace MazeGenerator.Scenes.Menu
{
    public class LevelMenu : MonoBehaviour
    {
        public GameObject button1;
        public GameObject button2;
        public GameObject button3;
        
        public void ChangeLevel(int level)
        {
            WaitAndLoadScript.ChosenLevel = (WaitAndLoadScript.Level)level;
            
        }
    }
}
