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

            go = new GameObject();
            go.AddComponent<SplashScreenAnimation>();
        }
    }
}
