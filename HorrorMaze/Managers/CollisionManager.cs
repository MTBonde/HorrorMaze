using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HorrorMaze
{
    public class CollisionManager
    {

        public static List<Collider> colliders = new List<Collider>();

        public static Vector3 CheckCircleCollision(Vector3 start, Vector3 end, float radius)
        {
            for (int i = 0; i < colliders.Count; i++)
            {
                Vector3 newEnd = colliders[i].CheckCircleCollision(start, end, radius);
                if (end != newEnd)
                    return newEnd;
            }
            return end;
        }
    }
}
