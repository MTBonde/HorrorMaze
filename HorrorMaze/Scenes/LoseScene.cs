using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HorrorMaze
{
    public class LoseScene : Scene
    {



        public override void SetupScene()
        {
            GameObject loseText = new GameObject();
            TextRenderer text = loseText.AddComponent<TextRenderer>();
            text.scale = 10;
            text.SetText("You Lose!");
            text.color = Color.Red;
            text.transform.Position = new Vector2(GameWorld.Instance.GraphicsDevice.Viewport.Width / 2, 200);

            GameObject tryAgain = new GameObject();
            tryAgain.transform.Position = new Vector2(GameWorld.Instance.GraphicsDevice.Viewport.Width / 2, GameWorld.Instance.GraphicsDevice.Viewport.Height / 2);
            UIButton btn = tryAgain.AddComponent<UIButton>();
            btn.OnClick += TryAgain;
            TextRenderer btnText = tryAgain.AddComponent<TextRenderer>();
            btnText.scale = 5;
            btnText.SetText("Retry");
        }

        public void TryAgain()
        {
            SceneManager.LoadScene(2);
        }
    }
}
