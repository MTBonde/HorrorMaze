using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace HorrorMaze
{
    public class MeshRenderer : Component
    {
        #region Fields & Properties
        /// <summary>
        /// the model that this meshrenderer is displaying
        /// </summary>
        private Model _model;
        #endregion

        #region Methods
        /// <summary>
        /// sets the model of the MeshRenderer to a model from the content with the given model_name
        /// </summary>
        /// <param name="model_name">name of the model</param>
        public void SetModel(string model_name)
        {
            _model = GameWorld.Instance.Content.Load<Model>(model_name);
        }

        /// <summary>
        /// draws the model at the Gameobjects current location from the pivot of the mesh/model
        /// </summary>
        public void Draw3D()
        {
            //checks if it has a model attach and returns if it dosent
            if (_model == null)
            {
                return;
            }
            //renders the model
            foreach (ModelMesh mesh in _model.Meshes)
            {
                foreach (BasicEffect effect in mesh.Effects)
                {
                    effect.LightingEnabled = false;

                    CameraManager.ApplyWorldShading(effect);
                    //effect.
                    //effect.EnableDefaultLighting();
                    //effect.AmbientLightColor = new Vector3(1f, 0, 0);
                    effect.View = SceneManager.active_scene.viewMatrix;
                    effect.World = SceneManager.active_scene.worldMatrix * Matrix.CreateRotationX(MathHelper.ToRadians(gameObject.transform.Rotation.X)) * Matrix.CreateRotationY(MathHelper.ToRadians(gameObject.transform.Rotation.Y)) * Matrix.CreateRotationZ(MathHelper.ToRadians(gameObject.transform.Rotation.Z)) * Matrix.CreateTranslation(gameObject.transform.Position3D);
                    effect.Projection = SceneManager.active_scene.projectionMatrix;
                    mesh.Draw();
                }
            }
        }
        #endregion
    }
}
