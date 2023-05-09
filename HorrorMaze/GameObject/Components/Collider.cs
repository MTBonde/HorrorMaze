using SharpDX.MediaFoundation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HorrorMaze
{
    public abstract class Collider : Component
    {



        public Collider()
        {
            CollisionManager.colliders.Add(this);
        }

        public abstract Vector3 CheckPointCollision(Vector3 startPoint, Vector3 endPoint);
        public abstract Vector3 CheckCircleCollision(Vector3 startPoint, Vector3 endPoint, float radius);
    }

    public class BoxCollider : Collider
    {

        public Vector3 size, offset;
        Vector3 cord1, cord2;

        public override Vector3 CheckPointCollision(Vector3 startPoint, Vector3 endPoint)
        {
            cord1 = -(size / 2) + offset + transform.Position3D;
            cord2 = (size / 2) + offset + transform.Position3D;
            if (endPoint.X > cord1.X && endPoint.X < cord2.X)
                if (endPoint.Y > cord1.Y && endPoint.Y < cord2.Y)
                {
                    if (startPoint.X > cord1.X && startPoint.X < cord2.X)
                    {
                        if (startPoint.Y < cord1.Y)
                            return new Vector3(endPoint.X, cord1.Y, endPoint.Z);
                        else
                            return new Vector3(endPoint.X, cord2.Y, endPoint.Z);
                    }
                    else
                    {
                        if (startPoint.X < cord1.X)
                            return new Vector3(cord1.X, endPoint.Y, endPoint.Z);
                        else
                            return new Vector3(cord2.X, endPoint.Y, endPoint.Z);
                    }
                }
            return endPoint;
        }

        public override Vector3 CheckCircleCollision(Vector3 startPoint, Vector3 endPoint, float radius)
        {
            cord1 = -(size / 2) + offset + transform.Position3D;
            cord2 = (size / 2) + offset + transform.Position3D;
            if (endPoint.X > cord1.X - radius && endPoint.X < cord2.X + radius)
                if (endPoint.Y > cord1.Y - radius && endPoint.Y < cord2.Y + radius)
                {
                    if (startPoint.X > cord1.X - radius && startPoint.X < cord2.X + radius)
                    {
                        if (startPoint.Y < transform.Position.Y - radius)
                            return new Vector3(endPoint.X, cord1.Y - radius, endPoint.Z);
                        else
                            return new Vector3(endPoint.X, cord2.Y + radius, endPoint.Z);
                    }
                    else
                    {
                        if (startPoint.X < transform.Position.X - radius)
                            return new Vector3(cord1.X - radius, endPoint.Y, endPoint.Z);
                        else
                            return new Vector3(cord2.X + radius, endPoint.Y, endPoint.Z);
                    }
                }
            return endPoint;
        }
    }

    public class MazeCollider : Collider
    {

        MazeCell[,] _cells;
        public float wallThickness = 0.2f;

        public void SetMaze(MazeCell[,] maze)
        {
            _cells = maze;
        }

        public override Vector3 CheckCircleCollision(Vector3 startPoint, Vector3 endPoint, float radius)
        {
            //needs fix for ends of the walls
            if (transform.Position.X < startPoint.X && transform.Position.X + _cells.GetLength(0) > startPoint.X)
                if (transform.Position.Y < startPoint.Y && transform.Position.Y + _cells.GetLength(1) > startPoint.Y)
                {
                    Vector3 movementVector = endPoint - startPoint;
                    int currentX = (int)(startPoint.X - transform.Position.X);
                    int currentY = (int)(startPoint.Y - transform.Position.Y);
                    if (movementVector.X > 0)
                    {
                        if (_cells[currentX, currentY].Walls[1])
                        {
                            if (currentX + 1 - wallThickness - radius < endPoint.X)
                                endPoint.X = currentX + 1 - wallThickness - radius;
                        }
                    }
                    else
                    {
                        if(currentX > 0)
                        {
                            if (_cells[currentX - 1, currentY].Walls[1])
                            {
                                if (currentX + wallThickness + radius > endPoint.X)
                                    endPoint.X = currentX + wallThickness + radius;
                            }
                        }
                        else
                        {
                            if (currentX + wallThickness + radius > endPoint.X)
                                endPoint.X = currentX + wallThickness + radius;
                        }
                    }
                    if (movementVector.Y > 0)
                    {
                        if (_cells[currentX, currentY].Walls[0])
                        {
                            if (currentY + 1 - wallThickness - radius < endPoint.Y)
                                endPoint.Y = currentY + 1 - wallThickness - radius;
                        }
                    }
                    else
                    {
                        
                        if (currentY > 0)
                        {
                            if (_cells[currentX, currentY - 1].Walls[0])
                            {
                                if (currentY + wallThickness + radius > endPoint.Y)
                                    endPoint.Y = currentY + wallThickness + radius;
                            }
                        }
                        else
                        {
                            if (currentY + wallThickness + radius > endPoint.Y)
                                endPoint.Y = currentY + wallThickness + radius;
                        }
                    }
                }
            return endPoint;
        }

        public override Vector3 CheckPointCollision(Vector3 startPoint, Vector3 endPoint)
        {
            if (transform.Position.X < startPoint.X && transform.Position.X + _cells.GetLength(0) > startPoint.X)
                if (transform.Position.Y < startPoint.Y && transform.Position.Y + _cells.GetLength(1) > startPoint.Y)
                {
                    Vector3 movementVector = endPoint - startPoint;
                    int currentX = (int)(startPoint.X - transform.Position.X) - 1;
                    int currentY = (int)(startPoint.Y - transform.Position.Y) - 1;
                    if (movementVector.X > 0)
                    {
                        if(_cells[currentX, currentY].Walls[1])
                        {
                            if(currentX + 1 - wallThickness < endPoint.X)
                                endPoint.X = currentX + 1 - wallThickness;
                        }
                    }
                    else
                    {
                        if (_cells[currentX - 1, currentY].Walls[1])
                        {
                            if (currentX - 1 + wallThickness < endPoint.X)
                                endPoint.X = currentX - 1 + wallThickness;
                        }
                    }
                    if (movementVector.Y > 0)
                    {
                        if (_cells[currentX, currentY].Walls[0])
                        {
                            if (currentY + 1 - wallThickness < endPoint.Y)
                                endPoint.Y = currentY + 1 - wallThickness;
                        }
                    }
                    else
                    {
                        if (_cells[currentX - 1, currentY].Walls[0])
                        {
                            if (currentY - 1 + wallThickness < endPoint.Y)
                                endPoint.Y = currentY - 1 + wallThickness;
                        }
                    }
                }
            return endPoint;
        }
    }
}
