using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HorrorMaze
{
    public class PlayerBoulderAnimation : Component
    {

        float time = 0, timer = 1.75f;

        public void Update()
        {
            if(time < timer)
            {
                time += Globals.DeltaTime;
                transform.Rotation += new Vector3(0,0,(360 / timer) * Globals.DeltaTime);
            }
            else
            {
                gameObject.GetComponent<PlayerController>().enabled = true;
                SceneManager.GetGameObjectByName("Boulder").GetComponent<TutorialEnemy>().wait = false;
                enabled = false;
            }
        }
    }
}
