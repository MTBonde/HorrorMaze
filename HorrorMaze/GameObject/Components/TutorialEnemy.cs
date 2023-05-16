using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HorrorMaze
{
    public class TutorialEnemy : Component
    {

        Vector3[] path;
        int currentPath;
        float _speed = 2;

        public void Start()
        {
            path = new Vector3[4] { new Vector3(-8.5f, -8.5f, 0) , new Vector3(-6.5f, -6.5f, 0) , new Vector3(-6.5f, -3.5f, 0) , new Vector3(0.5f, -3.5f, 0) };
        }

        public void Update()
        {
            if (currentPath < path.Length)
            {
                Vector3 dir = path[currentPath] - transform.Position3D;
                dir.Normalize();
                transform.Position3D = Vector3.Clamp(transform.Position3D + (dir * _speed * Globals.DeltaTime), transform.Position3D, path[currentPath]);
                if (transform.Position3D == path[currentPath])
                {
                    currentPath++;
                }
            }
        }

        public void OnCollision(GameObject go)
        {
            if (go.name == "Player")
                SceneManager.LoadScene(6);
        }
    }
}
