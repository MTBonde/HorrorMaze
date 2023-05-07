using Microsoft.VisualBasic.ApplicationServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace HorrorMaze
{
    internal class Pathing : Component
    {
        static List<int[]> open = new List<int[]>();
        static List<int[]> closed = new List<int[]>();
        public static List<int[]> GetPath(int player_x, int player_y, int monster_x, int monster_y, int map_width, int map_height)
        {
            #region assign mapt tiles
            int[][] map = new int[map_width * map_height][];
            int x_1 = 0;
            int y_1 = 0;
            for (int i = 0; i < map_width * map_height; i++)
            {
                int[] map_pos = new int[6]; // 2: F, 3: G, 4: H, 5: path
                map_pos[0] = x_1;
                map_pos[1] = y_1;
                if (x_1 == map_width - 1)
                {
                    x_1 = 0;
                    y_1 += 1;
                }
                else
                {
                    x_1 += 1;
                }
                map[i] = map_pos;
            }
            #endregion
            List<int[]> path = new List<int[]>();
            bool path_found = false;
            int index_found = 0;
            int[] current_one = new int[3] { player_x, player_y, 1000 };
            while (!path_found)
            {
                #region assign new info
                // check around player poss
                for (int i = 0; i < 4; i++)
                {
                    bool if_data = false;
                    int[] direction = Direction(i);

                    if (current_one[0] + direction[0] >= 0 && current_one[1] + direction[1] >= 0 && (current_one[1] + direction[1]) * map_width + current_one[0] + direction[0] < map_width * map_height)
                    {
                        for (int i_2 = 0; i_2 < closed.Count; i_2++) //check if theres data
                        {
                            if (closed[i_2][0] == current_one[0] + direction[0]
                             && closed[i_2][1] == current_one[1] + direction[1]) // if theres data
                            {
                                if_data = true;
                            }

                        }
                        if (!if_data)
                        {
                            int index = (current_one[1] + direction[1]) * map_width + current_one[0] + direction[0];
                            //add f, g, h and direction
                            map[index][2] = Distance(current_one[0] + direction[0], current_one[1] + direction[1], monster_x, monster_y); //g
                            map[index][3] = Distance(current_one[0] + direction[0], current_one[1] + direction[1], player_x, player_y); //h
                            map[index][4] = map[index][2] + map[index][3]; // F
                            map[index][5] = i; // direction
                            if (map[index][2] == 0)
                            {
                                index_found = index;
                                path_found = true;
                            }
                            int[] new_closed = new int[3] { current_one[0] + direction[0], current_one[1] + direction[1], map[index][2] + map[index][3] };
                            open.Add(new_closed);
                        }
                    }
                }
                #endregion

                #region close used
                closed.Add(current_one);
                for (int i = 0; i < open.Count; i++)
                {
                    if (open[i] == current_one)
                    {
                        open.RemoveAt(i);
                        break;
                    }
                }
                #endregion

                #region chose opens
                int index_lowest = 0;
                int lowest = open[0][2];
                for (int i = 0; i < open.Count; i++)
                {
                    if (open[i][2] < lowest)
                    {
                        lowest = open[i][2];
                        index_lowest = i;
                    }
                }
                current_one = open[index_lowest];

                #endregion
            }

            #region assign path to list
            bool path_added = false;
            while (!path_added)
            {
                int[] cell = new int[2] { map[index_found][0], map[index_found][1] };
                path.Add(cell);
                switch (map[index_found][5])
                {
                    case 0:
                        index_found = index_found + map_width;
                        break;
                    case 1:
                        index_found--;
                        break;
                    case 2:
                        index_found = index_found - map_width;
                        break;
                    case 3:
                        index_found++;
                        break;
                }
                if (player_y * map_width + player_x == index_found)
                    path_added = true;
            }
            #endregion

            return path;
        }
        #region bag methods
        static int Distance(int start_x, int start_y, int end_x, int end_y)
        {
            int x = start_x - end_x;
            int y = start_y - end_y;
            if (x < 0)
            {
                x = x * -1;
            }
            if (y < 0)
            {
                y = y * -1;
            }
            return x + y;
        }
        static int[] Direction(int i)
        {
            #region direction switch
            int[] direction = new int[2];
            switch (i)
            {
                case 0:
                    direction[0] = 0;
                    direction[1] = -1;
                    return direction;
                case 1:
                    direction[0] = +1;
                    direction[1] = 0;
                    return direction;
                case 2:
                    direction[0] = 0;
                    direction[1] = +1;
                    return direction;
                case 3:
                    direction[0] = -1;
                    direction[1] = 0;
                    return direction;
                default:
                    direction[0] = 0;
                    direction[1] = 0;
                    return direction;
            }
            #endregion
        }
        #endregion

    }
}
