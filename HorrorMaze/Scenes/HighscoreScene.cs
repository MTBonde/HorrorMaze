namespace HorrorMaze
{
    public class HighscoreScene : Scene
    {



        public override void SetupScene()
        {

            GameObject highscores = new GameObject();
            highscores.transform.Position = new Vector2(GameWorld.Instance.GraphicsDevice.Viewport.Width / 2, GameWorld.Instance.GraphicsDevice.Viewport.Height / 2);
            TextRenderer scoreboardText = highscores.AddComponent<TextRenderer>();
            scoreboardText.scale = 5;
            scoreboardText.SetText(HighscoreManager.GetScoreboard());
            scoreboardText.color = Color.Red;

        }
    }
}
