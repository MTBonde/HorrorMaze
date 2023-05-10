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
            Random rnd = new Random();
            //get path
            path = gameObject.GetComponent<Pathing>().GetPath(transform.Position, new Vector2(rnd.Next(5),rnd.Next(5)));
            at_pos = false;
        }
        void Update()
        {
            if (!at_pos)
            {
                //move
                transform.Position += getDirection(transform.Position);
                
                // checks if at next position in path, if so remove it from list.
                if (transform.Position.X - 0.5 >= path[path.Count - 1][0] - 0.005
                 && transform.Position.X - 0.5 <= path[path.Count - 1][0] + 0.005)
                    if (transform.Position.Y - 0.5 >= path[path.Count - 1][1] - 0.005
                     && transform.Position.Y - 0.5 <= path[path.Count - 1][1] + 0.005)
                    {
                        path.RemoveAt(path.Count - 1);
                        // if at the end of path
                        if (path.Count == 0)
                            at_pos = true;
                    }
            }            
        }
        /// <summary>
        /// gets a vector to add to monster vector, based on the direction to the point in path. The vectors lenght is based on speed.
        /// </summary>
        /// <param name="monster"> Monster position </param>
        /// <returns></returns>
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
    }
}
