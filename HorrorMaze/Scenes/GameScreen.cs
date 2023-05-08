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
            camTarget = new Vector3(0f, 0f, 0f);//look target will be replaced by PlayerController and Camera component
            camPosition = new Vector3(0f, -5f, 5f);//the players start location will be replaced by the Camera component later
            projectionMatrix = Matrix.CreatePerspectiveFieldOfView(MathHelper.ToRadians(45f), GameWorld.Instance.Graphics.GraphicsDevice.Viewport.AspectRatio, 1f, 1000f);//sets up the projection matrix to a field of view on 45 degrees
            viewMatrix = Matrix.CreateLookAt(camPosition, camTarget, new Vector3(0f, 0f, 1f));//Sets Z as the upwards axis for the view
            worldMatrix = Matrix.CreateWorld(Vector3.Zero, Vector3.Forward, Vector3.Up);

            //Test maze
            Maze maze = new Maze();
            GameObject go = new GameObject();
            go.AddComponent<MazeRenderer>().SetMaze(maze.GenerateMaze(5,5));

            //test enemy
            go = new GameObject();
            go.AddComponent<MeshRenderer>().SetModel("ghost_rig");
            go.transform.Position3D = new Vector3(0.5f, 0.5f, 0);

            //test cam
            go = new GameObject();
            go.AddComponent<Camera>();
        }
        #endregion
    }
}
