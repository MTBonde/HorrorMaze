using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HorrorMaze
{
    public class Staircase : Component
    {



        public void OnCollision(GameObject go)
        {
            Debug.WriteLine(go.name);
            if (go.name == "Player")
                SceneManager.GetGameObjectByName("MazeController").GetComponent<MazeController>().FloorDown();
        }
    }
}
