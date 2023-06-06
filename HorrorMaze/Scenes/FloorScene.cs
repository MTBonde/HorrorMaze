namespace HorrorMaze
{
    public class FloorScene : Scene
    {

        MazeCell[,] mazeCells;
        public DebugManager debugManager = new DebugManager();

        #region Methods
        public override void SetupScene()
        {
            //hides the cursor
            GameWorld.Instance.IsMouseVisible = false;
            //stops music track and starts ambient track
            AudioManager.StatBackgroundSound();
            //creates worlds center point
            worldMatrix = Matrix.CreateWorld(Vector3.Zero, Vector3.Forward, Vector3.Up);

            #region instatiating of GameObjects
            //player
            GameObject player = new GameObject();
            player.AddComponent<PlayerController>();
            player.name = "Player";

            //sets up and spawns the maze
            SetupNextFloor();

            //Ambience
            GameObject amb = new GameObject();
            amb.AddComponent<AmbienceController>();

            //Goal
            GameObject goal = new GameObject();
            goal.transform.Position3D = new Vector3(mazeCells.GetLength(0) - 1.5f, mazeCells.GetLength(1) + 0.39f, 0);
            goal.AddComponent<Goal>();

            //spawns everything for the tutorial
            SpawnTutorial();

            SpawnDebugCamera();
            #endregion

            // set timers
            SceneManager._gameTimer.ResetTimer();
            SceneManager._gameTimer.StartTimer();
        }

        private void SetupNextFloor()
        {
            Maze maze = new Maze(15, 15);
            //make maze start room
            mazeCells = new MazeCell[15, 15];
            for (int x = 0; x < mazeCells.GetLength(0); x++)
                for (int y = 0; y < mazeCells.GetLength(1); y++)
                    mazeCells[x, y] = new MazeCell();
            //makes start room
            for (int x = 0; x < 3; x++)
            {
                for (int y = 0; y < 3; y++)
                {
                    mazeCells[x, y].Visited = true;
                    if (y < mazeCells.GetLength(1) - 1)
                        mazeCells[x, y].Walls[0] = false;
                    if (x < mazeCells.GetLength(0) - 1)
                        mazeCells[x, y].Walls[1] = false;
                }
            }
            mazeCells[0, 2].Walls[0] = true;
            mazeCells[2, 0].Walls[1] = true;
            mazeCells[2, 1].Walls[1] = true;
            mazeCells[2, 2].Walls[0] = true;
            mazeCells[2, 2].Walls[1] = true;
            //makes end room
            for (int x = mazeCells.GetLength(0) - 3; x < mazeCells.GetLength(0); x++)
            {
                for (int y = mazeCells.GetLength(1) - 3; y < mazeCells.GetLength(1); y++)
                {
                    mazeCells[x, y].Visited = true;
                    if (y < mazeCells.GetLength(1) - 1)
                        mazeCells[x, y].Walls[0] = false;
                    if (x < mazeCells.GetLength(0) - 1)
                        mazeCells[x, y].Walls[1] = false;
                }
            }
            mazeCells[mazeCells.GetLength(0) - 2, mazeCells.GetLength(1) - 4].Walls[0] = false;
            mazeCells[mazeCells.GetLength(0) - 2, mazeCells.GetLength(1) - 1].Walls[0] = false;

            //escape door
            GameObject escapeDoor = new GameObject();
            escapeDoor.name = "EscapeDoor";
            escapeDoor.AddComponent<Door>();
            escapeDoor.transform.Position = new Vector2(mazeCells.GetLength(0) - 1.5f, mazeCells.GetLength(1) - 3);

            //escape door key
            GameObject escapeDoorKey = new GameObject();
            escapeDoorKey.AddComponent<Key>().door = escapeDoor.GetComponent<Door>();
            escapeDoorKey.transform.Position3D = new Vector3(Globals.Rnd.Next(3, mazeCells.GetLength(0) - 3) - 0.5f, Globals.Rnd.Next(3, mazeCells.GetLength(0) - 3) - 0.5f, 1);
            escapeDoorKey.AddComponent<MeshRenderer>().SetModel("3DModels\\key");

            //generates maze around the rooms
            mazeCells = maze.GenerateMazeFromMaze(mazeCells, new Point(1, 2));

            //places maze in the world
            GameObject mazeObject = new GameObject();
            mazeObject.name = "Maze";
            mazeObject.AddComponent<MazeRenderer>().SetMaze(mazeCells);
            mazeObject.AddComponent<MazeCollider>().SetMaze(mazeCells);
        }

        private void AddEnemy()
        {
            //enemy
            GameObject enemy = new GameObject();
            enemy.transform.Position3D = new Vector3(mazeCells.GetLength(0) - 1.5f, mazeCells.GetLength(1) - 1.5f, 0);
            enemy.name = "Enemy";
            enemy.AddComponent<Pathing>().mazeCells = mazeCells;
            enemy.AddComponent<Enemy>();

            ThreadManager.Startup(enemy);
            //enemy.AddComponent<BackupPathing>().SetMaze(mazeCells);
            //enemy.AddComponent<BackupEnemy>();
        }

        private void SpawnTutorial()
        {
            //tutorial ghost
            //new GameObject().AddComponent<TutorialEnemy>();
            GameObject boulder = new GameObject();

            //tutorial entrance wall defining
            MazeCell[,] tutorialCells = new MazeCell[10, 10];
            for (int x = 0; x < tutorialCells.GetLength(0); x++)
                for (int y = 0; y < tutorialCells.GetLength(1); y++)
                    tutorialCells[x, y] = new MazeCell();
            for (int x = 9; x < tutorialCells.GetLength(0); x++)
                for (int y = 6; y < tutorialCells.GetLength(0); y++)
                    tutorialCells[x, y].Walls[0] = false;
            for (int x = 2; x < tutorialCells.GetLength(0) - 1; x++)
                for (int y = 6; y < 7; y++)
                    tutorialCells[x, y].Walls[1] = false;
            for (int x = 2; x < 3; x++)
                for (int y = 0; y < 6; y++)
                    tutorialCells[x, y].Walls[0] = false;
            for (int x = 0; x < 5; x++)
                for (int y = 0; y < 5; y++)
                {
                    tutorialCells[x, y].Visited = true;
                    if (y != 4)
                        tutorialCells[x, y].Walls[0] = false;
                    if (x != 4)
                        tutorialCells[x, y].Walls[1] = false;
                }

            //spawn the tutorial area/maze in the world
            GameObject tutorialMaze = new GameObject();
            tutorialMaze.transform.Position = new Vector2(-9, -10);
            tutorialMaze.AddComponent<MazeRenderer>().SetMaze(tutorialCells);
            tutorialMaze.AddComponent<MazeCollider>().SetMaze(tutorialCells);

            //tutorial monster door
            GameObject tutorialMonsterDoor = new GameObject();
            tutorialMonsterDoor.name = "MonsterDoor";
            tutorialMonsterDoor.transform.Position3D = new Vector3(-8.5f, -10f, 0);
            Door tutorialMonsterDoor1 = tutorialMonsterDoor.AddComponent<Door>();

            //tutorial front door
            GameObject tutorialfrontDoor = new GameObject();
            tutorialfrontDoor.transform.Position3D = new Vector3(-6.5f, -5, 0);
            Door tutorialfrontDoor1 = tutorialfrontDoor.AddComponent<Door>();

            //tutorial start key
            GameObject tutorialKey = new GameObject();
            tutorialKey.transform.Position3D = new Vector3(-6.5f, -7.5f, 1.25f);
            tutorialKey.AddComponent<Key>().door = tutorialfrontDoor1; //tutorialMonsterDoor1;
            //tutorialKey.GetComponent<Key>().keyEvent += tutorialfrontDoor1.OpenDoor;
            tutorialKey.AddComponent<MeshRenderer>().SetModel("3DModels\\key");

            //tutorial exit door
            GameObject entranceDoor = new GameObject();
            entranceDoor.transform.Position3D = new Vector3(0.5f, 0, 0);
            Door door1 = entranceDoor.AddComponent<Door>();

            //tutorial exit door key
            GameObject key = new GameObject();
            key.transform.Position3D = new Vector3(0.5f, -0.5f, 1.25f);
            key.AddComponent<Key>().door = door1;
            key.AddComponent<MeshRenderer>().SetModel("3DModels\\key");

            //tutorial exit close key/area
            GameObject closeKey = new GameObject();
            closeKey.transform.Position3D = new Vector3(0.5f, 0.5f, 1.25f);
            closeKey.AddComponent<Key>().keyEvent += door1.CloseDoor;

            //boulder spawn event
            GameObject boulderSpawn = new GameObject();
            boulderSpawn.transform.Position3D = new Vector3(-4.5f, -3.5f, 0);
            boulderSpawn.AddComponent<Key>().keyEvent += SpawnBoulder;

            //Run Text
            GameObject runText = new GameObject();
            runText.AddComponent<MeshRenderer>().SetModel("3DModels\\run_text");
            runText.transform.Position3D = new Vector3(-6.5f, -3.101f, 1.5f);
        }

        private void SpawnBoulder()
        {
            SceneManager.GetGameObjectByName("Player").AddComponent<PlayerBoulderAnimation>();
            SceneManager.GetGameObjectByName("Player").GetComponent<PlayerController>().enabled = false;
        }

        private void SpawnDebugCamera()
        {
            GameObject debugCam = new GameObject();
            debugCam.transform.Position3D = new Vector3(0, 0, 10);
            debugCam.transform.Rotation = new Vector3(0, 10, 270);
            debugCam.AddComponent<Camera>().enabled = false;
            debugCam.AddComponent<DebugCameraController>().enabled = false;
            debugCam.name = "DebugCam";
        }
        #endregion
    }
}
