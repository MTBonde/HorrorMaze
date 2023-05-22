using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HorrorMaze
{
    /// <summary>
    /// an introduction enemy for the tutorial
    /// Niels
    /// </summary>
    public class TutorialEnemy : Component
    {

        Vector3[] path;
        int currentPath;
        float _speed = 2;
        GameObject player;
        float waitTimer;

        /// <summary>
        /// adds all needed components
        /// </summary>
        public void Awake()
        {
            gameObject.name = "Enemy";
            gameObject.AddComponent<BoxCollider>().size = new Vector3(1, 1, 1);
            gameObject.AddComponent<MeshRenderer>().SetModel("3DModels\\ghost_rig");
            gameObject.transform.Position3D = new Vector3(-8.5f, -10.5f, 0);
            BackupAudioSouce enemyAudioSouce = gameObject.AddComponent<BackupAudioSouce>();
            enemyAudioSouce.AddSoundEffect("SoundFX\\zombie1");
            enemyAudioSouce.AddSoundEffect("SoundFX\\zombie2");
            enemyAudioSouce.AddSoundEffect("SoundFX\\zombie3");
            enemyAudioSouce.AddSoundEffect("SoundFX\\zombie4");
            enemyAudioSouce.AddSoundEffect("SoundFX\\zombie5");
            enemyAudioSouce.randomLoopEffects = true;
            enemyAudioSouce.loop = true;
            enemyAudioSouce.Spacial = true;
            enemyAudioSouce.maxDistance = 3f;
            enemyAudioSouce.Play();
            BackupAudioSouce enemyAudioSouce1 = gameObject.AddComponent<BackupAudioSouce>();
            enemyAudioSouce1.SetSoundEffect("SoundFX\\heartBeat");
            enemyAudioSouce1.loop = true;
            enemyAudioSouce1.Spacial = true;
            enemyAudioSouce1.maxDistance = 5f;
            enemyAudioSouce1.Play();
        }

        /// <summary>
        /// sets references
        /// </summary>
        public void Start()
        {
            path = new Vector3[4] { new Vector3(-8.5f, -9.5f, 0), new Vector3(-6.5f, -6.5f, 0) , new Vector3(-6.5f, -3.5f, 0) , new Vector3(0.5f, -3.5f, 0) };
            player = SceneManager.GetGameObjectByName("Player");
        }

        /// <summary>
        /// makes the player follow the path
        /// </summary>
        public void Update()
        {
            if (waitTimer < 2)
            {
                waitTimer += Globals.DeltaTime;

                if (transform.Position3D == path[currentPath])
                {
                    currentPath++;
                    SceneManager.GetGameObjectByName("MonsterDoor").GetComponent<Door>().CloseDoor();
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
                    if (transform.Position3D.X > -6.5f && currentPath == 1)
                    {
                        minLocation.X = path[currentPath].X;
                        maxLocation.X = transform.Position3D.X;
                    }
                    transform.Position3D = Vector3.Clamp(transform.Position3D + (dir * _speed * Globals.DeltaTime), minLocation, maxLocation);
                    transform.Rotation = new Vector3(0, 0, MathHelper.ToDegrees(MathF.Atan2(-dir.X, dir.Y)));
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
                //SceneManager.LoadScene(3);
                SceneManager.LoadScene(6);
        }
    }
}
