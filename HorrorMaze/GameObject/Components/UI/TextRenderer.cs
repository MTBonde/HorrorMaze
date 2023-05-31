

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
        
        TextRenderer()
        {
            SetFont("UIFont");
        }

        public void SetText(string text)
        {
            this.text = text;
            origin = font.MeasureString(text) / 2;
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