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

        float moveScale = 1.0f;
        float mouseSensetivity = 0.1f;
        float rotateScale = 50;
        float _playerRadius = 0.15f;
        float _sprintMultiplier = 2.25f;
        public float energy = 3f; // Energy for sprint, in seconds
        public float maxEnergy = 3f;
        float energyRechargeTime = 5f; // Time to fully recharge energy, in seconds
        Stopwatch sprintTimer = new Stopwatch(); // Timer for sprint function

        Vector2 oldMousePos;
        bool oldSchool = false;
        bool canSprint = true;
        public bool isSprinting = false;

        public bool PlayBreathingSound { get; private set; } = false;

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

                // If LeftShift is pressed and there's enough energy
                if(keyState.IsKeyDown(Keys.LeftShift) && energy > 0 && canSprint)
                {
                    movement += (movement - transform.Position3D) * _sprintMultiplier;
                    energy -= Globals.DeltaTime;
                    isSprinting = true;
                }
                else
                {
                     isSprinting = false;
                    
                    //rechages enegy
                    if (energy < maxEnergy)
                    {
                       
                        canSprint = false;
                        energy = Math.Clamp(energy + Globals.DeltaTime / energyRechargeTime * maxEnergy,0,maxEnergy);
                        PlayBreathingSound = true;
                        if (energy > maxEnergy / 2)
                            canSprint = true;
                    }
                    // Deactivate heartbeat once energy is fully recharged
                    else if(PlayBreathingSound)
                        PlayBreathingSound = false;
                }
            }
            CollisionInfo colInfor = CollisionManager.CheckCircleCollision(transform.Position3D, movement, gameObject, _playerRadius,1.7f);
            transform.Position3D = colInfor.collisionPoint;
            CameraManager.lightDirection = facing;
        }
    }
}
