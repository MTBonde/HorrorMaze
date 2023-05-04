﻿using System;
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
        public MazeCell[,] MazeCells = new MazeCell[mazeWidth, mazeHeight];
        public const int mazeWidth = 20;
        public const int mazeHeight = 20;
        //GraphicsDevice _device;

        //VertexBuffer _floorBuffer;
        //Color[] _floorColors = new Color[2] { Color.White, Color.Gray };

        //VertexBuffer _wallBuffer;
        //Vector3[] _wallPoints = new Vector3[8];
        //Color[] _wallColors = new Color[4] { Color.Red, Color.Orange, Color.Red, Color.Orange };
        #endregion


        /// <summary>
        /// Thor 
        /// Evaluate each cell using DFS to create a maze out of the mazecells
        /// </summary>
        /// <param name="cell">is the current cell to evaluate</param>
        private MazeCell[,] EvaluateCell(Vector2 cell)
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
                Vector2 neighbor = cell;
                switch(selectedNeighbor)
                {
                    case 0:
                        neighbor += new Vector2(0, -1);
                        break;
                    case 1:
                        neighbor += new Vector2(1, 0);
                        break;
                    case 2:
                        neighbor += new Vector2(0, 1);
                        break;
                    case 3:
                        neighbor += new Vector2(-1, 0);
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
                    if(!MazeCells[(int)neighbor.X, (int)neighbor.Y].Visited)
                    {
                        // mark the neighbor as visited
                        MazeCells[(int)neighbor.X, (int)neighbor.Y].Visited = true;

                        // Update the current cell's wall
                        if(selectedNeighbor == 1) // Right neighbor
                        {
                            // Remove the right wall of the current cell
                            MazeCells[(int)cell.X, (int)cell.Y].Walls[1] = false;
                        }
                        else if(selectedNeighbor == 2) // Bottom neighbor
                        {
                            // Remove the bottom wall of the current cell
                            MazeCells[(int)cell.X, (int)cell.Y].Walls[0] = false;
                        }

                        // Update the corresponding wall of the neighboring cell
                        if(selectedNeighbor == 0) // Left neighbor
                        {
                            // Remove the right wall of the neighboring cell
                            MazeCells[(int)neighbor.X, (int)neighbor.Y].Walls[1] = false;
                        }
                        else if(selectedNeighbor == 3) // Up neighbor
                        {
                            // Remove the bottom wall of the neighboring cell
                            MazeCells[(int)neighbor.X, (int)neighbor.Y].Walls[0] = false;
                        }

                        // Recursively evaluate the neighboring cell untill all cells are marked as visited
                        EvaluateCell(neighbor);
                    }
                }
            }
            return MazeCells;
        }
    }
}
