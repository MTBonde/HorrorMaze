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
    public class SceneManager
    {

        public static Scene active_scene;

        public void Update(GameTime gameTime)
        {
            for (int i = 0; i < SceneManager.active_scene.gameObjects.Count; i++)
            {
                SceneManager.active_scene.gameObjects[i].Update(gameTime);
            }
        }

        public void Draw(Microsoft.Xna.Framework.Graphics.SpriteBatch spriteBatch)
        {
            for (int i = 0; i < SceneManager.active_scene.gameObjects.Count; i++)
            {
                SceneManager.active_scene.gameObjects[i].Draw(spriteBatch);
            }
        }
    }
}
