using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HorrorMaze
{
    public class Staircase : Component
    {



        public void OnCollision()
        {
            SceneManager.GetGameObjectByName("Player").transform.Position3D -= new Vector3(0, 0, 2);
        }
    }
}
