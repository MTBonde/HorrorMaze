using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace HorrorMaze
{
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


        private void EvaluateCell(Vector2 cell)
        {
            List<int> neighborCells = new List<int>();
            neighborCells.Add(0);
            neighborCells.Add(1);
            neighborCells.Add(2);
            neighborCells.Add(3);
            while(neighborCells.Count > 0)
            {
                int pick = _random.Next(0, neighborCells.Count);
                int selectedNeighbor = neighborCells[pick];
                neighborCells.RemoveAt(pick);
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
                if(
                    (neighbor.X >= 0) &&
                    (neighbor.X < mazeWidth) &&
                    (neighbor.Y >= 0) &&
                    (neighbor.Y < mazeHeight)
                )
                {
                    if(!MazeCells[(int)neighbor.X, (int)neighbor.Y].Visited)
                    {
                        MazeCells[(int)neighbor.X, (int)neighbor.Y].Visited = true;

                        // Update the current cell's wall
                        if(selectedNeighbor == 1) // Right neighbor
                        {
                            MazeCells[(int)cell.X, (int)cell.Y].Walls[1] = false;
                        }
                        else if(selectedNeighbor == 2) // Bottom neighbor
                        {
                            MazeCells[(int)cell.X, (int)cell.Y].Walls[0] = false;
                        }

                        // Update the corresponding wall of the neighboring cell
                        if(selectedNeighbor == 0) // Left neighbor
                        {
                            MazeCells[(int)neighbor.X, (int)neighbor.Y].Walls[1] = false;
                        }
                        else if(selectedNeighbor == 3) // Up neighbor
                        {
                            MazeCells[(int)neighbor.X, (int)neighbor.Y].Walls[0] = false;
                        }

                        EvaluateCell(neighbor);
                    }
                }
            }
        }
    }
}
