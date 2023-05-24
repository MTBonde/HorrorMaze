namespace HorrorMaze
{
    public class TittleScene : Scene
    {
        public override void SetupScene()
        {
            //create world center point
            worldMatrix = Matrix.CreateWorld(Vector3.Zero, Vector3.Forward, Vector3.Up);


            GameObject tittle = new GameObject();
            TextRenderer tittleText = tittle.AddComponent<TextRenderer>();
            //BloodTextRenderer tittleText = tittle.AddComponent<BloodTextRenderer>();
            tittleText.scale = 20;
            tittleText.SetText("Horror Maze");
            tittleText.color = Color.Red;
            tittleText.transform.Position = new Vector2(GameWorld.Instance.GraphicsDevice.Viewport.Width / 2, 200);

            GameObject madeInMonogame = new GameObject();
            TextRenderer madeInMonogameText = madeInMonogame.AddComponent<TextRenderer>();
            madeInMonogameText.scale = 5;
            madeInMonogameText.SetText("Made in Monogame");
            madeInMonogameText.color = Color.Red;
            madeInMonogameText.transform.Position = new Vector2(GameWorld.Instance.GraphicsDevice.Viewport.Width / 2, 350);

            GameObject by = new GameObject();
            TextRenderer byText = by.AddComponent<TextRenderer>();
            byText.scale = 5;
            byText.SetText("By Scare Factory");
            byText.color = Color.Red;
            byText.transform.Position = new Vector2(GameWorld.Instance.GraphicsDevice.Viewport.Width / 2, 410);

            GameObject play = new GameObject();
            play.transform.Position = new Vector2(GameWorld.Instance.GraphicsDevice.Viewport.Width / 2, GameWorld.Instance.GraphicsDevice.Viewport.Height / 2);
            UIButton btn = play.AddComponent<UIButton>();
            btn.OnClick += Play;
            TextRenderer btnText = play.AddComponent<TextRenderer>();
            btnText.scale = 4;
            btnText.SetText("Play");

            GameObject quit = new GameObject();
            quit.transform.Position = new Vector2(GameWorld.Instance.GraphicsDevice.Viewport.Width / 2, GameWorld.Instance.GraphicsDevice.Viewport.Height / 2 + 400);
            UIButton btm1 = quit.AddComponent<UIButton>();
            btm1.OnClick += Quit;
            TextRenderer btnText1 = quit.AddComponent<TextRenderer>();
            btnText1.scale = 4;
            btnText1.SetText("Quit");


            // Background Maze
            SetupMaze();
        }

        public void Play()
        {
            SceneManager.LoadScene(2);
        }

        public void Quit()
        {
            GameWorld.Instance.Exit();
        }

        public void SetupMaze()
        {
           

            // maze start room
            Maze maze = new Maze(3, 3);
            MazeCell[,] mazeCells = new MazeCell[3, 3];
            for(int x = 0; x < mazeCells.GetLength(0); x++)
                for(int y = 0; y < mazeCells.GetLength(1); y++)
                    mazeCells[x, y] = new MazeCell();


            //Camera
            GameObject camera = new GameObject();
            camera.transform.Position3D = new Vector3(1.5f, 1.5f, 1.6f);
            camera.transform.Rotation = new Vector3(0, 90, 0);            
            camera.AddComponent<Camera>();
            camera.name = "Player";

            //places maze in the world
            GameObject TitleScreenMaze = new GameObject();
            TitleScreenMaze.AddComponent<MazeRenderer>().SetMaze(mazeCells);
            maze.AddRoomBeforeMaze(new Point(0, 0), 3, 3, 0);

        
            


        }

        //public void Update()
        //{         
        //    float elapsed = Globals.DeltaTime;
        //    float rotateScale = 50;

        //    int turn = Globals.Rnd.Next(2);
        //    camera.transform.Rotation += turn == 0
        //        ?
        //        new Vector3(0, 0, rotateScale * elapsed / 3)
        //        :
        //        new Vector3(0, 0, -(rotateScale * elapsed) / 3);
        //}
    }
}
