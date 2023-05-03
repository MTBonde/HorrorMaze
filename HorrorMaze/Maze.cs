using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace HorrorMaze
{
    internal class Maze
    {
        #region Fields
        private Random rand = new Random();
        //public MazeCell[,] MazeCells = new MazeCell[mazeWidth, mazeHeight];
        public const int mazeWidth = 20;
        public const int mazeHeight = 20;
        GraphicsDevice device;

        VertexBuffer floorBuffer;
        Color[] floorColors = new Color[2] { Color.White, Color.Gray };

        VertexBuffer wallBuffer;
        Vector3[] wallPoints = new Vector3[8];
        Color[] wallColors = new Color[4] { Color.Red, Color.Orange, Color.Red, Color.Orange };
        #endregion

        #region Maze Generation
        public void GenerateMaze()
        {
            for(int x = 0; x < mazeWidth; x++)
            {

                for(int z = 0; z < mazeHeight; z++)
                {
                    //MazeCells[x, z].Walls[0] = true;
                    MazeCells[x, z].Walls[1] = true;
                    MazeCells[x, z].Walls[2] = true;
                    //MazeCells[x, z].Walls[3] = true;
                    MazeCells[x, z].Visited = false;
                }
            }

            MazeCells[0, 0].Visited = true;
            EvaluateCell(new Vector2(0, 0));
        }

        private void EvaluateCell(Vector2 cell)
        {
            List<int> neighborCells = new List<int>();
            neighborCells.Add(0);
            neighborCells.Add(1);
            neighborCells.Add(2);
            neighborCells.Add(3);
            while(neighborCells.Count > 0)
            {
                int pick = rand.Next(0, neighborCells.Count);
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
                    if(!MazeCells[(int)neighbor.X, (int)neighbor.Y].
                    Visited)
                    {
                        MazeCells[(int)neighbor.X, (int)neighbor.Y].Visited = true;


                        MazeCells[(int)cell.X, (int)cell.Y].Walls[selectedNeighbor] = false;

                        //MazeCells[(int)neighbor.X, (int)neighbor.Y].Walls[(selectedNeighbor + 2) % 4] = false;
                        MazeCells[(int)neighbor.X, (int)neighbor.Y].Walls[selectedNeighbor] = false;

                        EvaluateCell(neighbor);
                    }
                }
            }
        }
        #endregion
    }
}
