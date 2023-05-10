using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HorrorMaze
{
    public class CameraManager
    {

        public static Vector3 lightDirection = new Vector3(0.5f, 0.5f, 0.5f);
        public static Vector3 lightColor = new Color(0.5f,0.45f,0.35f).ToVector3();

        public static void ApplyWorldShading(BasicEffect effect)
        {
            effect.LightingEnabled = true;
            effect.AmbientLightColor = lightColor;
            //effect.DirectionalLight0.Direction = lightDirection;
            //effect.DirectionalLight0.DiffuseColor = Vector3.One;
            //effect.DirectionalLight0.Enabled = true;

            effect.FogEnabled = true;
            effect.FogColor = Color.Black.ToVector3();
            effect.FogStart = 0;
            effect.FogEnd = 3f;
        }
    }
}
