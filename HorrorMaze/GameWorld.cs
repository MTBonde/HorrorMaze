using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace HorrorMaze
{
    public class GameWorld : Game
    {
        #region Fields and properties
        private static GameWorld _instance;
        /// <summary>
        /// gets the GameWorld intance
        /// </summary>
        public static GameWorld Instance
        {
            get
            {
                if (_instance == null)
                    _instance = new GameWorld();
                return _instance;
            }
        }

        /// <summary>
        /// the gameworlds GraphicsDeviceManager
        /// </summary>
        public GraphicsDeviceManager graphics { get; private set; }
        private SpriteBatch _spriteBatch;
        #endregion

        #region Constructor
        public GameWorld()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
        }
        #endregion


        #region Methods
        protected override void Initialize()
        {
            // TODO: Add your initialization logic here

            base.Initialize();
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);

            // TODO: use this.Content to load your game content here
            SceneManager.SetupScene();
        }

        protected override void Update(GameTime gameTime)
        {
            if(GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            // TODO: Add your update logic here
            SceneManager.Update(gameTime);
            
            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            // TODO: Add your drawing code here
            SceneManager.Draw(_spriteBatch);

            base.Draw(gameTime);
        }
        #endregion
    }
}