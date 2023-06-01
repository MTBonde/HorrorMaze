

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
        // Wall texture
        public int[] wallmodel = new int[2] { 0, 0 };

    }
}
