namespace HorrorMaze
{
    /// <summary>     
    /// This class takes care of generating a maze using DFS algoritm
    /// by Thor
    /// </summary>
    public class Maze
    {
        // FIELDS
        #region Fields        
        private MazeCell[,] _mazeCells;
        #endregion

        // METHODS
        #region Methods
        // Maze Generation
        #region MAZEGENERATION
        /// <summary>
        /// Generate the maze, by making a multi array of mazecells, 
        /// setting all 4 walls to true and all mazecells to unvisited, 
        /// except the starting cell
        /// </summary>
        public void GenerateMaze()
        {
            for(int x = 0; x < _mazeCells.GetLength(0); x++)
                for(int z = 0; z < _mazeCells.GetLength(1); z++)
                {
                    _mazeCells[x, z].Walls[0] = true;
                    _mazeCells[x, z].Walls[1] = true;
                    _mazeCells[x, z].Walls[2] = true;
                    _mazeCells[x, z].Walls[3] = true;
                    _mazeCells[x, z].Visited = false;
                }
            _mazeCells[0, 0].Visited = true;
            EvaluateCell(new Point(0, 0));
        }

        /// <summary>
        /// Generates a maze by inputing a mazecell array and returning a generated maze using DFS.
        /// </summary>
        /// <param name="maze">The maze to generate from.</param>
        /// <param name="MazeStartPoint">The starting point of the maze.</param>
        /// Niels / Thor
        /// <returns>The generated maze.</returns>
        public MazeCell[,] GenerateMazeFromMaze(MazeCell[,] maze, Point MazeStartPoint)
        {
            // Generates a random number between 1 and 10.
            Globals.Rnd.Next(1, 10);

            // Sets the mazecells to the maze given in the signature.
            _mazeCells = maze;

            //remove random walls
            //KnockDownRandomWalls(2);

            //add rooms
            #region AddingRooms

            // Define maximum x and y values for room placement.
            // These are based on the dimensions of the maze,
            // with the intention of allowing room placement within every 5x5 area,
            // We use the moduos operator to achive that.
            int xRoomMax = (maze.GetLength(0) - (maze.GetLength(0) % 5)) / 5,
                yRoomMax = (maze.GetLength(1) - (maze.GetLength(1) % 5)) / 5;

            // Set the initial number of rooms to spawn
            int spawnRoomAmount = 4;

            // Create a list to store the locations of filled chunks.
            // Pre-fill the list with points in the top-left and bottom-right corners of the maze.
            List<Point> filledChuncks = new List<Point>() { new Point(0, 0), new Point(xRoomMax - 1, yRoomMax - 1) };

            // If the number of available room slots (total slots minus corners)
            // is less than the desired spawn amount, adjust the spawn amount down.
            if(xRoomMax * yRoomMax - 4 < spawnRoomAmount)
                spawnRoomAmount = xRoomMax * yRoomMax - 4;

            // Loop until we've created the desired number of rooms
            while(spawnRoomAmount > 0)
            {
                // Choose a random location in the maze to place a room
                int xRoomSpawnLocation = Globals.Rnd.Next(0, xRoomMax);
                int yRoomSpawnLocation = Globals.Rnd.Next(0, yRoomMax);

                // Determine a random offset within the chosen 5x5 area to place the room
                int xOffset = Globals.Rnd.Next(3), yOffset = Globals.Rnd.Next(3);

                // If the chosen location is already filled, skip this iteration
                if(filledChuncks.Contains(new Point(xRoomSpawnLocation, yRoomSpawnLocation)))
                    continue;

                // Add the chosen location to the filled chunks list
                filledChuncks.Add(new Point(xRoomSpawnLocation, yRoomSpawnLocation));

                // Add a room at the chosen location with the determined offset
                AddRoomBeforeMaze(new Point(xRoomSpawnLocation * 5 + xOffset, yRoomSpawnLocation * 5 + yOffset), 3, 3, 3, maze);

                // Create a new game object for the roomStatue to place in the room
                GameObject roomStatue = new GameObject();

                // Add a 3D model for the roomStatue from a file and set its position in the room
                roomStatue.AddComponent<MeshRenderer>().SetModel("3DModels\\statue_" + spawnRoomAmount);
                roomStatue.transform.Position = new Vector2(xRoomSpawnLocation * 5 + xOffset + 1.5f, yRoomSpawnLocation * 5 + yOffset + 1.5f);

                // Add a box collider to the roomStatue, and set its size and offset
                roomStatue.AddComponent<BoxCollider>().size = new Vector3(1, 1, 2);
                roomStatue.GetComponent<BoxCollider>().offset = new Vector3(0, 0, 1);

                // Decrease the number of rooms left to spawn
                spawnRoomAmount--;
            }
            #endregion AddingRooms

            //create maze
            return EvaluateCell(MazeStartPoint);
        }

        /// <summary>
        /// Generates a maze by inputing a mazecell array and returning a generated maze using DFS.
        /// </summary>
        /// <param name="maze">The maze to generate from.</param>
        /// <param name="MazeStartPoint">The starting point of the maze.</param>
        /// Niels / Thor
        /// <returns>The generated maze.</returns>
        public MazeCell[,] GenerateMazeFromMazeForFloor(MazeCell[,] maze, Point MazeStartPoint)
        {
            // Generates a random number between 1 and 10.
            Globals.Rnd.Next(1, 10);

            // Sets the mazecells to the maze given in the signature.
            _mazeCells = maze;

            //create maze
            return EvaluateCell(MazeStartPoint);
        }

        /// <summary>
        /// Evaluate each startingCell using DFS to create a maze out of the mazecells
        /// by thor
        /// </summary>
        /// <param name="startingCell">is the starting startingCell to evaluate</param>
        private MazeCell[,] EvaluateCell(Point startingCell)
        {
            // create a list of possible directions for unvisited neighbour cells 
            List<int> possibleDirectionsForUnvisitedCells = new List<int>();
            possibleDirectionsForUnvisitedCells.Add(0); // up
            possibleDirectionsForUnvisitedCells.Add(1); // Right
            possibleDirectionsForUnvisitedCells.Add(2); // Down
            possibleDirectionsForUnvisitedCells.Add(3); // Left

            // While there is still an potential direction with an unvisited neighbour cell do this
            while(possibleDirectionsForUnvisitedCells.Count > 0)
            {
                // Randomly pick a direction from the list and set is as selected, and remove the selected from the list
                int pick = Globals.Rnd.Next(0, possibleDirectionsForUnvisitedCells.Count);
                int selectedDirection = possibleDirectionsForUnvisitedCells[pick];
                possibleDirectionsForUnvisitedCells.RemoveAt(pick);

                // Find the coordinate of the selected neighbour cell
                Point SelectedNeighborCell = startingCell;
                switch(selectedDirection)
                {
                    case 0:
                        SelectedNeighborCell += new Point(0, -1);
                        break;
                    case 1:
                        SelectedNeighborCell += new Point(1, 0);
                        break;
                    case 2:
                        SelectedNeighborCell += new Point(0, 1);
                        break;
                    case 3:
                        SelectedNeighborCell += new Point(-1, 0);
                        break;
                }
                // make sure the Selected neighbour cell is within the maze
                if(
                    (SelectedNeighborCell.X >= 0) &&
                    (SelectedNeighborCell.X < _mazeCells.GetLength(0)) &&
                    (SelectedNeighborCell.Y >= 0) &&
                    (SelectedNeighborCell.Y < _mazeCells.GetLength(1))
                )
                {
                    // if the SelectedNeighborCell has not been visited
                    if(!_mazeCells[SelectedNeighborCell.X, SelectedNeighborCell.Y].Visited)
                    {
                        // mark the SelectedNeighborCell as visited
                        _mazeCells[SelectedNeighborCell.X, SelectedNeighborCell.Y].Visited = true;

                        // Update the current startingCell's wall
                        if(selectedDirection == 1) // Right SelectedNeighborCell
                        {
                            // Remove the right wall of the current startingCell
                            _mazeCells[startingCell.X, startingCell.Y].Walls[1] = false;
                        }
                        else if(selectedDirection == 2) // up SelectedNeighborCell
                        {
                            // Remove the bottom wall of the current startingCell
                            _mazeCells[startingCell.X, startingCell.Y].Walls[0] = false;
                        }

                        // Update the corresponding wall of the neighboring startingCell
                        if(selectedDirection == 0) // buttom SelectedNeighborCell
                        {
                            // Remove the right wall of the neighboring startingCell
                            _mazeCells[SelectedNeighborCell.X, SelectedNeighborCell.Y].Walls[0] = false;
                        }
                        else if(selectedDirection == 3) // left SelectedNeighborCell
                        {
                            // Remove the bottom wall of the neighboring startingCell
                            _mazeCells[SelectedNeighborCell.X, SelectedNeighborCell.Y].Walls[1] = false;
                        }

                        // Do again all cells has been marked as visited
                        EvaluateCell(SelectedNeighborCell);
                    }
                }
            }
            //if no more return the created maze
            return _mazeCells;
        }
        #endregion MAZEGENERATION

        // Rooms, WAll removel and exstra routes
        #region Rooms

        /// <summary>
        /// We use This function to add a room to an existing maze. 
        /// The room is defined by its bottom left corner, width, height, and number of entrances. 
        /// The function edits the maze passed in and returns the modified maze.
        /// Niels
        /// </summary>
        /// <param name="buttomLeftCornerOffRoom">The bottom-left corner of the room.</param>
        /// <param name="width">The width of the room.</param>
        /// <param name="height">The height of the room.</param>
        /// <param name="entances">The number of entrances in the room.</param>
        /// <param name="mazeToEdit">The maze to which the room will be added.</param>
        /// <returns>The modified maze with the added room.</returns>
        public MazeCell[,] AddRoomBeforeMaze(Point buttomLeftCornerOffRoom, int width, int height, int entances, MazeCell[,] mazeToEdit)
        {
            // For each startingCell within the room dimensions x and y
            for(int x = buttomLeftCornerOffRoom.X; x < buttomLeftCornerOffRoom.X + width; x++)
            {
                for(int y = buttomLeftCornerOffRoom.Y; y < buttomLeftCornerOffRoom.Y + height; y++)
                {
                    // If not the top edge of the room, remove the top wall
                    if(y != buttomLeftCornerOffRoom.Y + height - 1)
                        mazeToEdit[x, y].Walls[0] = false;

                    // If not the right edge of the room, remove the right wall
                    if(x != buttomLeftCornerOffRoom.X + width - 1)
                        mazeToEdit[x, y].Walls[1] = false;

                    // Mark the startingCell as visited
                    mazeToEdit[x, y].Visited = true;
                }
            }

            // Creates entrances while there are entrances left to create
            while(entances > 0)
            {
                int side = Globals.Rnd.Next(0, 2);
                int x = 0;
                int y = 0;

                // If side is 0, determine a north or south entrance
                if(side == 0)
                {
                    side = Globals.Rnd.Next(0, 2);
                    x = Globals.Rnd.Next(buttomLeftCornerOffRoom.X, buttomLeftCornerOffRoom.X + width);

                    // If possible, create a north entrance
                    if(buttomLeftCornerOffRoom.Y > 0 && side == 0)
                        y = buttomLeftCornerOffRoom.Y - 1;

                    // If possible, create a south entrance
                    else if(side == 1 && buttomLeftCornerOffRoom.Y + height != mazeToEdit.GetLength(1))
                        y = buttomLeftCornerOffRoom.Y + height - 1;
                    else
                        continue;

                    // If the startingCell has a north wall, remove it and lower entrances number
                    if(mazeToEdit[x, y].Walls[0])
                    {
                        mazeToEdit[x, y].Walls[0] = false;
                        entances--;
                    }
                }
                // If side is 1, determine a west or east entrance
                else if(side == 1)
                {
                    side = Globals.Rnd.Next(0, 2);
                    y = Globals.Rnd.Next(buttomLeftCornerOffRoom.Y, buttomLeftCornerOffRoom.Y + height);

                    // If possible, create a west entrance
                    if(buttomLeftCornerOffRoom.X > 0 && side == 0)
                        x = buttomLeftCornerOffRoom.X - 1;

                    // If possible, create an east entrance
                    else if(side == 1 && buttomLeftCornerOffRoom.X + width != mazeToEdit.GetLength(0))
                        x = buttomLeftCornerOffRoom.X + width - 1;
                    else
                        continue;

                    // If the startingCell has an east wall, remove it and lower entrances number
                    if(mazeToEdit[x, y].Walls[1])
                    {
                        mazeToEdit[x, y].Walls[1] = false;
                        entances--;
                    }
                }
            }

            // Return the edited maze
            return mazeToEdit;
        }
        

        /// <summary>
        /// Calculate rooms by dividing total number of cells with a dividing factor
        /// thor
        /// </summary>
        /// <param name="numberUsedForRoomCalculation">1 room pr x openspaces</param>
        private void AddRooms(int numberUsedForRoomCalculation)
        {
            // set the number of rooms based on overall maze size
            int totalCells = _mazeCells.GetLength(0) * _mazeCells.GetLength(1);
            // one 1 room per 10 maze cells
            int numberOfRooms = totalCells / numberUsedForRoomCalculation;

            // Add open spaces
            AddOpenSpace(3, 3, numberOfRooms);
        }

        /// <summary>
        /// add rooms/ open spaces in the maces at random locations
        /// </summary>
        /// <param name="width">width of the rooms</param>
        /// <param name="height">height of the rooms</param>
        /// <param name="numberOfOpenSpaces">how many open spaces</param>
        public void AddOpenSpace(int width, int height, int numberOfOpenSpaces)
        {
            for(int i = 0; i < numberOfOpenSpaces; i++)
            {
                int attempt = 0;
                const int maxAttempts = 10;  // might be to high/low
                while(attempt < maxAttempts)
                {
                    int startX = Globals.Rnd.Next(0, _mazeCells.GetLength(0) - width);
                    int startY = Globals.Rnd.Next(0, _mazeCells.GetLength(1) - height);

                    // Check if the open space area is unvisited aka has a room
                    bool canPlace = true;
                    for(int x = startX; x < startX + width; x++)
                    {
                        for(int y = startY; y < startY + height; y++)
                        {
                            if(_mazeCells[x, y].Visited)
                            {
                                canPlace = false;
                                break;
                            }
                        }
                        if(!canPlace)
                            break;
                    }

                    // If the area is unvisited, mark it as visited and remove walls
                    if(canPlace)
                    {
                        for(int x = startX; x < startX + width; x++)
                        {
                            for(int y = startY; y < startY + height; y++)
                            {
                                _mazeCells[x, y].Visited = true;
                                // Remove walls 
                                if(x > startX) _mazeCells[x, y].Walls[1] = false;
                                if(y > startY) _mazeCells[x, y].Walls[0] = false;
                            }
                        }
                        break;
                    }
                    // increase attempt counter
                    attempt++;
                }
            }
        }

        #endregion Rooms

        #region WALLREMOVE
        /// <summary>
        /// Remove walls randomly in the maze to create more routes to goal
        /// thor
        /// </summary>
        /// <param name="chance">percent change for a wall to be removed</param>
        public void KnockDownRandomWalls(int chance)
        {
            for(int x = 0; x < _mazeCells.GetLength(0); x++)
            {
                for(int y = 0; y < _mazeCells.GetLength(1); y++)
                {
                    // Randomly decide if we should create an extra path at this startingCell
                    if(Globals.Rnd.Next(100) < chance)
                    {
                        // Randomly pick a direction
                        int direction = Globals.Rnd.Next(4);

                        Point neighbor = new Point(x, y);
                        switch(direction)
                        {
                            case 0: // Up
                                neighbor.Y--;
                                break;
                            case 1: // Right
                                neighbor.X++;
                                break;
                            case 2: // Down
                                neighbor.Y++;
                                break;
                            case 3: // Left
                                neighbor.X--;
                                break;
                        }

                        // If the SelectedNeighborCell is within the maze, remove the wall
                        if(neighbor.X >= 0 && neighbor.X < _mazeCells.GetLength(0) - 2 && neighbor.Y >= 0 && neighbor.Y < _mazeCells.GetLength(1) - 2)
                        {
                            // For the current startingCell
                            if(direction == 0 || direction == 1) // Up or Right
                            {
                                _mazeCells[x, y].Walls[direction] = false;
                            }

                            // For the SelectedNeighborCell startingCell
                            if(direction == 2) // Down: adjust SelectedNeighborCell's Up wall
                            {
                                _mazeCells[neighbor.X, neighbor.Y].Walls[0] = false;
                            }
                            else if(direction == 3) // Left: adjust SelectedNeighborCell's Right wall
                            {
                                _mazeCells[neighbor.X, neighbor.Y].Walls[1] = false;
                            }
                        }
                    }
                }
            }
        }
        #endregion WALLREMOVE
        #endregion METHODS
    }
}
