using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

namespace MazeGenerator.Scenes.Menu
{
    public class LevelMenu : MonoBehaviour
    {
        public void ChangeLevel(int level)
        {
            WaitAndLoadScript.ChosenLevel = (WaitAndLoadScript.Level)level;
        }
    }
}
