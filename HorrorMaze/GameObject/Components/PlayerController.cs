using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HorrorMaze
{
    public class PlayerController : Component
    {

        float moveScale = 2.5f;
        float mouseSensetivity = 0.5f;
        float rotateScale = 50;
        float _playerRadius = 0.15f;
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
            //rotates player based on keboard inputs
            if (keyState.IsKeyDown(Keys.D))
                transform.Rotation -= new Vector3(0, 0, rotateScale * elapsed);
            if (keyState.IsKeyDown(Keys.A))
                transform.Rotation += new Vector3(0, 0, rotateScale * elapsed);
            if (keyState.IsKeyDown(Keys.E))
                transform.Rotation += new Vector3(0, rotateScale * elapsed, 0);
            if (keyState.IsKeyDown(Keys.Q))
                transform.Rotation -= new Vector3(0, rotateScale * elapsed, 0);
            //moves player based on keyboard inputs
            if (keyState.IsKeyDown(Keys.W))
            {
                CollisionInfo colInfor = CollisionManager.CheckCircleCollision(transform.Position3D,transform.Position3D + facing * moveScale * elapsed,_playerRadius);
                transform.Position3D = colInfor.collisionPoint;
                //checks if we colide with the win game object needs to use a object reference or name istead of coordinate
                if (colInfor.collider != null)
                    if (colInfor.collider.transform.Position == new Vector2(4.5f, 4.5f))
                        SceneManager.LoadScene(0);
            }
            if (keyState.IsKeyDown(Keys.S))
            {
                CollisionInfo colInfor = CollisionManager.CheckCircleCollision(transform.Position3D,transform.Position3D - facing * moveScale * elapsed,_playerRadius);
                transform.Position3D = colInfor.collisionPoint;
                //checks if we colide with the win game object needs to use a object reference or name istead of coordinate
                if (colInfor.collider != null)
                    if (colInfor.collider.transform.Position == new Vector2(4.5f, 4.5f))
                        SceneManager.LoadScene(0);
            }
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
