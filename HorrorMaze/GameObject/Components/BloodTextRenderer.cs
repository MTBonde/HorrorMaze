using HorrorMaze;

public class BloodTextRenderer : Component
{
    public List<SpriteFont> bloodyFonts = new List<SpriteFont>();
    public SpriteFont currentFont;
    public string text = "Text";
    public Color color = Color.Red;
    public float scale = 1;
    private Vector2 _origin;


    public BloodTextRenderer()  // Constructor made public
    {
        //LoadBloodyFonts();
        currentFont = GameWorld.Instance.Content.Load<SpriteFont>("//BloodyFonts//BloodFont1");
    }

    /// <summary>
    /// Set the text to be printed, find the center
    /// </summary>
    /// <param name="text"></param>
    public void SetText(string text)
    {
        // Pick a random font index
        int fontIndex = Globals.Rnd.Next(bloodyFonts.Count);

        // Set text
        this.text = text;
        // Update the current font
        currentFont = bloodyFonts[fontIndex];
        // Measure string with the current font
        _origin = currentFont.MeasureString(text) / 2;  
    }


    public void LoadBloodyFonts()
    {
        string path = "//BloodyFonts";

        // Load the bloody fonts
        for(int i = 1; i <= 7; i++)
        {
            var fontName = $"{path}//BloodFont{i}";
            var loadedFont = GameWorld.Instance.Content.Load<SpriteFont>(fontName);
            bloodyFonts.Add(loadedFont);
        }
    }

    public void DrawBloodText(SpriteBatch spriteBatch)
    {
        spriteBatch.DrawString(currentFont, text, transform.Position, color, 0, _origin, scale / 10, SpriteEffects.None, 1);
    }
}
