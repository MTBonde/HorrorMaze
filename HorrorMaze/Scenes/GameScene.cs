
namespace HorrorMaze
{
    public class GameScene : Scene
    {



        #region Methods
        public override void SetupScene()
        {
            // Initialize AudioManager
            AudioManager audioManager = new AudioManager();

            // Load sound effects
            audioManager.LoadSoundEffect("heartbeat");
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
            go = new GameObject();
            go.name = "Enemy";
            go.AddComponent<BoxCollider>().size = new Vector3(1, 1, 1);
            go.AddComponent<MeshRenderer>().SetModel("ghost_rig");
            go.transform.Position3D = new Vector3(0.5f, 0.5f, 0);
            go.AddComponent<Pathing>().mazeCells = cells;
            go.AddComponent<Enemy>();
            GameObject enemy = new GameObject();
            enemy = new GameObject();
            enemy.AddComponent<MeshRenderer>().SetModel("ghost_rig");
            enemy.transform.Position3D = new Vector3(0.5f, 0.5f, 0);
            enemy.AddComponent<Pathing>().mazeCells = cells;
            enemy.AddComponent<Enemy>();
            AudioSource enemyAudioSource = enemy.AddComponent<AudioSource>();

            // test thread
            ThreadManager.Startup(enemy);

            // Add the AudioComponent to AudioManager
            audioManager.AddAudioSource(enemyAudioSource);

            //test player
            GameObject player = new GameObject();           
            player.transform.Position3D = new Vector3(0.5f, 1.5f, 1.6f);
            player.transform.Rotation = new Vector3(0, 0, 0);
            player.AddComponent<PlayerController>();
            player.AddComponent<Camera>();
            // Set up the listener AudioComponent and attach it to the player
            PlayerAudioListener playerAudioListener = player.AddComponent<PlayerAudioListener>();


            //test cam
            go = new GameObject();
            go.transform.Position3D = new Vector3(1.5f, 1.5f, 1.6f);
            go.transform.Rotation = new Vector3(0, 0, 0);
            go.AddComponent<PlayerController>();
            go.AddComponent<Camera>();

            //test Goal
            go = new GameObject();
            go.name = "Goal";
            go.transform.Position3D = new Vector3(4.5f,4.5f,0);
            go.AddComponent<MeshRenderer>().SetModel("win_item");
            go.AddComponent<BoxCollider>().size = Vector3.One / 10;

            //test UI
            go = new GameObject();
            go.AddComponent<SpriteRenderer>().SetSprite("Company Logo");
            go.transform.Position = new Vector2(200,200);
            // Add the EnemyAudioController to the enemy object and set its properties:
            EnemyAudioController enemyAudioController = enemy.AddComponent<EnemyAudioController>();
            enemyAudioController.Setup(enemyAudioSource, playerAudioListener, audioManager);
            // Set the PlayerAudioListener in the AudioManager:
            audioManager.SetPlayerAudioListener(playerAudioListener);          
        }
        #endregion
    }
}
