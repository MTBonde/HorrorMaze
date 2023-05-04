using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HorrorMaze
{
    internal class Pathing : Component
    {
        List<int[]> Open = new List<int[]>();
        List<int[]> closed = new List<int[]>();
        public List<int[]> GetPath(int player_x, int player_y, int monster_x, int monster_y, int map_width, int map_height)
        {
            int[][] map = new int[map_width * map_height][];
            List<int[]> path = new List<int[]>();
            bool path_found = false;
            while (!path_found)
            {
                // check around player poss
                for (int i = 0; i < 8; i++)
                {

                }
                for (int i = 0; i < 8; i++)
                {
                    int[] direction = Direction(i);
                    //if (player_x - direction[0] && player_x - direction[0])
                    //{

                    //}
                }
            }
            return path;
        }
        int[] Direction(int i)
        {
            int[] direction = new int[2];
            switch (i)
            {
                case 0: 
                    direction[0] = -1;
                    direction[1] = -1;
                    return direction;
                case 1:
                    direction[0] = 0;
                    direction[1] = -1;
                    return direction;
                case 2:
                    direction[0] = +1;
                    direction[1] = -1;
                    return direction;
                case 3:
                    direction[0] = -1;
                    direction[1] = 0;
                    return direction;
                case 4:
                    direction[0] = 0;
                    direction[1] = 0;
                    return direction;
                case 5:
                    direction[0] = +1;
                    direction[1] = 0;
                    return direction;
                case 6:
                    direction[0] = -1;
                    direction[1] = +1;
                    return direction;
                case 7:
                    direction[0] = 0;
                    direction[1] = +1;
                    return direction;
                default:
                    direction[0] = +1;
                    direction[1] = +1;
                    return direction;
            }
        }
    }
}
