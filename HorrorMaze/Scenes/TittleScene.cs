using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace HorrorMaze
{
    public class TittleScene : Scene
    {



        public override void SetupScene()
        {
            GameObject tittle = new GameObject();
            TextRenderer tittleText = tittle.AddComponent<TextRenderer>();
            //BloodTextRenderer tittleText = tittle.AddComponent<BloodTextRenderer>();
            tittleText.scale = 20;
            tittleText.SetText("Horror Maze");
            tittleText.color = Color.Red;
            tittleText.transform.Position = new Vector2(GameWorld.Instance.GraphicsDevice.Viewport.Width / 2, 200);

            GameObject madeInMonogame = new GameObject();
            TextRenderer madeInMonogameText = madeInMonogame.AddComponent<TextRenderer>();
            madeInMonogameText.scale = 5;
            madeInMonogameText.SetText("Made in Monogame");
            madeInMonogameText.color = Color.Red;
            madeInMonogameText.transform.Position = new Vector2(GameWorld.Instance.GraphicsDevice.Viewport.Width / 2, 350);

            GameObject by = new GameObject();
            TextRenderer byText = by.AddComponent<TextRenderer>();
            byText.scale = 5;
            byText.SetText("By Scare Factory");
            byText.color = Color.Red;
            byText.transform.Position = new Vector2(GameWorld.Instance.GraphicsDevice.Viewport.Width / 2, 410);

            GameObject play = new GameObject();
            play.transform.Position = new Vector2(GameWorld.Instance.GraphicsDevice.Viewport.Width / 2, GameWorld.Instance.GraphicsDevice.Viewport.Height / 2);
            UIButton btn = play.AddComponent<UIButton>();
            btn.OnClick += Play;
            TextRenderer btnText = play.AddComponent<TextRenderer>();
            btnText.scale = 4;
            btnText.SetText("Play");

            GameObject quit = new GameObject();
            quit.transform.Position = new Vector2(GameWorld.Instance.GraphicsDevice.Viewport.Width / 2, GameWorld.Instance.GraphicsDevice.Viewport.Height / 2 + 400);
            UIButton btm1 = quit.AddComponent<UIButton>();
            btm1.OnClick += Quit;
            TextRenderer btnText1 = quit.AddComponent<TextRenderer>();
            btnText1.scale = 4;
            btnText1.SetText("Quit");
        }

        public void Play()
        {
            SceneManager.LoadScene(2);
        }

        public void Quit()
        {
            GameWorld.Instance.Exit();
        }
    }
}
