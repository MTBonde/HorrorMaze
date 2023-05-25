namespace HorrorMaze
{
    /// <summary>     
    /// This class takes care of generating a maze using DFS algoritm
    /// by Thor
    /// </summary>
    public class Maze
    {
        #region Fields
        private Random _random = new Random();
        public MazeCell[,] MazeCells;
        private int mazeWidth;
        private int mazeHeight;
        #endregion

        public Maze(int width, int height)
        {
            _random.Next(1, 100);
            mazeWidth = width;
            mazeHeight = height;
            MazeCells = new MazeCell[mazeWidth, mazeHeight];
            for(int i = 0; i < mazeWidth; i++)
            {
                for(int j = 0; j < mazeHeight; j++)
                {
                    MazeCells[i, j] = new MazeCell();
                }
            }
        }

        public MazeCell[,] GenerateMazeFromMaze(MazeCell[,] maze, Point MazeStartPoint)
        {
            _random.Next(1, 10);
            MazeCells = maze;
            mazeWidth = maze.GetLength(0);
            mazeHeight = maze.GetLength(1);
            //for (int x = 0; x < mazeWidth; x++)
            //{
            //    for (int y = 0; y < mazeHeight; y++)
            //    {
            //        MazeCells[x, y] = new MazeCell();
            //    }
            //}

            //AddRooms(10);
            KnockDownRandomWalls(2);
            MazeCells = AddRoomBeforeMaze(new Point(5, 5), 3, 3, 4, MazeCells);
            MazeCells = AddRoomBeforeMaze(new Point(9, 5), 3, 3, 4, MazeCells);
            return EvaluateCell(MazeStartPoint);
        }

   

        //    return MazeCells;
        //}

        private void AddRooms()
        {
            // set the number of rooms based on overall maze size
            int totalCells = mazeWidth * mazeHeight;
            // one 1 room per 10 maze cells
            int numberOfRooms = totalCells / 10;

            // Add open spaces
            AddOpenSpace(3, 3, numberOfRooms);
        }

        public MazeCell[,] AddRoomBeforeMaze(Point buttomLeftCornerOffRoom, int width, int height, int entances, MazeCell[,] mazeToEdit)
        {
            for (int x = buttomLeftCornerOffRoom.X; x < buttomLeftCornerOffRoom.X + width; x++)
            {
                for (int y = buttomLeftCornerOffRoom.Y; y < buttomLeftCornerOffRoom.Y + height; y++)
                {
                    if(y != buttomLeftCornerOffRoom.Y + height - 1)
                        mazeToEdit[x, y].Walls[0] = false;
                    if(x != buttomLeftCornerOffRoom.X + width - 1)
                        mazeToEdit[x, y].Walls[1] = false;
                    mazeToEdit[x, y].Visited = true;
                }
            }
            while(entances > 0)
            {
                int side = Globals.Rnd.Next(0,2);
                int x = 0;
                int y = 0;
                if (side == 0)
                {
                    side = Globals.Rnd.Next(0, 2);
                    x = Globals.Rnd.Next(buttomLeftCornerOffRoom.X,buttomLeftCornerOffRoom.X + width);
                    if(buttomLeftCornerOffRoom.Y > 0 && side == 0)
                        y = buttomLeftCornerOffRoom.Y - 1;
                    else if(side == 1)
                        y = buttomLeftCornerOffRoom.Y + height - 1;
                    else
                        continue;
                    if (mazeToEdit[x, y].Walls[0])
                    {
                        mazeToEdit[x, y].Walls[0] = false;
                        entances--;
                    }
                }
                else if (side == 1)
                {
                    side = Globals.Rnd.Next(0, 2);
                    y = Globals.Rnd.Next(buttomLeftCornerOffRoom.Y,buttomLeftCornerOffRoom.Y + height);
                    if (buttomLeftCornerOffRoom.X > 0 && side == 0)
                        x = buttomLeftCornerOffRoom.X - 1;
                    else if (side == 1)
                        x = buttomLeftCornerOffRoom.X + width - 1;
                    else
                        continue;
                    if(mazeToEdit[x, y].Walls[1])
                    {
                        mazeToEdit[x, y].Walls[1] = false;
                        entances--;
                    }
                }
            }
            return mazeToEdit;
        }

        public void AddRoomAfterMaze(Point buttomLeftCornerOffRoom, int width, int height)
        {

        }

        /// <summary>
        /// Evaluate each cell using DFS to create a maze out of the mazecells
        /// by thor
        /// </summary>
        /// <param name="cell">is the starting cell to evaluate</param>
        private MazeCell[,] EvaluateCell(Point cell)
        {
            // create a list of neoghboring cells 
            List<int> neighborCells = new List<int>();
            neighborCells.Add(0); // up
            neighborCells.Add(1); // Right
            neighborCells.Add(2); // Down
            neighborCells.Add(3); // Left

            // While there is still an unvisited neighbor cell do this
            while(neighborCells.Count > 0)
            {
                // Randomly pick a neighbor from the list and set is as selected, and remove the selected from the list
                int pick = _random.Next(0, neighborCells.Count);
                int selectedNeighbor = neighborCells[pick];
                neighborCells.RemoveAt(pick);

                // Find the coordinate of the neighbor
                Point neighbor = cell;
                switch(selectedNeighbor)
                {
                    case 0:
                        neighbor += new Point(0, -1);
                        break;
                    case 1:
                        neighbor += new Point(1, 0);
                        break;
                    case 2:
                        neighbor += new Point(0, 1);
                        break;
                    case 3:
                        neighbor += new Point(-1, 0);
                        break;
                }
                // make sure the neighbor is within the maze
                if(
                    (neighbor.X >= 0) &&
                    (neighbor.X < mazeWidth) &&
                    (neighbor.Y >= 0) &&
                    (neighbor.Y < mazeHeight)
                )
                {
                    // if the neighbor has not been visited
                    if(!MazeCells[neighbor.X, neighbor.Y].Visited)
                    {
                        // mark the neighbor as visited
                        MazeCells[neighbor.X, neighbor.Y].Visited = true;

                        // Update the current cell's wall
                        if(selectedNeighbor == 1) // Right neighbor
                        {
                            // Remove the right wall of the current cell
                            MazeCells[cell.X, cell.Y].Walls[1] = false;
                        }
                        else if(selectedNeighbor == 2) // up neighbor
                        {
                            // Remove the bottom wall of the current cell
                            MazeCells[cell.X, cell.Y].Walls[0] = false;
                        }

                        // Update the corresponding wall of the neighboring cell
                        if(selectedNeighbor == 0) // buttom neighbor
                        {
                            // Remove the right wall of the neighboring cell
                            MazeCells[neighbor.X, neighbor.Y].Walls[0] = false;
                        }
                        else if(selectedNeighbor == 3) // left neighbor
                        {
                            // Remove the bottom wall of the neighboring cell
                            MazeCells[neighbor.X, neighbor.Y].Walls[1] = false;
                        }

                        // Recursively evaluate the neighboring cell untill all cells are marked as visited
                        EvaluateCell(neighbor);
                    }
                }
            }
            //if no more 
            return MazeCells;
        }

        /// <summary>
        /// Remove walls randomly in the maze to create more routes to goal
        /// </summary>
        /// <param name="chance">percent change for a wall to be removed</param>
        public void KnockDownRandomWalls(int chance)
        {
            for(int x = 0; x < mazeWidth; x++)
            {
                for(int y = 0; y < mazeHeight; y++)
                {
                    // Randomly decide if we should create an extra path at this cell
                    if(_random.Next(100) < chance)
                    {
                        // Randomly pick a direction
                        int direction = _random.Next(4);

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

                        // If the neighbor is within the maze, remove the wall
                        if(neighbor.X >= 0 && neighbor.X < mazeWidth - 1 && neighbor.Y >= 0 && neighbor.Y < mazeHeight - 1)
                        {
                            // For the current cell
                            if(direction == 0 || direction == 1) // Up or Right
                            {
                                MazeCells[x, y].Walls[direction] = false;
                            }

                            // For the neighbor cell
                            if(direction == 2) // Down: adjust neighbor's Up wall
                            {
                                MazeCells[neighbor.X, neighbor.Y].Walls[0] = false;
                            }
                            else if(direction == 3) // Left: adjust neighbor's Right wall
                            {
                                MazeCells[neighbor.X, neighbor.Y].Walls[1] = false;
                            }
                        }

                    }
                }
            }
        }

        /// <summary>
        /// Calculate rooms by dividing total number of cells with a dividing factor
        /// </summary>
        /// <param name="numberUsedForRoomCalculation">1 room pr x openspaces</param>
        private void AddRooms(int numberUsedForRoomCalculation)
        {
            // set the number of rooms based on overall maze size
            int totalCells = mazeWidth * mazeHeight;
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
                    int startX = _random.Next(0, mazeWidth - width);
                    int startY = _random.Next(0, mazeHeight - height);

                    // Check if the open space area is unvisited aka has a room
                    bool canPlace = true;
                    for(int x = startX; x < startX + width; x++)
                    {
                        for(int y = startY; y < startY + height; y++)
                        {
                            if(MazeCells[x, y].Visited)
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
                                MazeCells[x, y].Visited = true;
                                // Remove walls 
                                if(x > startX) MazeCells[x, y].Walls[1] = false;
                                if(y > startY) MazeCells[x, y].Walls[0] = false;
                            }
                        }
                        break;
                    }

                    attempt++;
                }
            }
        }

    }
}
