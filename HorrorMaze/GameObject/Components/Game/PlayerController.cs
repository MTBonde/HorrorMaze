
namespace HorrorMaze
{
    /// <summary>
    /// used by the user to control the player
    /// Niels
    /// </summary>
    public class PlayerController : Component
    {

        float moveScale = 1.0f;
        float mouseSensetivity = 0.1f;
        float rotateScale = 50;
        float _playerRadius = 0.15f;
        float _sprintMultiplier = 2.25f;
        public float energy = 3f; // Energy for sprint, in seconds
        public float maxEnergy = 3f;
        float energyRechargeTime = 5f; // Time to fully recharge energy, in seconds

        Vector2 oldMousePos;
        bool oldSchool = false;
        bool canSprint = true;
        bool isSprinting;
        AudioSouce walking, running, lowStamina;

        public void RefillSprint()
        {
            energy = maxEnergy;
        }

        /// <summary>
        /// sets up needed components
        /// </summary>
        public void Awake()
        {
            gameObject.name = "Player";
            gameObject.transform.Position3D = new Vector3(-6.5f, -9.5f, 1.6f);
            gameObject.transform.Rotation = new Vector3(0, 120, 90);
            gameObject.AddComponent<Camera>();
            gameObject.AddComponent<AudioListner>();
            walking = gameObject.AddComponent<AudioSouce>();
            walking.SetSoundEffect("SoundFX\\walking");
            walking.loop = true;
            running = gameObject.AddComponent<AudioSouce>();
            running.SetSoundEffect("SoundFX\\running");
            running.loop = true;
            lowStamina = gameObject.AddComponent<AudioSouce>();
            lowStamina.SetSoundEffect("SoundFX\\breathing");
            lowStamina.loop = true;
        }

        /// <summary>
        /// chesks player inputs every frame and moves the player based on the input
        /// </summary>
        public void Update()
        {
            //elapsed time of previous frame
            float elapsed = Globals.DeltaTime;
            //keyboard ref needs to be replaced with input manager
            KeyboardState keyState = Keyboard.GetState();
            //the forward vector for the object
            Vector3 facing = Vector3.Transform(Vector3.Up, Matrix.CreateRotationZ(MathHelper.ToRadians(transform.Rotation.Z + 90)));
            Vector3 sideVector = Vector3.Transform(Vector3.Up, Matrix.CreateRotationZ(MathHelper.ToRadians(transform.Rotation.Z)));
            Vector3 movement = transform.Position3D;
            if (oldSchool)
            {
                //rotates player based on keboard inputs
                if (keyState.IsKeyDown(Keys.D))
                    transform.Rotation += new Vector3(0, 0, rotateScale * elapsed/3);
                if (keyState.IsKeyDown(Keys.A))
                    transform.Rotation -= new Vector3(0, 0, (rotateScale * elapsed)/3);
                //moves player based on keyboard input
                if (keyState.IsKeyDown(Keys.W))
                    movement += facing * moveScale * elapsed;
                if (keyState.IsKeyDown(Keys.S))
                    movement -= facing * moveScale * elapsed;
            }
            else
            {
                //rotates player based on mouse movement and resets mouse position to center of screen
                Vector2 currentMouse = Mouse.GetState().Position.ToVector2();
                Vector2 centerOfScreen = new Vector2(GameWorld.Instance.GraphicsDevice.Viewport.Width / 2, GameWorld.Instance.GraphicsDevice.Viewport.Height / 2);
                Vector2 changeThisFrame = new Vector2(currentMouse.X, currentMouse.Y) - oldMousePos;
                transform.Rotation -= new Vector3(0,changeThisFrame.Y,changeThisFrame.X) * mouseSensetivity;
                transform.Rotation = new Vector3(transform.Rotation.X, Math.Clamp(transform.Rotation.Y, 10, 170), transform.Rotation.Z);
                Mouse.SetPosition((int)centerOfScreen.X, (int)centerOfScreen.Y);
                oldMousePos = Mouse.GetState().Position.ToVector2();

                //moves player based on keyboard inputs
                if (keyState.IsKeyDown(Keys.W))
                    movement += facing * moveScale * elapsed;
                if (keyState.IsKeyDown(Keys.S))
                    movement -= facing * moveScale * elapsed;
                if (keyState.IsKeyDown(Keys.D))
                    movement += sideVector * moveScale * elapsed;
                if (keyState.IsKeyDown(Keys.A))
                    movement -= sideVector * moveScale * elapsed;
                // If LeftShift is pressed and there's enough energy
                if(keyState.IsKeyDown(Keys.LeftShift) && energy > 0 && canSprint)
                {
                    movement += (movement - transform.Position3D) * _sprintMultiplier;
                    energy -= Globals.DeltaTime;
                    if (!isSprinting)
                    {
                        isSprinting = true;
                        walking.Stop();
                        running.Play();
                    }
                }
                else
                {
                    if (isSprinting)
                    {
                        isSprinting = false;
                        walking.Play();
                        running.Stop();
                    }
                    else if (!walking.IsPlaying() && movement - transform.Position3D != Vector3.Zero)
                        walking.Play();
                    else if (walking.IsPlaying() && movement - transform.Position3D == Vector3.Zero)
                        walking.Stop();

                    //rechages enegy
                    if (energy < maxEnergy)
                    {
                       if(canSprint)
                        {
                            canSprint = false;
                            lowStamina.Play();
                        }
                        energy = Math.Clamp(energy + Globals.DeltaTime / energyRechargeTime * maxEnergy,0,maxEnergy);
                        lowStamina.volume = 1 - energy/maxEnergy;
                        if (energy > maxEnergy / 2)
                        {
                            canSprint = true;
                        }
                    }
                    else if (lowStamina.IsPlaying())
                    {
                        lowStamina.Stop();
                    }
                }
            }
            CollisionInfo colInfor = CollisionManager.CheckCircleCollision(transform.Position3D, movement, gameObject, _playerRadius,1.7f);
            transform.Position3D = new Vector3(colInfor.collisionPoint.X, colInfor.collisionPoint.Y, transform.Position3D.Z);
            CameraManager.lightDirection = facing;
        }
    }
}
