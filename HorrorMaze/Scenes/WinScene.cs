using System;
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
            GameObject winText = new GameObject();
            TextRenderer text = winText.AddComponent<TextRenderer>();
            text.text = "You Win";
            text.scale = 10;
            text.color = Color.Red;

            GameObject tryAgain = new GameObject(); 
            tryAgain.transform.Position = new Vector2(GameWorld.Instance.GraphicsDevice.Viewport.Width / 2, GameWorld.Instance.GraphicsDevice.Viewport.Height / 2);
            UIButton btn = tryAgain.AddComponent<UIButton>();
            btn.size = new Vector2(200, 200);
            btn.OnClick += TryAgain;
        }

        public void TryAgain()
        {
            SceneManager.LoadScene(2);
        }
    }
}
