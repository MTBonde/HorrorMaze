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
            //creates worlds center point
            worldMatrix = Matrix.CreateWorld(Vector3.Zero, Vector3.Forward, Vector3.Up);

            //Test maze
            Maze maze = new Maze();
            MazeCell[,] cells = maze.GenerateMaze(10, 10);
            GameObject go = new GameObject();
            go.AddComponent<MazeRenderer>().SetMaze(cells);
            go.AddComponent<MazeCollider>().SetMaze(cells);

            //test enemy
            go = new GameObject();
            go.name = "Enemy";
            go.AddComponent<BoxCollider>().size = new Vector3(1, 1, 1);
            go.AddComponent<MeshRenderer>().SetModel("ghost_rig");
            go.transform.Position3D = new Vector3(0.5f, 0.5f, 0);
            go.AddComponent<Pathing>().mazeCells = cells;
            go.AddComponent<Enemy>();

            ThreadManager.Startup(go);

            //test cam
            go = new GameObject();
            go.transform.Position3D = new Vector3(1.5f, 1.5f, 1.6f);
            go.transform.Rotation = new Vector3(0, 0, 0);
            go.AddComponent<PlayerController>();
            go.AddComponent<Camera>();

            //test Goal
            go = new GameObject();
            go.name = "Goal";
            go.transform.Position3D = new Vector3(4.5f,4.5f,0);
            go.AddComponent<MeshRenderer>().SetModel("win_item");
            go.AddComponent<BoxCollider>().size = Vector3.One / 10;

            //test UI
            go = new GameObject();
            go.AddComponent<SpriteRenderer>().SetSprite("Company Logo");
            go.transform.Position = new Vector2(200,200);
        }
        #endregion
    }
}
