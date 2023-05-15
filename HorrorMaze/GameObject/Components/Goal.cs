using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HorrorMaze
{
    public class Goal : Component
    {



        public void OnCollision(GameObject go)
        {
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
