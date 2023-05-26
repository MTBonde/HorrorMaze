using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HorrorMaze
{
    public class SplashScene : Scene
    {



        public override void SetupScene()
        {
            GameObject go = new GameObject();
            go.AddComponent<SpriteRenderer>().SetSprite("Company Logo");
            go.transform.Position = new Vector2(GameWorld.Instance.GraphicsDevice.Viewport.Width / 2, GameWorld.Instance.GraphicsDevice.Viewport.Height / 2);

            go = new GameObject();
            go.AddComponent<SplashScreenController>();

            GameObject bestWithHeadset = new GameObject();
            TextRenderer bestWithHeadsetText = bestWithHeadset.AddComponent<TextRenderer>();
            bestWithHeadset.transform.Position = new Vector2(GameWorld.Instance.GraphicsDevice.Viewport.Width / 2, GameWorld.Instance.GraphicsDevice.Viewport.Height / 2 + 300);
            bestWithHeadsetText.scale = 5f;
            bestWithHeadsetText.SetText("Use headset or surround system for best experience");
            bestWithHeadsetText.color = Color.White;
        }
    }
}
