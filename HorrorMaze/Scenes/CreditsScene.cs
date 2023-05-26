using System;
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
            GameObject CreditTextLine1Go = new GameObject();
            TextRenderer CreditTextLine1 = CreditTextLine1Go.AddComponent<TextRenderer>();
            CreditTextLine1.scale = 20;
            CreditTextLine1.color = Color.Red;
            CreditTextLine1.transform.Position = new Vector2(Globals.ScreenCenterWidth.X, 100);
            CreditTextLine1.SetText("Horror Maze");


            GameObject CreditTextLine2Go = new GameObject();
            TextRenderer CreditTextLine2 = CreditTextLine2Go.AddComponent<TextRenderer>();
            CreditTextLine2.scale = 5;
            CreditTextLine2.color = Color.Red;
            CreditTextLine2.transform.Position = new Vector2(Globals.ScreenCenterWidth.X, 300);
            CreditTextLine2.SetText("was made by");


            GameObject CreditTextLine3Go = new GameObject();
            TextRenderer CreditTextLine3 = CreditTextLine3Go.AddComponent<TextRenderer>();
            CreditTextLine3.scale = 10;
            CreditTextLine3.color = Color.Red;
            CreditTextLine3.transform.Position = new Vector2(Globals.ScreenCenterWidth.X, 400);
            CreditTextLine3.SetText("ScareFactory");


            GameObject CreditTextLine4Go = new GameObject();
            TextRenderer CreditTextLine4 = CreditTextLine4Go.AddComponent<TextRenderer>();
            CreditTextLine4.scale = 5;
            CreditTextLine4.color = Color.Red;
            CreditTextLine4.transform.Position = new Vector2(Globals.ScreenCenterWidth.X, 700);
            CreditTextLine4.SetText("M.T.Bonde, N.N.Andersen & T.S.Dietrich");


            GameObject CreditTextLine5Go = new GameObject();
            TextRenderer CreditTextLine5 = CreditTextLine5Go.AddComponent<TextRenderer>();
            CreditTextLine5.scale = 5;
            CreditTextLine5.color = Color.Red;
            CreditTextLine5.transform.Position = new Vector2(Globals.ScreenCenterWidth.X, 800);
            CreditTextLine5.SetText("3d-Models by N.N.Andersen");


            GameObject CreditTextLine6Go = new GameObject();
            TextRenderer CreditTextLine6 = CreditTextLine6Go.AddComponent<TextRenderer>();
            CreditTextLine6.scale = 5;
            CreditTextLine6.color = Color.Red;
            CreditTextLine6.transform.Position = new Vector2(Globals.ScreenCenterWidth.X, 900);
            CreditTextLine6.SetText("Music by Lofi-Lou. Subscribe on youtube");


            GameObject CreditTextLine7Go = new GameObject();
            TextRenderer CreditTextLine7 = CreditTextLine7Go.AddComponent<TextRenderer>();
            CreditTextLine7.scale = 5;
            CreditTextLine7.color = Color.Red;
            CreditTextLine7.transform.Position = new Vector2(Globals.ScreenCenterWidth.X, Globals.ScreenSize.Y -100);
            CreditTextLine7.SetText("Horror Maze was made using the Monogame Framework");
            CreditTextLine7Go.AddComponent<MainMenuTimer>();









        }
    }
}
