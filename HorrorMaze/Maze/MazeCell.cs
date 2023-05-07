using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HorrorMaze
{
    /// <summary>    
    /// This class defines a mazecell
    /// by Thor
    /// </summary>
    public class MazeCell
    {
        // Array of wals with up and right added
        public bool[] Walls = new bool[2] { true, true }; 
        // set mazecells to not visited from the beginning
        public bool Visited = false;
    }
}
