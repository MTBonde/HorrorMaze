﻿
namespace HorrorMaze
{
    public class Pathing : Component
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

        public List<Point> GetPath(Vector2 player, Vector2 monster)
        {
            List<int[]> open = new List<int[]>();
            List<int[]> closed = new List<int[]>();

            int map_width = mazeCells.GetLength(0);
            int map_height = mazeCells.GetLength(1);

            List<Point> path = new List<Point>();
            if ((int)player.X == (int)monster.X && (int)player.Y == (int)monster.Y)
            {
                Point u = new Point((int)player.X, (int)player.Y);
                path.Add(u);
                return path;
            }
            bool path_found = false;
            bool add_ = false;
            int index_found = 0;
            bool wall_check = false;
            int key_found = 0;
            int key_set = 0;

            int[] current_one = new int[5] { (int)(monster.X), (int)(monster.Y), 1000, 0, 0 };
            int key_id = 1;
            while (!path_found)
            {

                #region assign new info
                // check around player poss
                for (int i = 0; i < 4; i++)
                {
                    bool if_data = false;
                    int[] direction = Direction(i);
                    wall_check = false;

                    if (WallCheck(current_one[0], current_one[1], i, map_width, map_height, direction))
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
                            if (current_one[0] + direction[0] == (int)player.X && current_one[1] + direction[1] == (int)player.Y)
                            {
                                key_found = current_one[3];
                                path_found = true;
                            }
                            int f = Distance(current_one[0] + direction[0], current_one[1] + direction[1], (int)monster.X, (int)monster.Y) +
                                Distance(current_one[0] + direction[0], current_one[1] + direction[1], (int)player.X, (int)player.Y);

                            //key_set = KeyDirection(current_one, map_width, map_height, closed, current_one[3], f);

                            int[] new_closed = new int[5] { current_one[0] + direction[0], current_one[1] + direction[1], f, key_id, current_one[3] };
                            key_id++;
                            open.Add(new_closed);
                        }
                    }
                }
                #endregion

                #region close used
                add_ = true;
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
            bool last_add = true;
            while (!path_added)
            {
                for (int i = 0; i < closed.Count; i++)
                {
                    if (closed[i][3] == key_found)
                    {
                        index_found = i;
                        break;
                    }
                }
                Point cell = new Point(closed[index_found][0], closed[index_found][1]);
                if (last_add == true)
                {
                    last_add = false;
                    Point cell_ = new Point((int)player.X, (int)player.Y);
                    path.Add(cell_);
                }

                key_found = closed[index_found][4];
                if ((int)(monster.X) == closed[index_found][0] && (int)(monster.Y) == closed[index_found][1])
                    path_added = true;
                else
                    path.Add(cell);
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
        bool WallCheck(int player_x, int player_y, int i, int map_width, int map_height, int[] direction)
        {
            bool wall_check = false;
            if (player_x + direction[0] >= 0
                   && player_y + direction[1] >= 0
                   && player_y + direction[1] < map_height
                   && player_x + direction[0] < map_width)
            {
                wall_check = true;
                switch (i)
                {
                    case 0: // up
                        if (player_y > 0)
                            if (mazeCells[player_x, player_y - 1].Walls[0])
                            {
                                wall_check = false;
                            }

                        break;
                    case 1: // right
                        if (mazeCells[player_x, player_y].Walls[1])
                        {
                            wall_check = false;
                        }
                        break;
                    case 2: // down
                        if (mazeCells[player_x, player_y].Walls[0])
                        {
                            wall_check = false;
                        }
                        break;
                    case 3: // left
                        if (player_x > 0)
                            if (mazeCells[player_x - 1, player_y].Walls[1])
                            {
                                wall_check = false;
                            }
                        break;
                }
            }
            return wall_check;
        }
        int KeyDirection(int[] current, int map_width, int map_height, List<int[]> closed, int key, int f_value)
        {
            int key_find = 0;
            if (closed.Count > 0)
                for (int i = 0; i < 4; i++)
                {
                    int[] direction = Direction(i);
                    if (WallCheck(current[0], current[1], i, map_width, map_height, direction))
                    {

                        #region switch
                        switch (i)
                        {
                            case 0:
                                for (int k = 0; k < closed.Count; k++)
                                {
                                    if (closed[k][0] == current[0] && closed[k][1] == current[1] + 1)
                                        key_find = k;
                                }
                                break;
                            case 1:
                                for (int k = 0; k < closed.Count; k++)
                                {
                                    if (closed[k][0] == current[0] + 1 && closed[k][1] == current[1])
                                        key_find = k;
                                }
                                break;
                            case 2:
                                for (int k = 0; k < closed.Count; k++)
                                {
                                    if (closed[k][0] == current[0] && closed[k][1] == current[1] - 1)
                                        key_find = k;
                                }
                                break;
                            case 3:
                                for (int k = 0; k < closed.Count; k++)
                                {
                                    if (closed[k][0] == current[0] + 1 && closed[k][1] == current[1])
                                        key_find = k;
                                }
                                break;
                        }
                        #endregion
                        if (closed[key_find][2] < f_value)
                        {
                            key = key_find;
                            f_value = closed[key_find][2];
                        }

                    }
                }
            return key;
        }
        #endregion
    }
}
