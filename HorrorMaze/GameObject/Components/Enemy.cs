using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HorrorMaze
{
    public class Enemy : Component
    {
        List<int[]> path = new List<int[]>();
        float speed = 0.005f;
        bool at_pos = false;
        void Start()
        {
            path = gameObject.GetComponent<Pathing>().GetPath(transform.Position, new Vector2(3,3));
            at_pos = false;
        }
        void Update()
        {
            if (!at_pos)
            {
                transform.Position += getDirection(transform.Position);

                if (transform.Position.X - 0.5 >= path[path.Count - 1][0] - 0.005
                 && transform.Position.X - 0.5 <= path[path.Count - 1][0] + 0.005)
                    if (transform.Position.Y - 0.5 >= path[path.Count - 1][1] - 0.005
                     && transform.Position.Y - 0.5 <= path[path.Count - 1][1] + 0.005)
                    {
                        path.RemoveAt(path.Count - 1);
                        if (path.Count == 0)
                            at_pos = true;
                    }
            }
            
        }
        Vector2 getDirection(Vector2 monster)
        {
            Vector2 direction = new Vector2(0, 0);
            switch (path[path.Count - 1])
            {
                case int[] n when n[0] < monster.X - 0.5:
                    direction.X -= speed;
                    break;
                case int[] n when n[0] > monster.X - 0.5:
                    direction.X += speed;
                    break;
            }
            switch (path[path.Count - 1])
            {
                case int[] n when n[1] < monster.Y - 0.5:
                    direction.Y -= speed;
                    break;
                case int[] n when n[1] > monster.Y - 0.5:
                    direction.Y += speed;
                    break;
            }
            return direction;
        }
        double distance(float x, float y, float x_2, float y_2)
        {
            return Math.Sqrt(Math.Pow(x - x_2, 2) - Math.Pow(y - y_2, 2));
        }
    }
}
