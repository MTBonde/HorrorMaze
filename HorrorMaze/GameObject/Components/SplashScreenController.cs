using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HorrorMaze
{
    public class SplashScreenController : Component
    {

        float timer = 0, waitTimer = 5;

        public void Update()
        {
            timer += Globals.DeltaTime;
            if (timer > waitTimer)
            {
                BackupAudioManager.StartBackgroundMusic();
                SceneManager.LoadScene(1);
            }
        }
    }
}
