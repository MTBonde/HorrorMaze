

namespace HorrorMaze
{
    /// <summary>
    /// controls and animates the splash screen
    /// animation needs implementation
    /// Niels
    /// </summary>
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
