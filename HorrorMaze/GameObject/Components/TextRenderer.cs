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
    public enum TextAlignment
    {
        Left,
        Rigt,
        Center
    }

    /// <summary>
    /// used to render text in ui space
    /// Niels
    /// </summary>
    public class TextRenderer : Component
    {

        public SpriteFont font;
        public string text = "Text";
        public Color color = Color.Black;
        public float scale = 1;
        Vector2 origin;

        public void SetText(string text)
        {
            this.text = text;
            origin = font.MeasureString(text) / 2;
        }

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
            spriteBatch.DrawString(font,text,transform.Position,color,0,origin,scale/10, SpriteEffects.None,1);
        }
    }
}
