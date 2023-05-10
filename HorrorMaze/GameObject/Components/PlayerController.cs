using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
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
        Vector2 oldMousePos;

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
            //rotates player based on keboard inputs
            if (keyState.IsKeyDown(Keys.E))
                transform.Rotation += new Vector3(0, 0, rotateScale * elapsed);
            if (keyState.IsKeyDown(Keys.Q))
                transform.Rotation -= new Vector3(0, 0, rotateScale * elapsed);
            //moves player based on keyboard inputs
            Vector3 movement = transform.Position3D;
            if (keyState.IsKeyDown(Keys.W))
                movement += facing * moveScale * elapsed;
            if (keyState.IsKeyDown(Keys.S))
                movement -= facing * moveScale * elapsed;
            if (keyState.IsKeyDown(Keys.D))
                movement += sideVector * moveScale * elapsed;
            if (keyState.IsKeyDown(Keys.A))
                movement -= sideVector * moveScale * elapsed;
            if (keyState.IsKeyDown(Keys.LeftShift))
            {
                movement += (movement - transform.Position3D) * _sprintMultiplier;
            }
            CollisionInfo colInfor = CollisionManager.CheckCircleCollision(transform.Position3D, movement, _playerRadius);
            transform.Position3D = colInfor.collisionPoint;
            //checks if we colide with a object
            if (colInfor.collider != null)
            {
                //goal collision behaviour
                if (colInfor.collider.gameObject.name == "Goal")
                    SceneManager.LoadScene(0);
                //enemy collision behaviour
                if (colInfor.collider.gameObject.name == "Enemy")
                    transform.Position = new Vector2(0.5f,0.5f);
            }
            CameraManager.lightDirection = facing;
            //rotates player based on mouse movement and resets mouse position to center of screen
            Vector2 currentMouse = Mouse.GetState().Position.ToVector2();
            Vector2 centerOfScreen = new Vector2(GameWorld.Instance.GraphicsDevice.Viewport.Width / 2, GameWorld.Instance.GraphicsDevice.Viewport.Height / 2);
            Vector2 changeThisFrame = new Vector2(currentMouse.X, currentMouse.Y) - oldMousePos;
            transform.Rotation -= new Vector3(0,changeThisFrame.Y,changeThisFrame.X) * mouseSensetivity;
            Mouse.SetPosition((int)centerOfScreen.X, (int)centerOfScreen.Y);
            oldMousePos = Mouse.GetState().Position.ToVector2();
        }
    }
}
