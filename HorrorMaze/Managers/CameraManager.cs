﻿namespace HorrorMaze
{
    public class CameraManager
    {

        public static Vector3 lightDirection = new Vector3(0.5f, 0.5f, 0.5f);
        public static Vector3 lightColor = new Color(0.5f,0.45f,0.35f).ToVector3();
        public static Texture2D _vignette;
        static float vignetteScale = 1.0f;

        public static void Setup()
        {
            _vignette = GameWorld.Instance.Content.Load<Texture2D>("Vignette");
            vignetteScale = (float)GameWorld.Instance.GraphicsDevice.Viewport.Width / (float)_vignette.Width + 0.25f;
        }

        public static void ApplyWorldShading(BasicEffect effect)
        {
            effect.LightingEnabled = false;
            effect.AmbientLightColor = lightColor;
            //effect.DirectionalLight0.Direction = lightDirection;
            //effect.DirectionalLight0.DiffuseColor = Vector3.One;
            //effect.DirectionalLight0.Enabled = true;

            effect.FogEnabled = true;
            effect.FogColor = Color.Black.ToVector3();
            effect.FogStart = 0;
            effect.FogEnd = 3f;
        }

        public static void ApplyCameraEffects(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(_vignette, new Vector2(GameWorld.Instance.GraphicsDevice.Viewport.Width / 2, GameWorld.Instance.GraphicsDevice.Viewport.Height / 2), null, Color.White, 0, new Vector2(_vignette.Width / 2, _vignette.Height / 2), vignetteScale, SpriteEffects.None, 1);
        }
    }
}
