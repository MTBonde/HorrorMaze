
namespace HorrorMaze
{
    public class WanderingEnemy : Enemy
    {
        List<Point> path = new List<Point>();
        List<Point> path_sec = new List<Point>();
        float speed = 3f;
        bool at_pos = true;
        bool hunting = false;
        AudioSouce scream;
        GameObject player;
        public void Start()
        {
            GetPath();
            path = path_sec;
            GetPath();
        }
        public void Awake()
        {
            //gameObject.AddComponent<BoxCollider>().size = new Vector3(0.35f, 0.35f, 1);
            //gameObject.AddComponent<MeshRenderer>().SetModel("3DModels\\ghost_rig");
            //AudioSouce GrudgeAudioSouce = gameObject.AddComponent<AudioSouce>();
            //GrudgeAudioSouce.AddSoundEffect("SoundFX\\zombie1");
            //GrudgeAudioSouce.AddSoundEffect("SoundFX\\zombie2");
            //GrudgeAudioSouce.AddSoundEffect("SoundFX\\zombie3");
            //GrudgeAudioSouce.AddSoundEffect("SoundFX\\zombie4");
            //GrudgeAudioSouce.AddSoundEffect("SoundFX\\zombie5");
            //GrudgeAudioSouce.randomLoopEffects = true;
            //GrudgeAudioSouce.loop = true;
            //GrudgeAudioSouce.Spacial = true;
            //GrudgeAudioSouce.maxDistance = 3f;
            //GrudgeAudioSouce.Play();
            //AudioSouce HeartBeatAudioSouce = gameObject.AddComponent<AudioSouce>();
            //HeartBeatAudioSouce.SetSoundEffect("SoundFX\\heartBeat");
            //HeartBeatAudioSouce.loop = true;
            //HeartBeatAudioSouce.Spacial = true;
            //HeartBeatAudioSouce.maxDistance = 5f;
            //HeartBeatAudioSouce.Play();
            //scream = gameObject.AddComponent<AudioSouce>();
            //scream.SetSoundEffect("SoundFX\\chasing_scream_1");
            //scream.Spacial = true;
            //scream.maxDistance = 5;
            //player = SceneManager.GetGameObjectByName("Player");
        }
        public void GetPath()
        {
            Random rnd = new Random();
            bool chosen = false;
            bool if_add = true;
            float x = 0;
            float y = 0;
            if(hunting)
            {
                Vector3 playerPos = player.transform.Position3D;
                if((int)playerPos.X != (int)(transform.Position.Y) || (int)playerPos.Y != (int)(transform.Position.Y))
                {
                    x = (int)playerPos.X;
                    y = (int)playerPos.Y;
                }
                else
                    if_add = false;
            }
            else
            {
                while(!chosen)
                {
                    x = rnd.Next(gameObject.GetComponent<Pathing>().mazeCells.GetLength(0));
                    y = rnd.Next(gameObject.GetComponent<Pathing>().mazeCells.GetLength(1));
                    if(x != (int)(transform.Position.X) && y != (int)(transform.Position.Y))
                        chosen = true;
                }
            }
            if(path.Count > 0)
                path_sec = gameObject.GetComponent<Pathing>().GetPath(new Vector2(x, y), new Vector2(path[0].X, path[0].Y));
            else
                //get path
                if(if_add)
                path_sec = gameObject.GetComponent<Pathing>().GetPath(new Vector2(x, y), transform.Position);
        }
        public bool Hunting()
        {
            return hunting;
        }


        bool encounter = false;
        public override void Update_()
        {
            // check for hunt mode
            Vector3 playerPos = SceneManager.GetGameObjectByName("Player").transform.Position3D;
            if(playerPos.X > transform.Position.X - 4 &&
                playerPos.Y > transform.Position.Y - 4 &&
                playerPos.X < transform.Position.X + 4 &&
                playerPos.Y < transform.Position.Y + 4 &&
                !CollisionManager.RayCast(transform.Position3D + new Vector3(0, 0, 1.6f), playerPos))
            {
                if(!scream.IsPlaying() && !encounter)
                {
                    encounter = true;
                    scream.Play();
                }
                hunting = true;
            }
            else if(hunting)
            {
                hunting = false;
            }
            else if(encounter == true)
                if(Vector3.Distance(transform.Position3D, player.transform.Position3D) > scream.maxDistance || !scream.IsPlaying())
                {
                    scream.Stop();
                    encounter = false;
                }
            if(path.Count > 0)
            {
                // checks if at next position in path, if so remove it from list.
                if(transform.Position.X - 0.5 >= path[path.Count - 1].X - 0.05
                 && transform.Position.X - 0.5 <= path[path.Count - 1].X + 0.05)
                    if(transform.Position.Y - 0.5 >= path[path.Count - 1].Y - 0.05
                     && transform.Position.Y - 0.5 <= path[path.Count - 1].Y + 0.05)
                    {
                        path.RemoveAt(path.Count - 1);
                        // if at the end of path
                        if(path.Count == 0)
                        {
                            path = path_sec;
                            GetPath();
                            return;
                        }
                    }
                //move
                Vector2 dir = getDirection(transform.Position);
                transform.Position += dir;
                transform.Rotation = new Vector3(0, 0, MathHelper.ToDegrees(MathF.Atan2(-dir.X, dir.Y)));
            }
            else
            {
                path = path_sec;
                GetPath();
            }
        }
        /// <summary>
        /// gets a vector to add to monster vector, based on the direction to the point in path. The vectors lenght is based on speed.
        /// </summary>
        /// <param name="monster"> Monster position </param>
        /// <returns></returns>
        Vector2 getDirection(Vector2 monster)
        {
            Vector2 direction = new Vector2(0, 0);
            switch(path[path.Count - 1])
            {
                case Point n when n.X < monster.X - 0.5:
                    if((monster.X - 0.5) - n.X < speed * Globals.DeltaTime)
                        direction.X -= (float)((monster.X - 0.5) - n.X);
                    else
                        direction.X -= speed * Globals.DeltaTime;
                    break;
                case Point n when n.X > monster.X - 0.5:
                    if(n.X - (monster.X - 0.5) < speed * Globals.DeltaTime)
                        direction.X += (float)(n.X - (monster.X - 0.5));
                    else
                        direction.X += speed * Globals.DeltaTime;
                    break;
            }
            switch(path[path.Count - 1])
            {
                case Point n when n.Y < monster.Y - 0.5:
                    if((monster.Y - 0.5) - n.Y < speed * Globals.DeltaTime)
                        direction.Y -= (float)((monster.Y - 0.5) - n.Y);
                    else

                        direction.Y -= speed * Globals.DeltaTime;
                    break;
                case Point n when n.Y > monster.Y - 0.5:
                    if(n.Y - (monster.Y - 0.5) < speed * Globals.DeltaTime)
                        direction.Y += (float)(n.Y - (monster.Y - 0.5));
                    else
                        direction.Y += speed * Globals.DeltaTime;
                    break;
            }
            return direction;
        }
        Vector2 getDirectionEnd(Vector2 monster)
        {
            Vector3 playerPos = SceneManager.GetGameObjectByName("Player").transform.Position3D;
            Vector2 direction = new Vector2(0, 0);
            switch(playerPos.X)
            {
                case float n when n < monster.X:
                    if(monster.X - n < speed * Globals.DeltaTime)
                        direction.X -= (float)(monster.X - n);
                    else
                        direction.X -= speed * Globals.DeltaTime;
                    break;
                case float n when n > monster.X:
                    if(n - monster.X < speed * Globals.DeltaTime)
                        direction.X += (float)(n - monster.X);
                    else
                        direction.X += speed * Globals.DeltaTime;
                    break;
            }
            switch(playerPos.Y)
            {
                case float n when n < monster.Y:
                    if(monster.Y - n < speed * Globals.DeltaTime)
                        direction.Y -= (float)(monster.Y - n);
                    else

                        direction.Y -= speed * Globals.DeltaTime;
                    break;
                case float n when n > monster.Y:
                    if(n - monster.Y < speed * Globals.DeltaTime)
                        direction.Y += (float)(n - monster.Y);
                    else
                        direction.Y += speed * Globals.DeltaTime;
                    break;
            }

            return direction;
        }
        public void OnCollision(GameObject go)
        {
            if(go.name == "Player")
            {
                SceneManager.LoadScene(6);
            }
        }
    }
}
