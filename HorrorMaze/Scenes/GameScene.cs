
namespace HorrorMaze
{
    public class GameScene : Scene
    {
        #region Methods
        public override void SetupScene()
        {
            CameraManager.Setup();

            // Load sound effects
            SceneManager.audioManager.LoadSoundEffect("heartbeat");
            //audioManager.LoadSoundEffect("breathing");
            //audioManager.LoadSoundEffect("Footsteps");

            
            //GameWorld.Instance.IsMouseVisible = true;
            //creates worlds center point
            worldMatrix = Matrix.CreateWorld(Vector3.Zero, Vector3.Forward, Vector3.Up);

            //Test maze
            Maze maze = new Maze();
            MazeCell[,] cells = maze.GenerateMaze(10, 10);
            GameObject go = new GameObject();
            go.AddComponent<MazeRenderer>().SetMaze(cells);
            go.AddComponent<MazeCollider>().SetMaze(cells);

            //test enemy
            GameObject enemy = new GameObject();
            enemy.name = "Enemy";
            enemy.AddComponent<BoxCollider>().size = new Vector3(1, 1, 1);            
            enemy.AddComponent<MeshRenderer>().SetModel("ghost_rig");
            enemy.transform.Position3D = new Vector3(0.5f, 0.5f, 0);
            enemy.AddComponent<Pathing>().mazeCells = cells;
            enemy.AddComponent<Enemy>();
            AudioSource enemyAudioSource = enemy.AddComponent<AudioSource>();

            // test thread
            ThreadManager.Startup(enemy);

            // Add the AudioComponent to AudioManager
            SceneManager.audioManager.AddAudioSource(enemyAudioSource);

            //test player
            GameObject player = new GameObject();
            player.transform.Position3D = new Vector3(1.5f, 1.5f, 1.6f);            
            player.transform.Rotation = new Vector3(0, 0, 0);
            player.AddComponent<PlayerController>();
            player.AddComponent<Camera>();
            // Set up the listener AudioComponent and attach it to the player
            PlayerAudioListener playerAudioListener = player.AddComponent<PlayerAudioListener>();

            //test Goal
            GameObject goal = new GameObject();
            goal.name = "Goal";
            goal.transform.Position3D = new Vector3(4.5f,4.5f,0);
            goal.AddComponent<MeshRenderer>().SetModel("win_item");
            goal.AddComponent<BoxCollider>().size = Vector3.One / 10;
         

            // Add the EnemyAudioController to the enemy object and set its properties:
            EnemyAudioController enemyAudioController = enemy.AddComponent<EnemyAudioController>();
            enemyAudioController.Setup(enemyAudioSource, playerAudioListener, SceneManager.audioManager);
            // Set the PlayerAudioListener in the AudioManager:
            SceneManager.audioManager.SetPlayerAudioListener(playerAudioListener);          
        }
        #endregion
    }
}
