using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HorrorMaze
{
    /// <summary>
    /// used to render imiges in 2D space
    /// Niels
    /// </summary>
    public class SpriteRenderer : Component
    {

        private Texture2D sprite;
        public Vector2 Origin { get; set; }
        public float layer;
        public float scale = 0.25f;

        public void SetSprite(string spriteName)
        {
            sprite = GameWorld.Instance.Content.Load<Texture2D>(spriteName);
            Origin = new Vector2(sprite.Width / 2, sprite.Height / 2);
        }

        public void Draw2D(SpriteBatch spriteBatch)
        {
            if (sprite == null)
            {
                Console.WriteLine("no Image on sprite renderer");
                return;
            }
            spriteBatch.Draw(sprite, gameObject.transform.Position, null, Color.White, 0, Origin, scale, SpriteEffects.None, layer);
        }
    }
}
