
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

            //SceneManager._gameTimer = new TimeManager(0, true);


            //GameWorld.Instance.IsMouseVisible = true;
            //creates worlds center point
            worldMatrix = Matrix.CreateWorld(Vector3.Zero, Vector3.Forward, Vector3.Up);

            //test player
            GameObject player = new GameObject();
            player.name = "Player";
            player.transform.Position3D = new Vector3(1.5f, 1.5f, 1.6f);
            player.transform.Rotation = new Vector3(0, 0, 0);
            player.AddComponent<PlayerController>();
            player.AddComponent<Camera>();

            //Test maze
            Maze maze = new Maze();
            //make maze start room
            MazeCell[,] testCells = new MazeCell[10,10];
            for (int x = 0; x < testCells.GetLength(0); x++)
            {
                for (int y = 0; y < testCells.GetLength(1); y++)
                {
                    testCells[x, y] = new MazeCell();
                }
            }
            //makes start room
            for (int x = 0; x < 3; x++)
            {
                for (int y = 0; y < 3; y++)
                {
                    testCells[x, y].Visited = true;
                    testCells[x, y].Walls[0] = false;
                    testCells[x, y].Walls[1] = false;
                }
            }
            testCells[0,2].Walls[0] = true;
            testCells[2,0].Walls[1] = true;
            testCells[2,1].Walls[1] = true;
            testCells[2,2].Walls[0] = true;
            testCells[2,2].Walls[1] = true;
            //makes end room
            for (int x = testCells.GetLength(0)-3;x < testCells.GetLength(0); x++)
            {
                for(int y = testCells.GetLength(1)-3;y < testCells.GetLength(1); y++)
                {
                    testCells[x, y].Visited = true;
                    if(y < testCells.GetLength(1) - 1)
                    testCells[x, y].Walls[0] = false;
                    if(x < testCells.GetLength(0) - 1)
                    testCells[x, y].Walls[1] = false;
                }
            }
            testCells[testCells.GetLength(0) - 2, testCells.GetLength(1) - 4].Walls[0] = false;
            //generates maze around the start room
            MazeCell[,] cells = maze.GenerateMazeFromMaze(testCells,new Point(1,2));
            GameObject mazeObject = new GameObject();
            mazeObject.AddComponent<MazeRenderer>().SetMaze(cells);
            mazeObject.AddComponent<MazeCollider>().SetMaze(cells);

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

            
            // Set up the listener AudioComponent and attach it to the player
            PlayerAudioListener playerAudioListener = player.AddComponent<PlayerAudioListener>();

            //test Goal
            GameObject goal = new GameObject();
            goal.name = "Goal";
            goal.transform.Position3D = new Vector3(testCells.GetLength(0) - 1.5f,testCells.GetLength(1) - 0.5f,0);
            goal.AddComponent<MeshRenderer>().SetModel("win_item");
            goal.AddComponent<BoxCollider>().size = Vector3.One / 10;
            


            // Add the EnemyAudioController to the enemy object and set its properties:
            EnemyAudioController enemyAudioController = enemy.AddComponent<EnemyAudioController>();
            enemyAudioController.Setup(enemyAudioSource, playerAudioListener, SceneManager.audioManager);
            // Set the PlayerAudioListener in the AudioManager:
            SceneManager.audioManager.SetPlayerAudioListener(playerAudioListener);
            SceneManager._gameTimer.ResetToZero();
            SceneManager._gameTimer.StartTimer();
        }
        #endregion
    }
}
