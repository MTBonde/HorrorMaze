

namespace HorrorMaze
{
    /// <summary>
    /// used to keep track of scenes and to update active scene
    /// Niels
    /// </summary>
    public static class SceneManager
    {
        #region Fields & Variables

        public static TimeManager _gameTimer = new();

        /// <summary>
        /// the cunrently active scene
        /// </summary>
        public static Scene active_scene = new SplashScene();
        /// <summary>
        /// list of all scenes
        /// </summary>
        public static Scene[] scenes = new Scene[7] 
        { 
            new SplashScene(),
            new TittleScene(),
            new GameScene(),
            new HighscoreScene(),
            new CreditsScene(),
            new WinScene(),
            new LoseScene()
        };
        public static bool inGame = false;
        #endregion

        #region Methods
        /// <summary>
        /// loads and setups a scene and then replaces the current scene with it
        /// </summary>
        /// <param name="scene_number">the number of the scene to load</param>
        public static void LoadScene(int scene_number)//maybe make one that uses a string instead and looks it up in a dictionary
        {
            HighscoreManager.Starter();
            if(scene_number == 2)
                inGame = true;
            else
                inGame = false;
            for (int i = 0; i < active_scene.gameObjects.Count; i++)
            {
                active_scene.gameObjects[i].StopSound();
            }
            active_scene.gameObjects.Clear();
            CollisionManager.colliders.Clear();
            active_scene = scenes[scene_number];
            GameWorld.Instance.IsMouseVisible = true;
            ThreadManager.StopThreads();
            SetupScene();
        }

        /// <summary>
        /// run presetup and instatiate your gameobjects in the active scene
        /// </summary>
        public static void SetupScene()
        {
            active_scene.SetupScene();
            for (int i = 0; i < active_scene.gameObjects.Count; i++)
            {
                active_scene.gameObjects[i].Awake();
                active_scene.gameObjects[i].Start();
            }
        }

        /// <summary>
        /// Gets called every frame and tells all gameobjects to do their update
        /// </summary>
        /// <param name="gameTime"></param>
        public static void Update(GameTime gameTime)
        {
            //Global Update
            Globals.Update(gameTime);

            //Manager Update
            //audioManager.Update();
            //if(_gameTimer != null)
            //    _gameTimer.UpdateTimer();



            //Scene Update
            if (inGame)
                ((GameScene)active_scene).debugManager.Update(gameTime);
            for (int i = 0; i < active_scene.gameObjects.Count; i++)
            {
                active_scene.gameObjects[i].Update(gameTime);
            }
        }

        /// <summary>
        /// gets called at the end of each frame and tells the gameobjects to draw
        /// </summary>
        /// <param name="spriteBatch">the games spritebatch</param>
        public static void Draw(Microsoft.Xna.Framework.Graphics.SpriteBatch spriteBatch)
        {
            GameWorld.Instance.Graphics.GraphicsDevice.DepthStencilState = DepthStencilState.Default;
            GameWorld.Instance.Graphics.GraphicsDevice.RasterizerState = RasterizerState.CullNone;
            for (int i = 0; i < active_scene.gameObjects.Count; i++)
            {
                active_scene.gameObjects[i].Draw3D();
            }
            spriteBatch.Begin(SpriteSortMode.FrontToBack, null);
            for (int i = 0; i < active_scene.gameObjects.Count; i++)
            {
                active_scene.gameObjects[i].Draw2D(spriteBatch);
            }
            CameraManager.ApplyCameraEffects(spriteBatch);
            spriteBatch.End();
            spriteBatch.Begin(SpriteSortMode.FrontToBack, null);
            for (int i = 0; i < active_scene.gameObjects.Count; i++)
            {
                active_scene.gameObjects[i].DrawUI(spriteBatch);
            }
            spriteBatch.End();
        }

        /// <summary>
        /// get a gameobject that has a specific name
        /// </summary>
        /// <param name="name">the gameobjects name that u are looking for</param>
        /// <returns>the gameobject if it exists else returns null</returns>
        public static GameObject GetGameObjectByName(string name)
        {
            for (int i = 0; i < active_scene.gameObjects.Count; i++)
            {
                if (active_scene.gameObjects[i].name == name)
                    return active_scene.gameObjects[i];
            }
            return null;
        }
        #endregion
    }
}
