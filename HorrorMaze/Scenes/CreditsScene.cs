namespace HorrorMaze
{
    /// <summary>
    /// This class creates the credits 
    /// Thor
    /// </summary>
    public class CreditsScene : Scene
    {
        /// <summary>
        /// The SetupScene method is responsible for setting up Credit text.
        /// </summary>
        public override void SetupScene()
        {
            // Calculate the space each line of text should take on screen based on screen height and a divider factor (10)
            float screenHeightSpace = GameWorld.Instance.GraphicsDevice.Viewport.Height / 10f;
            // Calculate a screen resolution factor based on current screen height compared to a base resolution (1080p)
            float ScreenResolutionFactor = GameWorld.Instance.GraphicsDevice.Viewport.Height / 1080f;

            // Call Createtext for each line of text to display, set "text", textsize and position as argument
            CreateText("Horror Maze", 20 * ScreenResolutionFactor, new Vector2(Globals.ScreenCenterWidth.X, 100 * ScreenResolutionFactor));
            CreateText("was made by", 5 * ScreenResolutionFactor, new Vector2(Globals.ScreenCenterWidth.X, 100 * ScreenResolutionFactor * 2.25f));
            CreateText("ScareFactory", 10 * ScreenResolutionFactor, new Vector2(Globals.ScreenCenterWidth.X, 100 * ScreenResolutionFactor * 3));
            CreateText("M.T.Bonde, N.N.Andersen & T.S.Dietrich", 5 * ScreenResolutionFactor, new Vector2(Globals.ScreenCenterWidth.X, screenHeightSpace * 5));
            CreateText("3d-Models by N.N.Andersen", 5 * ScreenResolutionFactor, new Vector2(Globals.ScreenCenterWidth.X, screenHeightSpace * 5 * 1.1f));
            CreateText("Music by Lofi-Lou. Subscribe on youtube", 5 * ScreenResolutionFactor, new Vector2(Globals.ScreenCenterWidth.X, screenHeightSpace * 5 * 1.2f));
            CreateText("Horror Maze was made using the Monogame Framework", 5 * ScreenResolutionFactor, new Vector2(Globals.ScreenCenterWidth.X, screenHeightSpace * 9));
        }

        /// <summary>
        /// The CreateText method is used to create a new text object in the scene.
        /// It sets the text string, size, color, and position.
        /// If the text is about the Monogame Framework, it also adds a MainMenuTimer component.
        /// </summary>
        /// <param name="text">The text string to be displayed.</param>
        /// <param name="textsize">The size of the text to be displayed.</param>
        /// <param name="position">The position of the text in the scene.</param>
        private void CreateText(string text, float textsize, Vector2 position)
        {
            // Create a new game object for the text
            GameObject creditTextGO = new GameObject();
            // Add a TextRenderer component to the game object
            TextRenderer creditText = creditTextGO.AddComponent<TextRenderer>();
            // Set the scale of the text using the text size parametre
            creditText.scale = textsize;
            // Set the color of the text to red
            creditText.color = Color.Red;
            // Position the text in the scene
            creditText.transform.Position = position;
            // Set the text content
            creditText.SetText(text);

            // Add a timer to the last text, so that player is sent back to title screen after a time
            if(text == "Horror Maze was made using the Monogame Framework")
            {
                creditTextGO.AddComponent<MainMenuTimer>();
            }
        }
    }
}