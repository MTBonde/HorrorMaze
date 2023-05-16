
using SharpDX.Direct3D9;
using System.Security.Cryptography.X509Certificates;

namespace HorrorMaze
{
    public class GameScene : Scene
    {

        PlayerAudioListener _playerAudioListner;

        #region Methods
        public override void SetupScene()
        {
            //creates worlds center point
            worldMatrix = Matrix.CreateWorld(Vector3.Zero, Vector3.Forward, Vector3.Up);

            SetupCameraAndLoadSoundEffects();

            //test player
            //GameObject player = SetupPlayer();



            //test player
            GameObject player = new GameObject();
            player.name = "Player";
            player.transform.Position3D = new Vector3(-6.5f, -9.5f, 1.6f);
            player.transform.Rotation = new Vector3(0, 0, 0);
            player.AddComponent<PlayerController>();
            player.AddComponent<PlayerController>();
            player.AddComponent<Camera>();

            //Test maze
            Maze maze = new Maze(10, 10);
            //make maze start room
            MazeCell[,] testCells = new MazeCell[10, 10];
            for(int x = 0; x < testCells.GetLength(0); x++)
            {
                for(int y = 0; y < testCells.GetLength(1); y++)
                {
                    testCells[x, y] = new MazeCell();
                }
            }
            //makes start room
            for(int x = 0; x < 3; x++)
            {
                for(int y = 0; y < 3; y++)
                {
                    testCells[x, y].Visited = true;
                    if(y < testCells.GetLength(1) - 1)
                        testCells[x, y].Walls[0] = false;
                    if(x < testCells.GetLength(0) - 1)
                        testCells[x, y].Walls[1] = false;
                }
            }
            testCells[0, 2].Walls[0] = true;
            testCells[2, 0].Walls[1] = true;
            testCells[2, 1].Walls[1] = true;
            testCells[2, 2].Walls[0] = true;
            testCells[2, 2].Walls[1] = true;
            //makes end room
            for(int x = testCells.GetLength(0) - 3; x < testCells.GetLength(0); x++)
            {
                for(int y = testCells.GetLength(1) - 3; y < testCells.GetLength(1); y++)
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
            MazeCell[,] cells = maze.GenerateMazeFromMaze(testCells, new Point(1, 2));
            GameObject mazeObject = new GameObject();
            mazeObject.AddComponent<MazeRenderer>().SetMaze(cells);
            mazeObject.AddComponent<MazeCollider>().SetMaze(cells);

            //door
            GameObject entranceDoor = new GameObject();
            entranceDoor.transform.Position3D = new Vector3(0.5f, 0, 0); 
            entranceDoor.AddComponent<MeshRenderer>().SetModel("3DModels\\door");
            Door door1 = entranceDoor.AddComponent<Door>();
            //door1.OpenDoor();
            BoxCollider doorCol = entranceDoor.AddComponent<BoxCollider>();
            doorCol.size = new Vector3(1, 0.2f, 2.1f);
            doorCol.offset = new Vector3(0, 0, 1);

            //key
            GameObject key = new GameObject();
            key.transform.Position3D = new Vector3(0.5f, -0.5f, 1.25f);
            key.AddComponent<MeshRenderer>().SetModel("3DModels\\key");
            key.AddComponent<Key>().keyEvent += door1.OpenDoor;
            key.AddComponent<BoxCollider>().size = new Vector3(0.25f, 0.25f, 0.25f);

            GameObject closeKey = new GameObject();
            closeKey.transform.Position3D = new Vector3(0.5f, 0.5f, 1.25f);
            //closeKey.AddComponent<MeshRenderer>().SetModel("3DModels\\key");
            closeKey.AddComponent<Key>().keyEvent += door1.CloseDoor;
            closeKey.AddComponent<BoxCollider>().size = new Vector3(0.25f, 0.25f, 0.25f);

            //test enemy
            GameObject enemy = new GameObject();
            enemy.name = "Enemy";
            enemy.AddComponent<BoxCollider>().size = new Vector3(1, 1, 1);            
            enemy.AddComponent<MeshRenderer>().SetModel("3DModels\\ghost_rig");
            enemy.transform.Position3D = new Vector3(1.5f, 1.5f, 0);
            enemy.AddComponent<Pathing>().mazeCells = cells;
            enemy.AddComponent<Enemy>();
            AudioSource enemyAudioSource = enemy.AddComponent<AudioSource>();

            // test thread
            ThreadManager.Startup(enemy);

            // Add the AudioComponent to AudioManager
            SceneManager.audioManager.AddAudioSource(enemyAudioSource);


            // Set up the listener AudioComponent and attach it to the player
            PlayerAudioListener playerAudioListener = player.AddComponent<PlayerAudioListener>();
            _playerAudioListner = playerAudioListener;
            
            //Goal
            GameObject goal = new GameObject();
            goal.name = "Goal";
            goal.transform.Position3D = new Vector3(testCells.GetLength(0) - 1.5f,testCells.GetLength(1) - 0.5f,0);
            goal.AddComponent<MeshRenderer>().SetModel("3DModels\\win_item");
            goal.AddComponent<BoxCollider>().size = Vector3.One / 10;
            goal.AddComponent<Goal>();

            //tutorial entrance
            MazeCell[,] tutorialCells = new MazeCell[10, 10];
            for (int x = 0; x < tutorialCells.GetLength(0); x++)
            {
                for (int y = 0; y < tutorialCells.GetLength(1); y++)
                {
                    tutorialCells[x, y] = new MazeCell();
                }
            }
            for (int x = 9; x < tutorialCells.GetLength(0); x++)
            {
                for (int y = 6; y < tutorialCells.GetLength(0); y++)
                {
                    tutorialCells[x, y].Walls[0] = false;
                }
            }
            for (int x = 2; x < tutorialCells.GetLength(0) - 1; x++)
            {
                for (int y = 6; y < 7; y++)
                {
                    tutorialCells[x, y].Walls[1] = false;
                }
            }
            for (int x = 2; x < 3; x++)
            {
                for (int y = 0; y < 6; y++)
                {
                    tutorialCells[x, y].Walls[0] = false;
                }
            }
            for (int x = 0; x < 5; x++)
            {
                for (int y = 0; y < 5; y++)
                {
                    tutorialCells[x, y].Visited = true;
                    if(y != 4)
                        tutorialCells[x, y].Walls[0] = false;
                    if(x != 4)
                        tutorialCells[x, y].Walls[1] = false;
                }
            }
            GameObject tutorialMaze = new GameObject();
            tutorialMaze.transform.Position = new Vector2(-9, -10);
            tutorialMaze.AddComponent<MazeRenderer>().SetMaze(tutorialCells);
            tutorialMaze.AddComponent<MazeCollider>().SetMaze(tutorialCells);
            
            //tutorial doors
            GameObject tutorialMonsterDoor = new GameObject();
            tutorialMonsterDoor.transform.Position3D = new Vector3(-8.5f, -10f, 0);
            tutorialMonsterDoor.AddComponent<MeshRenderer>().SetModel("3DModels\\door");
            Door tutorialMonsterDoor1 = tutorialMonsterDoor.AddComponent<Door>();
            BoxCollider tutorialMonsterDoorCol = tutorialMonsterDoor.AddComponent<BoxCollider>();
            tutorialMonsterDoorCol.size = new Vector3(1, 0.2f, 2.1f);
            tutorialMonsterDoorCol.offset = new Vector3(0, 0, 1);

            GameObject tutorialfrontDoor = new GameObject();
            tutorialfrontDoor.transform.Position3D = new Vector3(-6.5f, -5, 0);
            tutorialfrontDoor.AddComponent<MeshRenderer>().SetModel("3DModels\\door");
            Door tutorialfrontDoor1 = tutorialfrontDoor.AddComponent<Door>();
            BoxCollider tutorialfrontDoorCol = tutorialfrontDoor.AddComponent<BoxCollider>();
            tutorialfrontDoorCol.size = new Vector3(1, 0.2f, 2.1f);
            tutorialfrontDoorCol.offset = new Vector3(0, 0, 1);


            //tutorial key
            GameObject tutorialKey = new GameObject();
            tutorialKey.transform.Position3D = new Vector3(-6.5f, -7.5f, 1.25f);
            tutorialKey.AddComponent<MeshRenderer>().SetModel("3DModels\\key");
            tutorialKey.AddComponent<Key>().door = tutorialMonsterDoor1;
            tutorialKey.GetComponent<Key>().keyEvent += tutorialfrontDoor1.OpenDoor;
            tutorialKey.GetComponent<Key>().keyEvent += SpawnTutorialGhost;
            tutorialKey.AddComponent<BoxCollider>().size = new Vector3(0.25f, 0.25f, 0.25f);


            // Add the EnemyAudioController to the enemy object and set its properties:
            EnemyAudioController enemyAudioController = enemy.AddComponent<EnemyAudioController>();
            enemyAudioController.Setup(enemyAudioSource, playerAudioListener, SceneManager.audioManager);

            // Add playerAudioController and Audio Sourcing for the player 
            AudioSource playerAudioSource = player.AddComponent<AudioSource>();
            PlayerAudioController playerAudioController = player.AddComponent<PlayerAudioController>();
            playerAudioController.Setup(playerAudioSource, playerAudioListener, SceneManager.audioManager);


            // Set the PlayerAudioListener in the AudioManager:
            SceneManager.audioManager.SetPlayerAudioListener(playerAudioListener);
            SceneManager._gameTimer.ResetTimer();
            SceneManager._gameTimer.StartTimer();
        }

        public void SpawnTutorialGhost()
        {
            GameObject enemy = new GameObject();
            enemy.name = "Enemy";
            enemy.AddComponent<BoxCollider>().size = new Vector3(1, 1, 1);
            enemy.AddComponent<MeshRenderer>().SetModel("3DModels\\ghost_rig");
            enemy.transform.Position3D = new Vector3(-8.5f, -10.5f, 0);
            enemy.AddComponent<TutorialEnemy>();
            enemy.Start();
            AudioSource enemyAudioSource = enemy.AddComponent<AudioSource>();

            // Add the AudioComponent to AudioManager
            SceneManager.audioManager.AddAudioSource(enemyAudioSource);

            // Add the EnemyAudioController to the enemy object and set its properties:
            EnemyAudioController enemyAudioController = enemy.AddComponent<EnemyAudioController>();
            enemyAudioController.Setup(enemyAudioSource, _playerAudioListner, SceneManager.audioManager);
        }

        private void SetupCameraAndLoadSoundEffects()
        {
            CameraManager.Setup();

            // Load sound effects
            SceneManager.audioManager.LoadSoundEffect("heartbeat");
            SceneManager.audioManager.LoadSoundEffect("grudge");
            //SceneManager.audioManager.LoadSoundEffect("breathing");
            //SceneManager.audioManager.LoadSoundEffect("Footsteps");
        }
        private GameObject SetupPlayer()
        {
            GameObject player = new GameObject();
            player.name = "Player";
            player.transform.Position3D = new Vector3(1.5f, 1.5f, 1.6f);
            player.transform.Rotation = new Vector3(0, 0, 0);
            player.AddComponent<PlayerController>();           
            player.AddComponent<Camera>();

            return player;
        }

        #endregion
    }
}
