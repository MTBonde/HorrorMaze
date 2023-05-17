using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HorrorMaze
{
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
