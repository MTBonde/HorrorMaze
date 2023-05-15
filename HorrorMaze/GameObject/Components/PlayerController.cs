using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Security.Cryptography.Xml;
using System.Text;
using System.Threading.Tasks;

namespace HorrorMaze
{
    public class PlayerController : Component
    {

        float moveScale = 2.5f;
        float mouseSensetivity = 0.25f;
        float rotateScale = 50;
        float _playerRadius = 0.15f;
        float _sprintMultiplier = 0.25f;
        float energy = 10f; // Energy for sprint, in seconds
        float energyRechargeTime = 20f; // Time to fully recharge energy, in seconds
        Stopwatch sprintTimer = new Stopwatch(); // Timer for sprint function

        Vector2 oldMousePos;
        bool oldSchool = false;

        public bool PlayHeartBeatSound { get; private set; } = false;

        //chesks player inputs every frame and moves the player based on the input
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
                //if (keyState.IsKeyDown(Keys.LeftShift))
                //    movement += (movement - transform.Position3D) * _sprintMultiplier;

                // If LeftShift is pressed and there's enough energy
                if(keyState.IsKeyDown(Keys.LeftShift) && energy > 0)
                {
                    // Start sprint timer if not already running
                    if(!sprintTimer.IsRunning)
                        sprintTimer.Start();

                    // Calculate energy consumption
                    float energyConsumed = (float)sprintTimer.Elapsed.TotalSeconds;
                    if(energyConsumed <= energy)
                    {
                        // move at sprint speed
                        movement += (movement - transform.Position3D) * _sprintMultiplier;
                        // Drain energy
                        energy -= (float)energyConsumed; 
                    }
                    else
                    {
                        // Activate heartbeat
                        PlayHeartBeatSound = true;
                        // Reset sprint timer
                        sprintTimer.Reset(); 
                    }
                }
                else
                {
                    // Regenerate energy over time when not sprinting
                    if(sprintTimer.IsRunning)
                        sprintTimer.Reset();

                    if(energy < 10)
                    {
                        // Recharge energy over time
                        energy += Globals.DeltaTime / energyRechargeTime;
                        // cap at maximum energy
                        if(energy > 10) 
                            energy = 10; 
                    }

                    // Deactivate heartbeat once energy is fully recharged
                    if(PlayHeartBeatSound && energy == 10)
                        PlayHeartBeatSound = false;
                }
            }
            CollisionInfo colInfor = CollisionManager.CheckCircleCollision(transform.Position3D, movement, _playerRadius);
            transform.Position3D = colInfor.collisionPoint;
            //checks if we colide with a object
            if (colInfor.collider != null)
            {
                //goal collision behaviour
                if (colInfor.collider.gameObject.name == "Goal")
                {
                    //stop timer here
                    TimeSpan endTime = SceneManager._gameTimer.GetElapsedTime();
                    SceneManager._gameTimer.StopTimer();
                    Debug.WriteLine($"Game ends. The end time is {endTime} milliseconds.");

                    //
                    SceneManager.LoadScene(5);
                }
                //enemy collision behaviour
                if (colInfor.collider.gameObject.name == "Enemy")
                    SceneManager.LoadScene(6);
            }
            CameraManager.lightDirection = facing;
        }
    }
}
