﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace HorrorMaze
{
    public class GameWorld : Game
    {
        private SpriteBatch _spriteBatch;

        // PROPERTIES
        public GraphicsDeviceManager Graphics { get; private set; }

        // GameWorld Singleton
        private static GameWorld _instance;
        /* 
        ??= (null coalescing assignment)  spørger om værdien af var. "instance" er null. 
        Hvis instance er null, instantieres en ny "GameWorld" og tildeles til "instance"
        Hvis "instance IKKE er null returneres den existerende værdi(gameworld) af instancen
        Den nyeste c# har ikke brug for at få at vide hvilken "ny" vi snakker om derfor New()
        */
        public static GameWorld Instance => _instance ??= new();

        private GameWorld()
        {
            Graphics = new GraphicsDeviceManager(this);
            Graphics.PreferredBackBufferWidth = GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Width;
            Graphics.PreferredBackBufferHeight = GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Height;      
            Graphics.ApplyChanges();
            this.IsFixedTimeStep = false;//false;
            this.TargetElapsedTime = TimeSpan.FromSeconds(1d / 120d); //60);

            Content.RootDirectory = "Content";
        }
        


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
            CameraManager.Setup();
            AudioManager.LoadMusic();
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
            GraphicsDevice.Clear(Color.Black);

            // TODO: Add your drawing code here
            SceneManager.Draw(_spriteBatch);

            base.Draw(gameTime);
        }
        #endregion
    }
}