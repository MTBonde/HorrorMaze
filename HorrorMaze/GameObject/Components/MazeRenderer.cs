using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HorrorMaze
{
    /// <summary>
    /// a renderer that renders a maze
    /// by Niels
    /// </summary>
    public class MazeRenderer : Component
    {

        MazeCell[,] _mazeCells = new MazeCell[5,5];
        Model _wall, _floor, _celing;

        /// <summary>
        /// sets up the MazeRenderer to draw a given maze
        /// </summary>
        /// <param name="mazeCells">the maze that needs to be drawn</param>
        public MazeRenderer()
        {
            SetMaze(new MazeCell[10, 10]);
            _wall = GameWorld.Instance.Content.Load<Model>("wall");
            _floor = GameWorld.Instance.Content.Load<Model>("floor");
            _celing = GameWorld.Instance.Content.Load<Model>("celling");
        }

        public void SetMaze(MazeCell[,] maze)
        {
            _mazeCells = maze;
        }

        /// <summary>
        /// draws the maze with the gameobjects position being the buttom left corner
        /// </summary>
        public void Draw3D()
        {
            if (_mazeCells == null)
            {
                Debug.WriteLine("Warning: the maze that is given is null");
                return;
            }
            //draws floor and celling
            for (int x = 0; x < _mazeCells.GetLength(0); x++)
            {
                for (int y = 0; y < _mazeCells.GetLength(1); y++)
                {
                    DrawFloor(new Vector3(x + 0.5f,y + 0.5f,0), new Vector3(0,0,0));
                    DrawFloor(new Vector3(x + 0.5f,y + 0.5f,2), new Vector3(0,180,0));
                }
            }
            //outer walls spawning
            for (int x = 0; x < _mazeCells.GetLength(0); x++)
            {
                DrawWall(transform.Position3D + new Vector3(x, 0, 0), transform.Rotation + new Vector3(0,0,270));
                //DrawWall(transform.Position3D + new Vector3(x, _mazeCells.GetLength(1), 0), transform.Rotation + new Vector3(0, 0, 270));
            }
            for (int y = 0; y < _mazeCells.GetLength(1); y++)
            {
                DrawWall(transform.Position3D + new Vector3(0, y, 0), transform.Rotation + new Vector3(0, 0, 0));
                //DrawWall(transform.Position3D + new Vector3(_mazeCells.GetLength(0), y, 0), transform.Rotation + new Vector3(0, 0, 0));
            }
            //inner walls spawning
            for (int x = 0; x < _mazeCells.GetLength(0); x++)
            {
                for (int y = 0; y < _mazeCells.GetLength(1); y++)
                {
                    if (_mazeCells[x, y].Walls[1])
                    {
                        DrawWall(transform.Position3D + new Vector3(x + 1, y + 1, 0), transform.Rotation + new Vector3(0, 0, 180));
                    }
                    if (_mazeCells[x, y].Walls[0])
                    {
                        DrawWall(transform.Position3D + new Vector3(x + 1, y + 1, 0), transform.Rotation + new Vector3(0, 0, 90));
                    }
                }
            }
        }

        private void DrawFloor(Vector3 centerPosition, Vector3 rotation)
        {
            foreach (ModelMesh mesh in _floor.Meshes)
            {
                foreach (BasicEffect effect in mesh.Effects)
                {

                    CameraManager.ApplyWorldShading(effect);

                    effect.View = SceneManager.active_scene.viewMatrix;
                    effect.World = SceneManager.active_scene.worldMatrix * Matrix.CreateRotationX(MathHelper.ToRadians(rotation.X)) * Matrix.CreateRotationY(MathHelper.ToRadians(rotation.Y)) * Matrix.CreateRotationZ(MathHelper.ToRadians(rotation.Z)) * Matrix.CreateTranslation(centerPosition);
                    effect.Projection = SceneManager.active_scene.projectionMatrix;
                    mesh.Draw();
                }
            }
        }

        private void DrawCelling()
        {

        }

        private void DrawWall(Vector3 wallCorner, Vector3 wallRotation)
        {
            foreach (ModelMesh mesh in _wall.Meshes)
            {
                foreach (BasicEffect effect in mesh.Effects)
                {

                    CameraManager.ApplyWorldShading(effect);

                    effect.View = SceneManager.active_scene.viewMatrix;
                    effect.World = SceneManager.active_scene.worldMatrix * Matrix.CreateRotationX(MathHelper.ToRadians(wallRotation.X)) * Matrix.CreateRotationY(MathHelper.ToRadians(wallRotation.Y)) * Matrix.CreateRotationZ(MathHelper.ToRadians(wallRotation.Z)) * Matrix.CreateTranslation(wallCorner);
                    effect.Projection = SceneManager.active_scene.projectionMatrix;
                    mesh.Draw();
                }
            }
        }
    }
}
