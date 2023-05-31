namespace HorrorMaze
{
    /// <summary>
    /// Setup the Titlescreen
    /// Niels/Thor
    /// </summary>
    public class TittleScene : Scene
    {
        /// <summary>
        /// Initializes the scene by creating and positioning game objects.
        /// Niels
        /// </summary>
        public override void SetupScene()
        {
            // Create a world matrix at the origin, facing forward, with up direction
            worldMatrix = Matrix.CreateWorld(Vector3.Zero, Vector3.Forward, Vector3.Up);

            // Create a game object for the title
            GameObject title = new GameObject();
            // Add a text renderer component to the title game object
            TextRenderer titleText = title.AddComponent<TextRenderer>();
            // Set scale, text, and color properties of the title text renderer
            titleText.scale = 20;
            titleText.SetText("Horror Maze");
            titleText.color = Color.Red;
            // Position the title in the viewport
            titleText.transform.Position = new Vector2(GameWorld.Instance.GraphicsDevice.Viewport.Width / 2, 200);


            // 'Made in Monogame' text, The same procedure as last time, Miss Sophie?.
            GameObject madeInMonogame = new GameObject();
            TextRenderer madeInMonogameText = madeInMonogame.AddComponent<TextRenderer>();
            madeInMonogameText.scale = 5;
            madeInMonogameText.SetText("Made in Monogame");
            madeInMonogameText.color = Color.Red;
            madeInMonogameText.transform.Position = new Vector2(GameWorld.Instance.GraphicsDevice.Viewport.Width / 2, 350);

            // 'By Scare Factory' text, The same procedure as every time, James.
            GameObject by = new GameObject();
            TextRenderer byText = by.AddComponent<TextRenderer>();
            byText.scale = 5;
            byText.SetText("By Scare Factory");
            byText.color = Color.Red;
            byText.transform.Position = new Vector2(GameWorld.Instance.GraphicsDevice.Viewport.Width / 2, 410);

            // 'Play' button
            GameObject play = new GameObject();
            play.transform.Position = new Vector2(GameWorld.Instance.GraphicsDevice.Viewport.Width / 2, GameWorld.Instance.GraphicsDevice.Viewport.Height / 2);
            UIButton btn = play.AddComponent<UIButton>();
            btn.OnClick += Play;
            TextRenderer btnText = play.AddComponent<TextRenderer>();
            btnText.scale = 4;
            btnText.SetText("Play");

            // 'Quit' button
            GameObject quit = new GameObject();
            quit.transform.Position = new Vector2(GameWorld.Instance.GraphicsDevice.Viewport.Width / 2, GameWorld.Instance.GraphicsDevice.Viewport.Height / 2 + 400);
            UIButton btm1 = quit.AddComponent<UIButton>();
            btm1.OnClick += Quit;
            TextRenderer btnText1 = quit.AddComponent<TextRenderer>();
            btnText1.scale = 4;
            btnText1.SetText("Quit");

            // Maze in the background
            SetupMaze();
        }

        /// <summary>
        /// Load the next scene.
        /// </summary>
        public void Play()
        {
            SceneManager.LoadScene(2);
        }

        /// <summary>
        /// Exit the game world.
        /// </summary>
        public void Quit()
        {
            GameWorld.Instance.Exit();
        }

        /// <summary>
        /// Creates a new maze to use as background on the titlescreen.
        /// Thor
        /// </summary>
        public void SetupMaze()
        {
            // Create a new maze to use as background on the titlescreen
            Maze maze = new Maze(3, 3);
            MazeCell[,] mazeCells = new MazeCell[3, 3];
            // Initialize the cells in the maze
            for(int x = 0; x < mazeCells.GetLength(0); x++)
                for(int y = 0; y < mazeCells.GetLength(1); y++)
                    mazeCells[x, y] = new MazeCell();
            // Add a room to the maze
            maze.AddRoomBeforeMaze(new Point(0, 0), 3, 3, 0, mazeCells);

            // Create and configure the camera
            GameObject camera = new GameObject();
            camera.transform.Position3D = new Vector3(1.5f, 1.5f, 1.6f);
            camera.transform.Rotation = new Vector3(0, 90, 0);
            camera.AddComponent<Camera>();
            camera.name = "Player";
            // add title scrren camera component, it has the update so the camera moves random
            camera.AddComponent<TitleScreenCamera>();

            // Create a gate
            GameObject gate = new GameObject();
            gate.AddComponent<MeshRenderer>().SetModel("3DModels\\door");
            gate.transform.Position = new Vector2(0.5f, 0);

            // Position the maze in the game world
            GameObject TitleScreenMaze = new GameObject();
            TitleScreenMaze.AddComponent<MazeRenderer>().SetMaze(mazeCells);
        }
    }
}
