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
        GameObject player;
        float waitTimer;

        public void Start()
        {
            path = new Vector3[4] { new Vector3(-8.5f, -9.5f, 0), new Vector3(-6.5f, -6.5f, 0) , new Vector3(-6.5f, -3.5f, 0) , new Vector3(0.5f, -3.5f, 0) };
            player = SceneManager.GetGameObjectByName("Player");
        }

        public void Update()
        {
            if (waitTimer < 2)
            {
                waitTimer += Globals.DeltaTime;

                if (transform.Position3D == path[currentPath])
                {
                    currentPath++;
                }
                else if(currentPath == 0)
                {
                    Vector3 dir = path[currentPath] - transform.Position3D;
                    dir.Normalize();
                    Vector3 minLocation = transform.Position3D;
                    Vector3 maxLocation = path[currentPath];
                    transform.Position3D = Vector3.Clamp(transform.Position3D + (dir * _speed * Globals.DeltaTime), minLocation, maxLocation);
                }
            }
            else if (currentPath < path.Length)
            {
                if (player.transform.Position3D.Y > -5)
                {
                    Vector3 dir = path[currentPath] - transform.Position3D;
                    dir.Normalize();
                    Vector3 minLocation = transform.Position3D;
                    Vector3 maxLocation = path[currentPath];
                    if (transform.Position3D.X > -6.5f && currentPath == 0)
                    {
                        minLocation.X = path[currentPath].X;
                        maxLocation.X = transform.Position3D.X;
                    }
                    transform.Position3D = Vector3.Clamp(transform.Position3D + (dir * _speed * Globals.DeltaTime), minLocation, maxLocation);
                    if (transform.Position3D == path[currentPath])
                    {
                        currentPath++;
                    }
                }
                else
                {
                    Vector3 dir = player.transform.Position3D - transform.Position3D;
                    dir.Normalize();
                    transform.Position3D += new Vector3(dir.X,dir.Y,0) * _speed * Globals.DeltaTime;
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
