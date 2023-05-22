using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HorrorMaze
{
    /// <summary>
    /// a timer that goes to the main menu after a given time
    /// Niels
    /// </summary>
    public class MainMenuTimer : Component
    {

        float timer, timerTime = 20f;

        void Update()
        {
            if (timer < timerTime)
                timer += Globals.DeltaTime;
            else
                SceneManager.LoadScene(0);
        }
    }
}
