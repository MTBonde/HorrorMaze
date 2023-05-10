using SharpDX.Direct2D1.Effects;
using SharpDX.DirectWrite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;

namespace HorrorMaze
{
    public class TextRenderer : Component
    {

        public SpriteFont font;
        public string text = "Text";
        public Color color = Color.Black;
        public float scale = 1;
        private TextAlignment _textAlignment;

        TextRenderer()
        {
            SetFont("UIFont");
        }

        public void SetFont(string fontName)
        {
            font = GameWorld.Instance.Content.Load<SpriteFont>(fontName);
        }

        public void DrawUI(SpriteBatch spriteBatch)
        {
            spriteBatch.DrawString(font,text,transform.Position,color,0,Vector2.Zero,scale/10, SpriteEffects.None,1);
        }
    }
}
