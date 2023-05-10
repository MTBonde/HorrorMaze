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

        public MazeCell[,] mazeCells;

        #region Astar
        /// <summary>
        /// Returns a list of 2 long arrays of x and y coords. last in list is monster pos
        /// It goes to player pos, though player position is not in list
        /// </summary>
        /// <param name="player_x"> player x coords </param>
        /// <param name="player_y"> player y coords </param>
        /// <param name="monster_x"> monster x coords </param>
        /// <param name="monster_y"> monster y coords </param>
        /// <param name="map_width"> The width of the labyrinth </param>
        /// <param name="map_height"> The height of the labyrinth </param>
        /// <returns></returns>
        public List<int[]> GetPath(Vector2 player, Vector2 monster)
        {
            List<int[]> open = new List<int[]>();
            List<int[]> closed = new List<int[]>();

            int map_width = mazeCells.GetLength(0);
            int map_height = mazeCells.GetLength(1);

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
            bool add_ = false;
            bool wall_check = false;
            int index_found = 0;
            int[] current_one = new int[3] { (int)player.X, (int)player.Y, 1000 };
            while (!path_found)
            {
                #region assign new info
                // check around player poss
                for (int i = 0; i < 4; i++)
                {
                    bool if_data = false;
                    int[] direction = Direction(i);
                    wall_check = false;
                    #region check for walls
                    if (current_one[0] + direction[0] >= 0
                    && current_one[1] + direction[1] >= 0
                    && current_one[1] + direction[1] < map_height 
                    && current_one[0] + direction[0] < map_width)
                    {
                        wall_check = true;
                        switch (i)
                        {
                            case 0: // up
                                if (current_one[1] > 0)
                                    if (mazeCells[current_one[0], current_one[1] - 1].Walls[0])
                                    {
                                        wall_check = false;
                                    }
                                
                                break;
                            case 1: // right
                                if (mazeCells[current_one[0], current_one[1]].Walls[1])
                                {
                                    wall_check = false;
                                }
                                break;
                            case 2: // down
                                if (mazeCells[current_one[0], current_one[1]].Walls[0])
                                {
                                    wall_check = false;
                                }
                                break;
                            case 3: // left
                                if (current_one[0] > 0)
                                    if (mazeCells[current_one[0] - 1, current_one[1]].Walls[1])
                                    {
                                        wall_check = false;
                                    }
                                break;
                        }
                    }
                    #endregion
                    if (wall_check)
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
                            map[index][2] = Distance(current_one[0] + direction[0], current_one[1] + direction[1], (int)monster.X, (int)monster.Y); //g
                            map[index][3] = Distance(current_one[0] + direction[0], current_one[1] + direction[1], (int)player.X, (int)player.Y); //h
                            map[index][4] = map[index][2] + map[index][3]; // F
                            map[index][5] = i; // direction
                            if (map[index][2] == 0)
                            {
                                index_found = index;
                                path_found = true;
                            }
                            int[] new_open = new int[3] { current_one[0] + direction[0], current_one[1] + direction[1], map[index][2] + map[index][3] };
                            open.Add(new_open);
                        }
                    }
                }
                #endregion

                #region close used
                for (int i = 0; i < closed.Count; i++)
                {
                    if (closed[i][0] == current_one[0] && closed[i][1] == current_one[1])
                    {
                        add_ = false;
                        break;
                    }
                }
                if (add_)
                    closed.Add(current_one);
                add_ = true;
                for (int i = 0; i < open.Count; i++)
                {
                    if (open[i][0] == current_one[0] && open[i][1] == current_one[1])
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
                if ((int)player.Y * map_width + (int)player.X == index_found)
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
        #endregion
    }
}
