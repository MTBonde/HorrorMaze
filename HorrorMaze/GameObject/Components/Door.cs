using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HorrorMaze
{
    public class Door : Component
    {



        public void OpenDoor()
        {
            transform.Position3D += new Vector3(0, 0, 1.8f);
        }
    }
}
