namespace HorrorMaze
{
    public abstract class Enemy : Component
    {
        // Enemy fields
        public bool isHunting = false;
        public bool hasEncounter = false;

        // Unique enemy behavior
        public bool canGoTroughWalls = false;
        public bool canTransform = false;
        public bool canMoveWhenNotSeen = false;

        // Pathing
        private List<Point> _currentPath = new List<Point>();
        private List<Point> _nextPath = new List<Point>();

        AudioSouce _scream;
        public GameObject Player;

        public float Speed { get; protected set; }

        public virtual void Awake()
        {
            // Set Collider and player referance
            gameObject.AddComponent<BoxCollider>().size = new Vector3(0.35f, 0.35f, 1);
            Player = SceneManager.GetGameObjectByName("Player");
        }

        public virtual void Start()
        {
            // get path for current path
            GetPath();
            _currentPath = _nextPath;

            // get path for next path
            GetPath();
        }
        
        private void GetPath()
        {
            bool chosen = false;
            bool if_add = true;
            float x = 0;
            float y = 0;
            if(isHunting)
            {
                Vector3 playerPos = Player.transform.Position3D;
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
                    x = Globals.Rnd.Next(gameObject.GetComponent<Pathing>().mazeCells.GetLength(0));
                    y = Globals.Rnd.Next(gameObject.GetComponent<Pathing>().mazeCells.GetLength(1));
                    if(x != (int)(transform.Position.X) && y != (int)(transform.Position.Y))
                        chosen = true;
                }
            }
            if(_currentPath.Count > 0)
                _nextPath = gameObject.GetComponent<Pathing>().GetPath(new Vector2(x, y), new Vector2(_currentPath[0].X, _currentPath[0].Y));
            else
                //get path
                if(if_add)
                _nextPath = gameObject.GetComponent<Pathing>().GetPath(new Vector2(x, y), transform.Position);
        }
        private bool Hunting()
        {
            return isHunting;
        }



        public virtual void Update_()
        {
            Vector3 playerPos = SceneManager.GetGameObjectByName("Player").transform.Position3D;
            if(playerPos.X > transform.Position.X - 4 &&
                playerPos.Y > transform.Position.Y - 4 &&
                playerPos.X < transform.Position.X + 4 &&
                playerPos.Y < transform.Position.Y + 4 &&
                !CollisionManager.RayCast(transform.Position3D + new Vector3(0, 0, 1.6f), playerPos))
            {
                if(!_scream.IsPlaying() && !hasEncounter)
                {
                    hasEncounter = true;
                    _scream.Play();
                }
                isHunting = true;
            }
            else if(isHunting)
            {
                isHunting = false;
            }
            else if(hasEncounter == true)
                if(Vector3.Distance(transform.Position3D, Player.transform.Position3D) > _scream.maxDistance || !_scream.IsPlaying())
                {
                    _scream.Stop();
                    hasEncounter = false;
                }
            if(_currentPath.Count > 0)
            {
                //checks if at next position in path, if so remove it from list.
                    if(transform.Position.X - 0.5 >= _currentPath[_currentPath.Count - 1].X - 0.05
                     && transform.Position.X - 0.5 <= _currentPath[_currentPath.Count - 1].X + 0.05)
                    if(transform.Position.Y - 0.5 >= _currentPath[_currentPath.Count - 1].Y - 0.05
                     && transform.Position.Y - 0.5 <= _currentPath[_currentPath.Count - 1].Y + 0.05)
                    {
                        _currentPath.RemoveAt(_currentPath.Count - 1);
                        //if at the end of path
                            if(_currentPath.Count == 0)
                        {
                            _currentPath = _nextPath;
                            GetPath();
                            return;
                        }
                    }

                //move
                Vector2 dir = GetDirection(transform.Position);
                transform.Position += dir;
                transform.Rotation = new Vector3(0, 0, MathHelper.ToDegrees(MathF.Atan2(-dir.X, dir.Y)));
            }
            else
            {
                _currentPath = _nextPath;
                GetPath();
            }
        }
        /// <summary>
        /// gets a vector to add to monster vector, based on the direction to the point in path. The vectors lenght is based on speed.
        /// </summary>
        /// <param name="monster"> Monster position </param>
        /// <returns></returns>        
        protected Vector2 GetDirection(Vector2 monster)
        {
            Vector2 direction = new Vector2(0, 0);
            switch(_currentPath[_currentPath.Count - 1])
            { //Globals.DeltaTime
                case Point n when n.X < monster.X - 0.5:
                    if((monster.X - 0.5) - n.X < Speed * Globals.DeltaTime)
                        direction.X -= (float)((monster.X - 0.5) - n.X);
                    else
                        direction.X -= Speed * Globals.DeltaTime;
                    break;
                case Point n when n.X > monster.X - 0.5:
                    if(n.X - (monster.X - 0.5) < Speed * Globals.DeltaTime)
                        direction.X += (float)(n.X - (monster.X - 0.5));
                    else
                        direction.X += Speed * Globals.DeltaTime;
                    break;
            }
            switch(_currentPath[_currentPath.Count - 1])
            {
                case Point n when n.Y < monster.Y - 0.5:
                    if((monster.Y - 0.5) - n.Y < Speed * Globals.DeltaTime)
                        direction.Y -= (float)((monster.Y - 0.5) - n.Y);
                    else
                        direction.Y -= Speed * Globals.DeltaTime;
                    break;
                case Point n when n.Y > monster.Y - 0.5:
                    if(n.Y - (monster.Y - 0.5) < Speed * Globals.DeltaTime)
                        direction.Y += (float)(n.Y - (monster.Y - 0.5));
                    else
                        direction.Y += Speed * Globals.DeltaTime;
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
