using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HorrorMaze
{
    /// <summary>
    /// a basic enemy that goes around the maze and follows the player if it can see it
    /// Niels
    /// </summary>
    public class BackupEnemy : Component
    {

        //the current path
        List<Vector2> path = new List<Vector2>();
        //the spped of the enemy
        float _speed = 1.25f;
        //the player object
        GameObject player;

        /// <summary>
        /// adds all components needed for the enemy
        /// </summary>
        public void Awake()
        {
            gameObject.name = "Enemy";
            gameObject.AddComponent<BoxCollider>().size = new Vector3(0.35f, 0.35f, 1);
            gameObject.AddComponent<MeshRenderer>().SetModel("3DModels\\ghost_rig");
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
        /// sets all references to other objects
        /// </summary>
        public void Start()
        {
            //finds the player so it can look at the players location
            player = SceneManager.GetGameObjectByName("Player");
        }

        public void Update()
        {
            //checks if the player is within range of the monster
            if (player.transform.Position.X > transform.Position.X - 3 &&
                player.transform.Position.X > transform.Position.Y - 3 &&
                player.transform.Position.X < transform.Position.X + 3 &&
                player.transform.Position.X < transform.Position.Y + 3)
            {
                //checks if the monster can see the player
                if (!CollisionManager.RayCast(transform.Position3D, player.transform.Position3D))
                {
                    //sets it to go to the player
                    path = gameObject.GetComponent<BackupPathing>().GetPath(transform.Position.ToPoint(), player.transform.Position.ToPoint());
                    //the path is null if the player is on the same tile as the monster and this makes the monster go to the players exact point then
                    if(path == null)
                    {
                        path = new List<Vector2>();
                        path.Add(player.transform.Position);
                    }
                }
            }
            //checks if theres is a path for the enemy to walk along
            else if (path != null && path.Count > 0)
            {
                //creates its direction vector
                Vector2 dir = path[0] - transform.Position;
                dir.Normalize();
                //finds min andf max location for the clamp (stops the monster from jittering back and forth for because of overshouting)
                Vector2 minLocation = transform.Position;
                Vector2 maxLocation = path[0];
                if (transform.Position.X > path[0].X)
                {
                    minLocation.X = path[0].X;
                    maxLocation.X = transform.Position.X;
                }
                if (transform.Position.Y > path[0].Y)
                {
                    minLocation.Y = path[0].Y;
                    maxLocation.Y = transform.Position.Y;
                }
                //sets is position using a clamp (again uses to stop overshouting)
                transform.Position = Vector2.Clamp(transform.Position + (dir * _speed * Globals.DeltaTime), minLocation, maxLocation);
                //rotates the ghost towards the way it is walking
                transform.Rotation = new Vector3(0, 0, MathHelper.ToDegrees(MathF.Atan2(-dir.X, dir.Y)));
                //checks if we are at the path current closets location and removes it if we are
                if (transform.Position == path[0])
                {
                    path.Remove(path[0]);
                }
            }
            //creates a path for the enemy to a random location if there it has no path 
            else
            {
                Random rnd = new Random();
                path = gameObject.GetComponent<BackupPathing>().GetPath(
                    new Point((int)(transform.Position.X - 0.5f), (int)(transform.Position.Y - 0.5f)),
                    new Point(
                        rnd.Next(0, gameObject.GetComponent<BackupPathing>()._mazeCells.GetLength(0)),
                        rnd.Next(0, gameObject.GetComponent<BackupPathing>()._mazeCells.GetLength(1))));
            }
        }

        /// <summary>
        /// is called when anything is colliding with the enemy
        /// </summary>
        /// <param name="go">the object that is colliding</param>
        public void OnCollision(GameObject go)
        {
            //checks if the players is colliding with it and loads the lose scene if so
            if(go != null)
                if (go.name == "Player")
                    //SceneManager.LoadScene(3);
                    SceneManager.LoadScene(6);
        }
    }
}
