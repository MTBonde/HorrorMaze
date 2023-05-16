using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HorrorMaze
{

    public class BackupPathing : Component
    {
        private class AStarCell
        {
            public int gCost = int.MaxValue;
            public int fCost = int.MaxValue;
            public AStarCell cameFrom;
            public Point cord;
        }

        public MazeCell[,] _mazeCells;
        AStarCell[,] aStarInfo;
        Point goal;

        public void SetMaze(MazeCell[,] maze)
        {
            _mazeCells = maze;
            aStarInfo = new AStarCell[maze.GetLength(0),maze.GetLength(1)];
            for (int x = 0; x < maze.GetLength(0); x++)
            {
                for (int y = 0; y < maze.GetLength(1); y++)
                {
                    aStarInfo[x, y] = new AStarCell();
                    aStarInfo[x, y].cord = new Point(x,y);
                }
            }
        }

        public List<Vector2> GetPath(Point startPos, Point endPos)
        {
            if (startPos == endPos)
                return null;
            for (int x = 0; x < _mazeCells.GetLength(0); x++)
            {
                for (int y = 0; y < _mazeCells.GetLength(1); y++)
                {
                    aStarInfo[x, y] = new AStarCell();
                    aStarInfo[x, y].cord = new Point(x, y);
                }
            }
            goal = endPos;
            List<Point> open = new List<Point>() { startPos };

            aStarInfo[startPos.X, startPos.Y].gCost = 0;
            aStarInfo[startPos.X, startPos.Y].fCost = GetDistanceToGoal(startPos);

            while (open.Count > 0)
            {
                //finds the point closest to goal without walls
                Point current = open[0];
                for (int i = 1; i < open.Count; i++)
                {
                    if (aStarInfo[current.X, current.Y].fCost > aStarInfo[open[i].X, open[i].Y].fCost)
                        current = open[i];
                }
                open.Remove(current);
                //checks if w found goal
                if (current == goal)
                    return ReconstructPath(current);
                //finds neigbours
                List<Point> neighbours = new List<Point>();
                if (current.X > 0)
                    neighbours.Add(current - new Point(1, 0));
                if (current.X < _mazeCells.GetLength(0) - 1)
                    neighbours.Add(current + new Point(1, 0));
                if (current.Y > 0)
                    neighbours.Add(current - new Point(0, 1));
                if (current.Y < _mazeCells.GetLength(1) - 1)
                    neighbours.Add(current + new Point(0, 1));
                for (int i = 0; i < neighbours.Count; i++)
                {
                    if (aStarInfo[current.X,current.Y].gCost + 10 < aStarInfo[neighbours[i].X, neighbours[i].Y].gCost)
                    {
                        if (current.X > neighbours[i].X)
                        {
                            if (_mazeCells[current.X - 1, current.Y].Walls[1])
                                continue;
                        }
                        else if (current.X < neighbours[i].X)
                        {
                            if (_mazeCells[current.X, current.Y].Walls[1])
                                continue;
                        }
                        else if (current.Y > neighbours[i].Y)
                        {
                            if (_mazeCells[current.X, current.Y - 1].Walls[0])
                                continue;
                        }
                        else if (current.Y < neighbours[i].Y)
                        {
                            if (_mazeCells[current.X, current.Y].Walls[0])
                                continue;
                        }
                        aStarInfo[neighbours[i].X, neighbours[i].Y].cameFrom = aStarInfo[current.X,current.Y];
                        aStarInfo[neighbours[i].X, neighbours[i].Y].gCost = aStarInfo[current.X, current.Y].gCost + 10;
                        aStarInfo[neighbours[i].X, neighbours[i].Y].fCost = GetDistanceToGoal(neighbours[i]);
                        if (!open.Contains(neighbours[i]))
                            open.Add(neighbours[i]);
                    }
                }
            }
            Debug.WriteLine("No Route");
            return null;
        }

        public List<Vector2> ReconstructPath(Point end)
        {
            List<Vector2> path = new List<Vector2>();
            Point current = goal;
            while (aStarInfo[current.X,current.Y].cameFrom != null)
            {
                path.Add(current.ToVector2() + new Vector2(0.5f, 0.5f));
                current = aStarInfo[current.X, current.Y].cameFrom.cord;
            }
            path.Reverse();
            return path;
        }

        private int GetDistanceToGoal(Point startPos)
        {
            int distance = 0;
            int currentX = startPos.X, currentY = startPos.Y;
            while(currentX != goal.X)
            {
                distance += 10;
                if (currentX < goal.X)
                    currentX++;
                else
                    currentX--;
            }
            while(currentY != goal.Y)
            {
                distance += 10;
                if(currentY < goal.Y)
                    currentY++;
                else
                    currentY--;
            }
            return distance;
        }
    }
}
