using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using SharpDX.Direct2D1;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace HorrorMaze
{
    public static class SceneManager
    {

        #region Fields & Variables
        /// <summary>
        /// the cunrently active scene
        /// </summary>
        public static Scene active_scene = new GameScreen();
        /// <summary>
        /// list of all scenes
        /// </summary>
        private static Scene[] scenes = new Scene[7] 
        { 
            new SplashScreen(),
            new TittleScreen(),
            new GameScreen(),
            new HighscoreScreen(),
            new CreditsScreen(),
            new WinScreen(),
            new LoseScreen()
        };
        #endregion

        #region Methods
        /// <summary>
        /// loads and setups a scene and then replaces the current scene with it
        /// </summary>
        /// <param name="scene_number">the number of the scene to load</param>
        public static void LoadScene(int scene_number)//maybe make one that uses a string instead and looks it up in a dictionary
        {
            active_scene.gameObjects.Clear();
            active_scene = scenes[scene_number];
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
            for (int i = 0; i < active_scene.gameObjects.Count; i++)
            {
                active_scene.gameObjects[i].Draw3D();
            }
            spriteBatch.Begin(SpriteSortMode.FrontToBack, null);
            for (int i = 0; i < active_scene.gameObjects.Count; i++)
            {
                active_scene.gameObjects[i].Draw2D(spriteBatch);
            }
            spriteBatch.End();
            spriteBatch.Begin(SpriteSortMode.FrontToBack, null);
            for (int i = 0; i < active_scene.gameObjects.Count; i++)
            {
                active_scene.gameObjects[i].DrawUI(spriteBatch);
            }
            spriteBatch.End();
        }
        #endregion
    }
}
