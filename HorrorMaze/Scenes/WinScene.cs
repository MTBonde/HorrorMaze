﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HorrorMaze
{
    public class WinScene : Scene
    {



        public override void SetupScene()
        {
            BackupAudioManager.StartBackgroundMusic();

            GameObject winText = new GameObject();
            TextRenderer text = winText.AddComponent<TextRenderer>();
            text.scale = 10;
            text.SetText("You Win!");
            text.color = Color.Red;
            text.transform.Position = new Vector2 (GameWorld.Instance.GraphicsDevice.Viewport.Width / 2, 200);
            winText.AddComponent<MainMenuTimer>();

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

            GameObject highscoreMenu = new GameObject();
            highscoreMenu.transform.Position = new Vector2(GameWorld.Instance.GraphicsDevice.Viewport.Width / 2 + 300, GameWorld.Instance.GraphicsDevice.Viewport.Height / 2);
            UIButton btn2 = highscoreMenu.AddComponent<UIButton>();
            btn2.OnClick += HighscoreMenu;
            TextRenderer btnText2 = highscoreMenu.AddComponent<TextRenderer>();
            btnText2.scale = 4;
            btnText2.SetText("Highscore");
        }

        public void TryAgain()
        {
            SceneManager.LoadScene(2);
        }

        public void MainMenu()
        {
            SceneManager.LoadScene(4);
        }

        public void HighscoreMenu()
        {
            SceneManager.LoadScene(3);
        }
    }
}
