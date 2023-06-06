

namespace HorrorMaze
{
    public class HighscoreScene : Scene
    {

        GameObject HighscoreText = new GameObject(), highscoreAdd, nameInputField;

        public override void SetupScene()
        {
            //throw new NotImplementedException();

            HighscoreText = new GameObject();
            TextRenderer text = HighscoreText.AddComponent<TextRenderer>();
            text.scale = 4;
            //List<string[]> scores = HighscoreManager.CommandRead();
            //string add = "";
            //for (int i = 0; i < scores.Count; i++)
            //{
            //    add += "nr. " + scores[i][0] + ": " + scores[i][1] + " time: " + (int)(int.Parse(scores[i][2]) / 60) + "minutes, " + (int.Parse(scores[i][2]) % 60) + " seconds\n";
            //}
            //text.SetText(add);
            NetworkManager.GetScores(text);
            text.color = Color.Red;
            text.transform.Position = new Vector2(GameWorld.Instance.GraphicsDevice.Viewport.Width / 2, 400);

            GameObject tryAgain = new GameObject();
            tryAgain.transform.Position = new Vector2(GameWorld.Instance.GraphicsDevice.Viewport.Width / 2, GameWorld.Instance.GraphicsDevice.Viewport.Height / 2);
            UIButton btn = tryAgain.AddComponent<UIButton>();
            btn.OnClick += TryAgain;
            TextRenderer btnText = tryAgain.AddComponent<TextRenderer>();
            btnText.scale = 4;
            btnText.SetText("Retry");

            GameObject mainMenu = new GameObject();
            mainMenu.transform.Position = new Vector2(GameWorld.Instance.GraphicsDevice.Viewport.Width / 2, GameWorld.Instance.GraphicsDevice.Viewport.Height / 2 + 200);
            UIButton btn1 = mainMenu.AddComponent<UIButton>();
            btn1.OnClick += MainMenu;
            TextRenderer btnText1 = mainMenu.AddComponent<TextRenderer>();
            btnText1.scale = 3.5f;
            btnText1.SetText("Main Menu");

            highscoreAdd = new GameObject();
            highscoreAdd.transform.Position = new Vector2(GameWorld.Instance.GraphicsDevice.Viewport.Width / 2 - 300, GameWorld.Instance.GraphicsDevice.Viewport.Height / 2);
            UIButton btn2 = highscoreAdd.AddComponent<UIButton>();
            btn2.OnClick += HighscoreAdd;
            TextRenderer btnText2 = highscoreAdd.AddComponent<TextRenderer>();
            btnText2.scale = 4;
            btnText2.SetText("Add Score");

            nameInputField = new GameObject();
            nameInputField.transform.Position = new Vector2(GameWorld.Instance.GraphicsDevice.Viewport.Width / 2 - 600, GameWorld.Instance.GraphicsDevice.Viewport.Height / 2);
            nameInputField.AddComponent<TextRenderer>();
            nameInputField.GetComponent<TextRenderer>().scale = 5;
            nameInputField.GetComponent<TextRenderer>().SetText("Enter name...");
            nameInputField.GetComponent<TextRenderer>().color = Color.White;
            nameInputField.AddComponent<InputField>();
        }
        public void TryAgain()
        {
            SceneManager.LoadScene(2);
        }

        public void MainMenu()
        {
            SceneManager.LoadScene(1);
        }
        public void HighscoreAdd()
        {
            if (nameInputField.GetComponent<InputField>().input != "") 
            {
                //TimeSpan endTime = SceneManager._gameTimer.GetElapsedTime();
                //int x = endTime.Seconds + endTime.Minutes * 60 + endTime.Hours * 360;
                //HighscoreManager.New_Score(nameInputField.GetComponent<InputField>().input, x);
                //ReloadHigh();
                TimeSpan endTime = SceneManager._gameTimer.GetElapsedTime();
                int finalScore = endTime.Seconds + endTime.Minutes * 60 + endTime.Hours * 360;
                NetworkManager.AddScore(nameInputField.GetComponent<InputField>().input, finalScore.ToString(), HighscoreText.GetComponent<TextRenderer>());
                highscoreAdd.GetComponent<UIButton>().enabled = false;
            }
        }
        //void ReloadHigh()
        //{
        //    List<string[]> scores = HighscoreManager.CommandRead();
        //    string add = "";
        //    for (int i = 0; i < scores.Count; i++)
        //    {
        //        add += "NR. " + scores[i][0] + ": " + scores[i][1] + " Time: " + (int)(int.Parse(scores[i][2])/60) + "Minutes, " + (int.Parse(scores[i][2]) % 60) + " Seconds\n";
        //    }
        //    HighscoreText.GetComponent<TextRenderer>().SetText(add);
        //}
    }
}
