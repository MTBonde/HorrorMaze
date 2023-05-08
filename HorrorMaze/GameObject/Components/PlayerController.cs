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

        float moveScale = 15f;
        float rotateScale = MathHelper.PiOver2;

        public void Update()
        {
            float elapsed = Globals.DeltaTime;
            KeyboardState keyState = Keyboard.GetState();
            float moveAmount = 0f;
            if (keyState.IsKeyDown(Keys.D))
            {
                gameObject.GetComponent<Camera>().Rotation =
                   MathHelper.WrapAngle(transform.Rotation.Z - (rotateScale * elapsed));
            }
            if (keyState.IsKeyDown(Keys.A))
            {
                gameObject.GetComponent<Camera>().Rotation =
                    MathHelper.WrapAngle(transform.Rotation.Z + (rotateScale * elapsed));
            }
            if (keyState.IsKeyDown(Keys.W))
            {
                //camera.MoveForward(moveScale * elapsed);
                moveAmount = moveScale * elapsed;
            }
            if (keyState.IsKeyDown(Keys.S))
            {
                //camera.MoveForward(-moveScale * elapsed);
                moveAmount = -moveScale * elapsed;
            }
            if (moveAmount != 0)
            {
                Vector3 newLocation = gameObject.GetComponent<Camera>().PreviewMove(moveAmount);
                bool moveOk = true;
                //if (newLocation.X < 0  newLocation.X > Maze.mazeWidth)
                //    moveOk = false;
                //if (newLocation.Z < 0  newLocation.Z > Maze.mazeHeight)
                //    moveOk = false;
                if (moveOk)
                    gameObject.GetComponent<Camera>().MoveForward(moveAmount);
            }
        }
    }
}
