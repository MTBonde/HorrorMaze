using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HorrorMaze
{
    public class HighscoreScene : Scene
    {

        GameObject HighscoreText = new GameObject();

        public override void SetupScene()
        {
            //throw new NotImplementedException();

            HighscoreText = new GameObject();
            TextRenderer text = HighscoreText.AddComponent<TextRenderer>();
            text.scale = 4;
            List<string[]> scores = HighscoreManager.CommandRead();
            string add = "";
            for (int i = 0; i < scores.Count; i++)
            {
                add += "nr " + scores[i][0] + " called " + scores[i][1] + ", Got a time of: " + scores[i][2] + "\n";
            }
            text.SetText(add);
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

            GameObject highscoreAdd = new GameObject();
            highscoreAdd.transform.Position = new Vector2(GameWorld.Instance.GraphicsDevice.Viewport.Width / 2 - 300, GameWorld.Instance.GraphicsDevice.Viewport.Height / 2);
            UIButton btn2 = highscoreAdd.AddComponent<UIButton>();
            btn2.OnClick += HighscoreAdd;
            TextRenderer btnText2 = highscoreAdd.AddComponent<TextRenderer>();
            btnText2.scale = 4;
            btnText2.SetText("Add Score");
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
            TimeSpan endTime = SceneManager._gameTimer.GetElapsedTime();
            int x = endTime.Seconds;
            ReloadHigh();
            HighscoreManager.New_Score("john", x);
        }
        void ReloadHigh()
        {
            List<string[]> scores = HighscoreManager.CommandRead();
            string add = "";
            for (int i = 0; i < scores.Count; i++)
            {
                add += "nr " + scores[i][0] + " called " + scores[i][1] + ", Got a time of: " + scores[i][2] + "\n";
            }
            HighscoreText.GetComponent<TextRenderer>().SetText(add);
        }
    }
}
