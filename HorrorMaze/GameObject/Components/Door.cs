using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HorrorMaze
{
    public class Door : Component
    {

        Vector3 _openPos, _closePos;
        bool open;

        public void Awake() 
        {
            _closePos = transform.Position3D;
            _openPos = _closePos + new Vector3(0, 0, 1.8f);
        }

        public void OpenDoor()
        {
            if (!open)
                transform.Position3D = _openPos;
        }

        public void CloseDoor()
        {
            if (open)
                transform.Position3D = _closePos;
        }
    }
}
