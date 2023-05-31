
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
            gameObject.AddComponent<MeshRenderer>().SetModel("3DModels\\out");
            gameObject.AddComponent<BoxCollider>().size = Vector3.One + Vector3.Up;
            gameObject.GetComponent<BoxCollider>().offset = Vector3.Up;
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
