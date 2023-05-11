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
            path = gameObject.GetComponent<Pathing>().GetPath(new Vector2(rnd.Next(10),rnd.Next(10)), transform.Position);
            at_pos = false;
        }
        void Update()
        {
            if (!at_pos)
            {
                
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
                //move
                transform.Position += getDirection(transform.Position);
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
                    if ((monster.X - 0.5) - n[0] < speed)
                        direction.X -= (float)(n[0] - (monster.X - 0.5));
                    else
                        direction.X -= speed;
                    break;
                case int[] n when n[0] > monster.X - 0.5:
                    if (n[0] - (monster.X - 0.5) < speed)
                        direction.X += (float)(n[0] - (monster.X - 0.5));
                    else
                        direction.X += speed;
                    break;
            }
            switch (path[path.Count - 1])
            {
                case int[] n when n[1] < monster.Y - 0.5:
                    if ((monster.Y - 0.5) - n[1]< speed)
                        direction.Y -= (float)(n[1] - (monster.Y - 0.5));
                    else
                        direction.Y -= speed;
                    break;
                case int[] n when n[1] > monster.Y - 0.5:
                    if (n[1] - (monster.Y - 0.5) < speed)
                        direction.Y += (float)(n[1] - (monster.Y - 0.5));
                    else
                        direction.Y += speed;
                    break;
            }
            //Random rnd = new Random();
            //switch (rnd.Next(10))
            //{
            //    case 0:
            //        direction.X = -speed;
            //        break;
            //    case 1:
            //        direction.X = speed;
            //        break;
            //    case 2:
            //        direction.Y = -speed;
            //        break;
            //    case 3:
            //        direction.Y = speed;
            //        break;
            //}
            return direction;
        }
    }
}
