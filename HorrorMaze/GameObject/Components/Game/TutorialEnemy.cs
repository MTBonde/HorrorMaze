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
        public bool wait = false;

        /// <summary>
        /// adds all needed components
        /// </summary>
        public void Awake()
        {
            gameObject.name = "Boulder";
            gameObject.AddComponent<BoxCollider>().size = new Vector3(1, 1, 1);
            gameObject.AddComponent<MeshRenderer>().SetModel("3DModels\\boulder");
            gameObject.transform.Position3D = new Vector3(-7.5f, -3.5f, 3.25f);
            BackupAudioSouce spawnSound = gameObject.AddComponent<BackupAudioSouce>();
            spawnSound.SetSoundEffect("SoundFX\\boulder_spawn");
            spawnSound.Spacial = true;
            spawnSound.Play();
        }

        /// <summary>
        /// sets references
        /// </summary>
        public void Start()
        {
            path = new Vector3[2] { new Vector3(-6.5f, -3.5f, 0.75f) , new Vector3(0.5f, -3.5f, 0.75f) };
            player = SceneManager.GetGameObjectByName("Player");
        }

        bool firstFrame = true;

        /// <summary>
        /// makes the player follow the path
        /// </summary>
        public void Update()
        {
            if (firstFrame)
            {
                Awake();
                Start();
                firstFrame = false;
            }
            if (currentPath < path.Length && !wait)
            {
                Vector3 dir = path[currentPath] - transform.Position3D;
                dir.Normalize();
                Vector3 minLocation = transform.Position3D;
                Vector3 maxLocation = path[currentPath];
                minLocation.Z = path[currentPath].Z;
                maxLocation.Z = transform.Position3D.Z;
                transform.Position3D = Vector3.Clamp(transform.Position3D + (dir * _speed * Globals.DeltaTime), minLocation, maxLocation);
                transform.Rotation += new Vector3(0, 100 * Globals.DeltaTime, 0);
                //transform.Rotation = new Vector3(0, 0, MathHelper.ToDegrees(MathF.Atan2(-dir.X, dir.Y)));
                if (transform.Position3D == path[currentPath])
                {
                    currentPath++;
                    wait = true;
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
