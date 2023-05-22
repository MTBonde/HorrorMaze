using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HorrorMaze
{
    /// <summary>
    /// Win consition for the game
    /// Niels
    /// </summary>
    public class Goal : Component
    {



        /// <summary>
        /// adds all needed components
        /// </summary>
        public void Awake()
        {
            gameObject.name = "Goal";
            gameObject.AddComponent<MeshRenderer>().SetModel("3DModels\\win_item");
            gameObject.AddComponent<BoxCollider>().size = Vector3.One / 10;
        }

        public void OnCollision(GameObject go)
        {
            if (go != null)
                if (go.name == "Player")
                {
                    //stop timer here
                    TimeSpan endTime = SceneManager._gameTimer.GetElapsedTime();
                    SceneManager._gameTimer.StopTimer();
                    Debug.WriteLine($"Game ends. The end time is {endTime} milliseconds.");

                    //load win scene
                    SceneManager.LoadScene(5);
                }
        }
    }
}
