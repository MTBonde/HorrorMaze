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
            GameObject winText = new GameObject();
            TextRenderer text = winText.AddComponent<TextRenderer>();
            text.scale = 10;
            text.SetText("Maze Horror");
            text.color = Color.Red;
            text.transform.Position = new Vector2(GameWorld.Instance.GraphicsDevice.Viewport.Width / 2, -200);

            GameObject play = new GameObject();
            play.transform.Position = new Vector2(GameWorld.Instance.GraphicsDevice.Viewport.Width / 2, GameWorld.Instance.GraphicsDevice.Viewport.Height / 2);
            UIButton btn = play.AddComponent<UIButton>();
            btn.OnClick += Play;
            TextRenderer btnText = play.AddComponent<TextRenderer>();
            btnText.scale = 4;
            btnText.SetText("Play");

            GameObject quit = new GameObject();
            quit.transform.Position = new Vector2(GameWorld.Instance.GraphicsDevice.Viewport.Width / 2, GameWorld.Instance.GraphicsDevice.Viewport.Height / 2 + 200);
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
