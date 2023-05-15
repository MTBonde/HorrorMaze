using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HorrorMaze
{
    public static class CollisionManager
    {

        public static List<Collider> colliders = new List<Collider>();

        public static CollisionInfo CheckCircleCollision(Vector3 start, Vector3 end, GameObject go, float radius, float height)
        {
            CollisionInfo col = new CollisionInfo();
            col.collisionPoint = end;
            for (int i = 0; i < colliders.Count; i++)
            {
                CollisionInfo newCol = colliders[i].CheckCylinderCollision(start, end, go, radius, height);
                if (end != newCol.collisionPoint)
                    return newCol;
            }
            return col;
        }
    }
}
