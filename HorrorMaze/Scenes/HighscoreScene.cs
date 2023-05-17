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

            GameObject mainMenu = new GameObject();
            mainMenu.transform.Position = new Vector2(GameWorld.Instance.GraphicsDevice.Viewport.Width / 2, GameWorld.Instance.GraphicsDevice.Viewport.Height / 2 + 200);
            UIButton btn1 = mainMenu.AddComponent<UIButton>();
            btn1.OnClick += NextScene;
            TextRenderer btnText1 = mainMenu.AddComponent<TextRenderer>();
            btnText1.scale = 3.5f;
            btnText1.SetText("Continue");
        }

        public void NextScene()
        {
            SceneManager.LoadScene(6);
        }
    }
}
