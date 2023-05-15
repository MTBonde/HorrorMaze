using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

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
        //GraphicsDevice _device;

        //VertexBuffer _floorBuffer;
        //Color[] _floorColors = new Color[2] { Color.White, Color.Gray };

        //VertexBuffer _wallBuffer;
        //Vector3[] _wallPoints = new Vector3[8];
        //Color[] _wallColors = new Color[4] { Color.Red, Color.Orange, Color.Red, Color.Orange };
        #endregion

        public MazeCell[,] GenerateMaze(int width, int height)
        {
            _random.Next(1, 10);
            mazeHeight = height;
            mazeWidth = width;
            MazeCells = new MazeCell[mazeWidth, mazeHeight];
            for (int x = 0; x < mazeWidth; x++)
            {
                for (int y = 0; y < mazeHeight; y++)
                {
                    MazeCells[x, y] = new MazeCell();
                }
            }
            MazeCells[0,0].Visited = true;
            return EvaluateCell(new Point(0,0));
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
            return EvaluateCell(MazeStartPoint);
        }

        /// <summary>
        /// Thor 
        /// Evaluate each cell using DFS to create a maze out of the mazecells
        /// </summary>
        /// <param name="cell">is the current cell to evaluate</param>
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
    }
}
