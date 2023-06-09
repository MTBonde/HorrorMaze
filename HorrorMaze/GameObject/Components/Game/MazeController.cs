using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HorrorMaze
{
    public class MazeController : Component
    {

        List<MazeFloor> mazeFloors = new List<MazeFloor>();
        MazeFloor tutorialFloor = new MazeFloor();
        Maze mazeGenerator = new Maze();
        GameObject mazePart1, mazePart2, mazePart3;
        int currentFloor = 0;
        TextRenderer textRenderer;

        public void Awake()
        {
            gameObject.name = "MazeController";
            SpawnTutorial();
            SetupNextFloor();
            SetupNextFloor();
            SetupNextFloor();
            textRenderer = gameObject.AddComponent<TextRenderer>();
            textRenderer.color = Color.White;
            textRenderer.scale = 3;
            textRenderer.TextPivot = TextRenderer.TextPivots.TopCenter;
            textRenderer.SetText("Floor: " + currentFloor);
            transform.Position = new Vector2(200, 0);
        }

        public void Start()
        {
            //apply first 3 mazes floors for rendering and playing
            mazePart1 = new GameObject();
            mazePart1.AddComponent<MazeRenderer>().SetMaze(mazeFloors[0].maze);
            //mazePart1.AddComponent<MazeCollider>().SetMaze(mazeFloors[0].maze);
            mazePart1.transform.Position3D = new Vector3(0,0,2);
            mazePart2 = new GameObject();
            mazePart2.transform.Position3D = new Vector3(0,0,0);
            mazePart2.AddComponent<MazeRenderer>().SetMaze(mazeFloors[0].maze);
            mazePart2.AddComponent<MazeCollider>().SetMaze(mazeFloors[0].maze);
            mazeFloors[0].EnableFloor();
            mazePart3 = new GameObject();
            mazePart3.AddComponent<MazeRenderer>().SetMaze(mazeFloors[1].maze);
            //mazePart3.AddComponent<MazeCollider>().SetMaze(mazeFloors[1].maze);
            mazePart3.transform.Position3D = new Vector3(0,0,-2);
        }

        public void FloorDown()
        {
            SetupNextFloor();
            mazeFloors[currentFloor].DisableFloor();
            currentFloor++;
            mazeFloors[currentFloor].EnableFloor();
            mazePart1.GetComponent<MazeRenderer>().SetMaze(mazeFloors[currentFloor - 1].maze);
            //mazePart1.GetComponent<MazeCollider>().SetMaze(mazeFloors[currentFloor + 1].maze);
            mazePart2.GetComponent<MazeRenderer>().SetMaze(mazeFloors[currentFloor].maze);
            mazePart2.GetComponent<MazeCollider>().SetMaze(mazeFloors[currentFloor].maze);
            mazePart3.GetComponent<MazeRenderer>().SetMaze(mazeFloors[currentFloor + 1].maze);
            //mazePart3.GetComponent<MazeCollider>().SetMaze(mazeFloors[currentFloor - 1].maze);
            textRenderer.SetText("Floor: " + currentFloor);
        }

        private void SetupNextFloor()
        {
            MazeFloor floor = new MazeFloor();
            MazeCell[,] mazeCells = new MazeCell[15, 15];
            for (int x = 0; x < mazeCells.GetLength(0); x++)
                for (int y = 0; y < mazeCells.GetLength(1); y++)
                    mazeCells[x, y] = new MazeCell();
            //check if first floor
            if (mazeFloors.Count == 0)
            {
                //makes start room
                for (int x = 0; x < 3; x++)
                {
                    for (int y = 0; y < 3; y++)
                    {
                        mazeCells[x, y].Visited = true;
                        mazeCells[x, y].Used = true;
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
            }
            else
            {
                //make stair entrances
                for (int x = 0;x < mazeFloors[mazeFloors.Count-1].maze.GetLength(0); x++)
                {
                    for(int y = 0;y < mazeFloors[mazeFloors.Count-1].maze.GetLength(1); y++)
                    {
                        if(mazeFloors[mazeFloors.Count - 1].maze[x, y].hasStairsDown)
                        {
                            mazeCells[x,y].hasStairsUp = true;
                            mazeCells[x,y].Used = true;
                            mazeCells[x,y].Visited = true;
                            mazeCells[x,y].Walls[0] = false;
                        }
                    }
                }
            }

            #region random rooms
            // Define maximum x and y values for room placement.
            // These are based on the dimensions of the maze,
            // with the intention of allowing room placement within every 5x5 area,
            // We use the moduos operator to achive that.
            int xRoomMax = (mazeCells.GetLength(0) - (mazeCells.GetLength(0) % 5)) / 5,
                yRoomMax = (mazeCells.GetLength(1) - (mazeCells.GetLength(1) % 5)) / 5;

            // Set the initial number of rooms to spawn
            int spawnRoomAmount = 4;

            // Create a list to store the locations of filled chunks.
            // Pre-fill the list with points in the top-left and bottom-right corners of the maze.
            List<Point> filledChuncks = new List<Point>();

            for (int x = 0; x < mazeCells.GetLength(0); x++)
            {
                for (int y = 0; y < mazeCells.GetLength(0); y++)
                {
                    if (mazeCells[x,y].Visited || mazeCells[x, y].Used)
                    {
                        if (!filledChuncks.Contains(new Point((x - (x % 5)) / 5, (y - (y % 5)) / 5)))
                        {
                            filledChuncks.Add(new Point((x - (x % 5)) / 5, (y - (y % 5)) / 5));

                        }
                    }
                }
            }

            // If the number of available room slots (total slots minus corners)
            // is less than the desired spawn amount, adjust the spawn amount down.
            if (xRoomMax * yRoomMax - filledChuncks.Count < spawnRoomAmount)
                spawnRoomAmount = xRoomMax * yRoomMax - filledChuncks.Count;

            // Loop until we've created the desired number of rooms
            while (spawnRoomAmount > 0)
            {
                // Choose a random location in the maze to place a room
                int xRoomSpawnLocation = Globals.Rnd.Next(0, xRoomMax);
                int yRoomSpawnLocation = Globals.Rnd.Next(0, yRoomMax);

                // Determine a random offset within the chosen 5x5 area to place the room
                int xOffset = Globals.Rnd.Next(3), yOffset = Globals.Rnd.Next(3);

                // If the chosen location is already filled, skip this iteration
                if (filledChuncks.Contains(new Point(xRoomSpawnLocation, yRoomSpawnLocation)))
                    continue;

                // Add the chosen location to the filled chunks list
                filledChuncks.Add(new Point(xRoomSpawnLocation, yRoomSpawnLocation));

                // Add a room at the chosen location with the determined offset
                mazeCells = mazeGenerator.AddRoomBeforeMaze(new Point(xRoomSpawnLocation * 5 + xOffset, yRoomSpawnLocation * 5 + yOffset), 3, 3, 3, mazeCells);

                // Create a new game object for the roomStatue to place in the room
                GameObject roomStatue = new GameObject();
                floor.mazeObjects.Add(roomStatue);

                // Add a 3D model for the roomStatue from a file and set its position in the room
                roomStatue.AddComponent<MeshRenderer>().SetModel("3DModels\\statue_" + spawnRoomAmount);
                roomStatue.transform.Position = new Vector2(xRoomSpawnLocation * 5 + xOffset + 1.5f, yRoomSpawnLocation * 5 + yOffset + 1.5f);

                // Add a box collider to the roomStatue, and set its size and offset
                roomStatue.AddComponent<BoxCollider>().size = new Vector3(1, 1, 2);
                roomStatue.GetComponent<BoxCollider>().offset = new Vector3(0, 0, 1);

                // Decrease the number of rooms left to spawn
                spawnRoomAmount--;
            }
            #endregion

            //makes Down staircase
            Point staircasePoint = new Point(Globals.Rnd.Next(1, mazeCells.GetLength(0) - 1), Globals.Rnd.Next(1, mazeCells.GetLength(1) - 1));
            while (mazeCells[staircasePoint.X, staircasePoint.Y].Used)
            {
                staircasePoint = new Point(Globals.Rnd.Next(1, mazeCells.GetLength(0) - 1), Globals.Rnd.Next(1, mazeCells.GetLength(1) - 1));
            }
            mazeCells[staircasePoint.X, staircasePoint.Y].hasStairsDown = true;
            mazeCells[staircasePoint.X, staircasePoint.Y].Used = true;
            mazeCells[staircasePoint.X, staircasePoint.Y].Visited = true;
            mazeCells[staircasePoint.X, staircasePoint.Y - 1].Walls[0] = false;

            //down collider
            GameObject staircase = new GameObject();
            staircase.transform.Position3D = new Vector3(staircasePoint.X + 0.5f, staircasePoint.Y + 0.5f, 0);
            staircase.AddComponent<BoxCollider>().size = new Vector3(1,0.6f,1.5f);
            staircase.GetComponent<BoxCollider>().offset = new Vector3(0,0.2f,1);
            staircase.AddComponent<Staircase>();

            floor.mazeObjects.Add(staircase);

            //stair door
            GameObject escapeDoor = new GameObject();
            escapeDoor.name = "StaircaseDoor";
            escapeDoor.AddComponent<Door>();
            escapeDoor.transform.Position3D = new Vector3(staircasePoint.X + 0.5f, staircasePoint.Y, 0);
            floor.mazeObjects.Add(escapeDoor);

            //stair door key
            Point staircaseKeyPoint = new Point(Globals.Rnd.Next(0, mazeCells.GetLength(0)), Globals.Rnd.Next(0, mazeCells.GetLength(1)));
            while (mazeCells[staircaseKeyPoint.X, staircaseKeyPoint.Y].Used)
            {
                staircaseKeyPoint = new Point(Globals.Rnd.Next(0, mazeCells.GetLength(0)), Globals.Rnd.Next(0, mazeCells.GetLength(1)));
            }
            GameObject escapeDoorKey = new GameObject();
            escapeDoorKey.AddComponent<Key>().door = escapeDoor.GetComponent<Door>();
            escapeDoorKey.transform.Position3D = new Vector3(Globals.Rnd.Next(3, mazeCells.GetLength(0) - 3) - 0.5f, Globals.Rnd.Next(3, mazeCells.GetLength(0) - 3) - 0.5f, 1);
            escapeDoorKey.AddComponent<MeshRenderer>().SetModel("3DModels\\key");
            floor.mazeObjects.Add(escapeDoorKey);

            //generates maze around the rooms
            mazeCells = mazeGenerator.GenerateMazeFromMazeForFloor(mazeCells, staircasePoint - new Point(0,1));

            //places maze in the world needs new implemetation
            //GameObject mazeObject = new GameObject();
            //mazeObject.name = "Maze";
            //mazeObject.AddComponent<MazeRenderer>().SetMaze(mazeCells);
            //mazeObject.AddComponent<MazeCollider>().SetMaze(mazeCells);

            floor.maze = mazeCells;
            floor.DisableFloor();
            mazeFloors.Add(floor);
        }

        private void AddEnemy(int mazeNumber)
        {
            //enemy
            GameObject enemy = new GameObject();
            enemy.transform.Position3D = new Vector3(mazeFloors[mazeNumber].maze.GetLength(0) - 1.5f, mazeFloors[mazeNumber].maze.GetLength(1) - 1.5f, 0);
            enemy.name = "Enemy";
            enemy.AddComponent<Pathing>().mazeCells = mazeFloors[mazeNumber].maze;
            enemy.AddComponent<Enemy>();
            //enemy.AddComponent<BackupPathing>().SetMaze(mazeCells);
            //enemy.AddComponent<BackupEnemy>();

            mazeFloors[mazeNumber].mazeObjects.Add(enemy);

            ThreadManager.Startup(enemy);
        }

        private void SpawnTutorial()
        {
            //tutorial entrance wall defining
            tutorialFloor.maze = new MazeCell[10, 10];
            for (int x = 0; x < tutorialFloor.maze.GetLength(0); x++)
                for (int y = 0; y < tutorialFloor.maze.GetLength(1); y++)
                    tutorialFloor.maze[x, y] = new MazeCell();
            for (int x = 9; x < tutorialFloor.maze.GetLength(0); x++)
                for (int y = 6; y < tutorialFloor.maze.GetLength(0); y++)
                    tutorialFloor.maze[x, y].Walls[0] = false;
            for (int x = 2; x < tutorialFloor.maze.GetLength(0) - 1; x++)
                for (int y = 6; y < 7; y++)
                    tutorialFloor.maze[x, y].Walls[1] = false;
            for (int x = 2; x < 3; x++)
                for (int y = 0; y < 6; y++)
                    tutorialFloor.maze[x, y].Walls[0] = false;
            for (int x = 0; x < 5; x++)
                for (int y = 0; y < 5; y++)
                {
                    tutorialFloor.maze[x, y].Visited = true;
                    if (y != 4)
                        tutorialFloor.maze[x, y].Walls[0] = false;
                    if (x != 4)
                        tutorialFloor.maze[x, y].Walls[1] = false;
                }

            //spawn the tutorial area/maze in the world
            GameObject tutorialMaze = new GameObject();
            tutorialMaze.transform.Position = new Vector2(-9, -10);
            tutorialMaze.AddComponent<MazeRenderer>().SetMaze(tutorialFloor.maze);
            tutorialMaze.AddComponent<MazeCollider>().SetMaze(tutorialFloor.maze);
            tutorialFloor.mazeObjects.Add(tutorialMaze);

            //tutorial monster door
            GameObject tutorialMonsterDoor = new GameObject();
            tutorialMonsterDoor.name = "MonsterDoor";
            tutorialMonsterDoor.transform.Position3D = new Vector3(-8.5f, -10f, 0);
            Door tutorialMonsterDoor1 = tutorialMonsterDoor.AddComponent<Door>();
            tutorialFloor.mazeObjects.Add(tutorialMonsterDoor);

            //tutorial front door
            GameObject tutorialfrontDoor = new GameObject();
            tutorialfrontDoor.transform.Position3D = new Vector3(-6.5f, -5, 0);
            Door tutorialfrontDoor1 = tutorialfrontDoor.AddComponent<Door>();
            tutorialFloor.mazeObjects.Add(tutorialfrontDoor);

            //tutorial start key
            GameObject tutorialKey = new GameObject();
            tutorialKey.transform.Position3D = new Vector3(-6.5f, -7.5f, 1.25f);
            tutorialKey.AddComponent<Key>().door = tutorialfrontDoor1; //tutorialMonsterDoor1;
            //tutorialKey.GetComponent<Key>().keyEvent += tutorialfrontDoor1.OpenDoor;
            tutorialKey.AddComponent<MeshRenderer>().SetModel("3DModels\\key");
            tutorialFloor.mazeObjects.Add(tutorialKey);

            //tutorial exit door
            GameObject entranceDoor = new GameObject();
            entranceDoor.transform.Position3D = new Vector3(0.5f, 0, 0);
            Door door1 = entranceDoor.AddComponent<Door>();
            tutorialFloor.mazeObjects.Add(entranceDoor);

            //tutorial exit door key
            GameObject key = new GameObject();
            key.transform.Position3D = new Vector3(0.5f, -0.5f, 1.25f);
            key.AddComponent<Key>().door = door1;
            key.AddComponent<MeshRenderer>().SetModel("3DModels\\key");
            tutorialFloor.mazeObjects.Add(key);

            //tutorial exit close key/area
            GameObject closeKey = new GameObject();
            closeKey.transform.Position3D = new Vector3(0.5f, 0.5f, 1.25f);
            closeKey.AddComponent<Key>().keyEvent += door1.CloseDoor;
            tutorialFloor.mazeObjects.Add(closeKey);

            //boulder spawn event
            GameObject boulderSpawn = new GameObject();
            boulderSpawn.transform.Position3D = new Vector3(-4.5f, -3.5f, 0);
            boulderSpawn.AddComponent<Key>().keyEvent += SpawnBoulder;
            tutorialFloor.mazeObjects.Add(boulderSpawn);

            //Run Text
            GameObject runText = new GameObject();
            runText.AddComponent<MeshRenderer>().SetModel("3DModels\\run_text");
            runText.transform.Position3D = new Vector3(-6.5f, -3.101f, 1.5f);
            tutorialFloor.mazeObjects.Add(runText);
        }

        private void SpawnBoulder()
        {
            SceneManager.GetGameObjectByName("Player").AddComponent<PlayerBoulderAnimation>();
            SceneManager.GetGameObjectByName("Player").GetComponent<PlayerController>().enabled = false;
        }
    }

    public class MazeFloor
    {

        public MazeCell[,] maze;
        public List<GameObject> mazeObjects = new List<GameObject>();

        public void EnableFloor()
        {
            for (int i = 0; i < mazeObjects.Count; i++)
            {
                mazeObjects[i].enabled = true;
                for (int j = 0; j < mazeObjects[i].Components.Count; j++)
                {
                    mazeObjects[i].Components[j].enabled = true;
                }
            }
        }

        public void DisableFloor()
        {
            for (int i = 0; i < mazeObjects.Count; i++)
            {
                mazeObjects[i].enabled = false;
                for (int j = 0; j < mazeObjects[i].Components.Count; j++)
                {
                    mazeObjects[i].Components[j].enabled = false;
                }
            }
        }
    }
}
