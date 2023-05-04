using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HorrorMaze
{
    public class GameScreen : Scene
    {



        #region Methods
        public override void SetupScene()
        {
            camTarget = new Vector3(5f, 1f, 0.75f);//look target will be replaced by PlayerController and Camera component
            camPosition = new Vector3(5f, -0f, 50f);//the players start location will be replaced by the Camera component later
            projectionMatrix = Matrix.CreatePerspectiveFieldOfView(MathHelper.ToRadians(45f), GameWorld.Instance.graphics.GraphicsDevice.Viewport.AspectRatio, 1f, 1000f);//sets up the projection matrix to a field of view on 45 degrees
            viewMatrix = Matrix.CreateLookAt(camPosition, camTarget, new Vector3(0f, 0f, 1f));//Sets Z as the upwards axis for the view
            worldMatrix = Matrix.CreateWorld(Vector3.Zero, Vector3.Up, Vector3.Backward);//Sets Z as the upwards axis for the World and y as the forward

            //Test maze
            GameObject go = new GameObject();
            go.AddComponent<MazeRenderer>();
        }
        #endregion
    }
}
