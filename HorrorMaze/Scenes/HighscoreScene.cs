namespace HorrorMaze
{
    public class HighscoreScene : Scene
    {

        GameObject HighscoreText = new GameObject(), highscoreAdd, nameInputField;
        bool _TimeTrial = false;

        public override void SetupScene()
        {
            HighscoreText = new GameObject();
            TextRenderer highscoreText = HighscoreText.AddComponent<TextRenderer>();
            highscoreText.scale = 2;
            highscoreText.TextPivot = TextRenderer.TextPivots.TopCenter;
            highscoreText.color = Color.Red;
            highscoreText.transform.Position = new Vector2(GameWorld.Instance.GraphicsDevice.Viewport.Width / 2, 0);
            if (SceneManager.floorClearCount == -1)
                NetworkManager.GetTimeScores(highscoreText);
            else
                NetworkManager.GetFloorScores(highscoreText);

            highscoreAdd = new GameObject();
            highscoreAdd.transform.Position = new Vector2(GameWorld.Instance.GraphicsDevice.Viewport.Width / 2, GameWorld.Instance.GraphicsDevice.Viewport.Height / 2 + 200);
            UIButton addHighscoreButton = highscoreAdd.AddComponent<UIButton>();
            addHighscoreButton.OnClick += HighscoreAdd;
            TextRenderer addHighscoreButtonText = highscoreAdd.AddComponent<TextRenderer>();
            addHighscoreButtonText.scale = 4;
            addHighscoreButtonText.SetText("Add Score");

            nameInputField = new GameObject();
            nameInputField.transform.Position = new Vector2(GameWorld.Instance.GraphicsDevice.Viewport.Width / 2 , GameWorld.Instance.GraphicsDevice.Viewport.Height / 2);
            nameInputField.AddComponent<TextRenderer>();
            nameInputField.GetComponent<TextRenderer>().scale = 5;
            nameInputField.GetComponent<TextRenderer>().SetText("Enter name...");
            nameInputField.GetComponent<TextRenderer>().color = Color.White;
            nameInputField.AddComponent<InputField>();
        }
        public void TryAgain()
        {
            if (SceneManager.floorClearCount == -1)
            {
                SceneManager.LoadScene(2);
            } else
            {
                SceneManager.LoadScene(7);
            }
        }

        public void MainMenu()
        {
            SceneManager.LoadScene(1);
        }
        public void HighscoreAdd()
        {
            if (nameInputField.GetComponent<InputField>().input != "") 
            {

                if (SceneManager.floorClearCount == -1)
                {
                    TimeSpan endTime = SceneManager._gameTimer.GetElapsedTime();
                    int finalScore = endTime.Seconds + endTime.Minutes * 60 + endTime.Hours * 360;
                    NetworkManager.AddTimeScore(nameInputField.GetComponent<InputField>().input, finalScore.ToString(), HighscoreText.GetComponent<TextRenderer>());
                }
                else
                {
                    NetworkManager.AddFloorScore(nameInputField.GetComponent<InputField>().input, SceneManager.floorClearCount.ToString(), SceneManager.floorClearTime.ToString(), HighscoreText.GetComponent<TextRenderer>());
                }
                highscoreAdd.GetComponent<UIButton>().enabled = false;
                highscoreAdd.GetComponent<TextRenderer>().enabled = false;
                highscoreAdd.enabled = false;
                nameInputField.enabled = false;
                nameInputField.GetComponent<TextRenderer>().enabled = false;

                GameObject tryAgain = new GameObject();
                tryAgain.transform.Position = new Vector2(GameWorld.Instance.GraphicsDevice.Viewport.Width / 2, GameWorld.Instance.GraphicsDevice.Viewport.Height / 2 + 200);
                UIButton btn = tryAgain.AddComponent<UIButton>();
                btn.OnClick += TryAgain;
                TextRenderer btnText = tryAgain.AddComponent<TextRenderer>();
                btnText.scale = 4;
                btnText.SetText("Retry");

                GameObject mainMenu = new GameObject();
                mainMenu.transform.Position = new Vector2(GameWorld.Instance.GraphicsDevice.Viewport.Width / 2, GameWorld.Instance.GraphicsDevice.Viewport.Height / 2 + 400);
                UIButton btn1 = mainMenu.AddComponent<UIButton>();
                btn1.OnClick += MainMenu;
                TextRenderer btnText1 = mainMenu.AddComponent<TextRenderer>();
                btnText1.scale = 3.5f;
                btnText1.SetText("Main Menu");
            }
        }
    }
}
