using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace HorrorMaze
{
    public class GameScene : Scene
    {



        #region Methods
        public override void SetupScene()
        {
            //GameWorld.Instance.IsMouseVisible = true;
            //camTarget = new Vector3(0f, 0f, 0f);//look target will be replaced by PlayerController and Camera component
            //camPosition = new Vector3(0f, -5f, 0);//the players start location will be replaced by the Camera component later
            //projectionMatrix = Matrix.CreatePerspectiveFieldOfView(MathHelper.ToRadians(45f), GameWorld.Instance.Graphics.GraphicsDevice.Viewport.AspectRatio, 1f, 1000f);//sets up the projection matrix to a field of view on 45 degrees
            //viewMatrix = Matrix.CreateLookAt(camPosition, camTarget, new Vector3(0f, 0f, 1f));//Sets Z as the upwards axis for the view
            worldMatrix = Matrix.CreateWorld(Vector3.Zero, Vector3.Forward, Vector3.Up);

            //Test maze
            Maze maze = new Maze();
            MazeCell[,] cells = maze.GenerateMaze(10, 10);
            GameObject go = new GameObject();
            go.AddComponent<MazeRenderer>().SetMaze(cells);

            //test enemy
            go = new GameObject();
            go.AddComponent<MeshRenderer>().SetModel("ghost_rig");
            go.transform.Position3D = new Vector3(0.5f, 0.5f, 0);
            go.AddComponent<Pathing>().mazeCells = cells;
            go.AddComponent<Enemy>();

            ThreadManager.Startup(go);

            //test cam
            go = new GameObject();
            go.transform.Position3D = new Vector3(0.5f, 1.5f, 1.6f);
            go.transform.Rotation = new Vector3(0, 0, 0);
            go.AddComponent<PlayerController>();
            go.AddComponent<Camera>();
        }
        #endregion
    }
}
