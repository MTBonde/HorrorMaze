﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HorrorMaze
{
    /// <summary>
    /// CreditScene
    /// Thor
    /// </summary>
    public class CreditsScene : Scene
    {
        public override void SetupScene()
        {
            var screenHeightSpace = GameWorld.Instance.GraphicsDevice.Viewport.Height / 10;
            
            GameObject CreditTextLine1Go = new GameObject();
            TextRenderer CreditTextLine1 = CreditTextLine1Go.AddComponent<TextRenderer>();
            CreditTextLine1.scale = screenHeightSpace / 8;
            CreditTextLine1.color = Color.Red;
            CreditTextLine1.transform.Position = new Vector2(Globals.ScreenCenterWidth.X, screenHeightSpace);
            CreditTextLine1.SetText("Horror Maze");


            GameObject CreditTextLine2Go = new GameObject();
            TextRenderer CreditTextLine2 = CreditTextLine2Go.AddComponent<TextRenderer>();
            CreditTextLine2.scale = screenHeightSpace / 32;
            CreditTextLine2.color = Color.Red;
            CreditTextLine2.transform.Position = new Vector2(Globals.ScreenCenterWidth.X, screenHeightSpace*2);
            CreditTextLine2.SetText("was made by");


            GameObject CreditTextLine3Go = new GameObject();
            TextRenderer CreditTextLine3 = CreditTextLine3Go.AddComponent<TextRenderer>();
            CreditTextLine3.scale = screenHeightSpace / 16;
            CreditTextLine3.color = Color.Red;
            CreditTextLine3.transform.Position = new Vector2(Globals.ScreenCenterWidth.X, screenHeightSpace*2.5f);
            CreditTextLine3.SetText("ScareFactory");


            GameObject CreditTextLine4Go = new GameObject();
            TextRenderer CreditTextLine4 = CreditTextLine4Go.AddComponent<TextRenderer>();
            CreditTextLine4.scale = 5;
            CreditTextLine4.color = Color.Red;
            CreditTextLine4.transform.Position = new Vector2(Globals.ScreenCenterWidth.X, screenHeightSpace*5);
            CreditTextLine4.SetText("M.T.Bonde, N.N.Andersen & T.S.Dietrich");


            GameObject CreditTextLine5Go = new GameObject();
            TextRenderer CreditTextLine5 = CreditTextLine5Go.AddComponent<TextRenderer>();
            CreditTextLine5.scale = 5;
            CreditTextLine5.color = Color.Red;
            CreditTextLine5.transform.Position = new Vector2(Globals.ScreenCenterWidth.X, screenHeightSpace*6);
            CreditTextLine5.SetText("3d-Models by N.N.Andersen");


            GameObject CreditTextLine6Go = new GameObject();
            TextRenderer CreditTextLine6 = CreditTextLine6Go.AddComponent<TextRenderer>();
            CreditTextLine6.scale = 5;
            CreditTextLine6.color = Color.Red;
            CreditTextLine6.transform.Position = new Vector2(Globals.ScreenCenterWidth.X, screenHeightSpace*7);
            CreditTextLine6.SetText("Music by Lofi-Lou. Subscribe on youtube");


            GameObject CreditTextLine7Go = new GameObject();
            TextRenderer CreditTextLine7 = CreditTextLine7Go.AddComponent<TextRenderer>();
            CreditTextLine7.scale = 5;
            CreditTextLine7.color = Color.Red;
            CreditTextLine7.transform.Position = new Vector2(Globals.ScreenCenterWidth.X, screenHeightSpace *9);
            CreditTextLine7.SetText("Horror Maze was made using the Monogame Framework");
            CreditTextLine7Go.AddComponent<MainMenuTimer>();









        }
    }
}
