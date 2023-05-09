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
                transform.Position3D = CollisionManager.CheckCircleCollision(transform.Position3D,transform.Position3D + facing * moveScale * elapsed,0.1f);
            }
            if (keyState.IsKeyDown(Keys.S))
                transform.Position3D -= facing * moveScale * elapsed;
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
