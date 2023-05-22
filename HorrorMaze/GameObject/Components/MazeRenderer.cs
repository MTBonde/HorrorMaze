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
    /// Niels
    /// </summary>
    public class MazeRenderer : Component
    {

        MazeCell[,] _mazeCells = new MazeCell[5,5];
        Model[] _wallModels;
        Model _wall, _floor, _celing;
        int _renderDist = 5;
        Transform _playerTransform;

        /// <summary>
        /// sets up the MazeRenderer to draw a given maze
        /// </summary>
        /// <param name="mazeCells">the maze that needs to be drawn</param>
        public MazeRenderer()
        {
            _wall = GameWorld.Instance.Content.Load<Model>("3DModels\\wall");
            _floor = GameWorld.Instance.Content.Load<Model>("3DModels\\floor");
            _celing = GameWorld.Instance.Content.Load<Model>("3DModels\\celling");
            _wallModels = new Model[5];
            _wallModels[0] = GameWorld.Instance.Content.Load<Model>("3DModels\\wall");
            _wallModels[1] = GameWorld.Instance.Content.Load<Model>("3DModels\\wall_1");
            _wallModels[2] = GameWorld.Instance.Content.Load<Model>("3DModels\\wall_2");
            _wallModels[3] = GameWorld.Instance.Content.Load<Model>("3DModels\\wall_3");
            _wallModels[4] = GameWorld.Instance.Content.Load<Model>("3DModels\\wall_4");
        }

        public void Start()
        {
            _playerTransform = SceneManager.GetGameObjectByName("Player").transform;
        }

        public void SetMaze(MazeCell[,] maze)
        {
            _mazeCells = maze;
            WallModelRandomizer(10);
        }

        /// <summary>
        /// gives the walls some random textures to have them be different from each other
        /// </summary>
        /// <param name="specialWallChance">change in percent for a wall to have a special texture</param>
        public void WallModelRandomizer(int specialWallChance)
        {
            Random random = new Random();
            for (int x = 0; x < _mazeCells.GetLength(0); x++)
            {
                for (int y = 0; y < _mazeCells.GetLength(1); y++)
                {
                    if (random.Next(0,100) < specialWallChance)
                    {
                        _mazeCells[x, y].wallmodel[0] = random.Next(1, _wallModels.Length);
                    }
                    if (random.Next(0, 100) < specialWallChance)
                    {
                        _mazeCells[x, y].wallmodel[1] = random.Next(1, _wallModels.Length);
                    }
                }
            }
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
            //defines the range of tiles to render
            Point min = new Point(), max = new Point();
            if (_playerTransform.Position3D.X - _renderDist - transform.Position3D.X < 0)
                min.X = 0;
            else
                min.X = ((int)_playerTransform.Position3D.X) - _renderDist - (int)transform.Position3D.X;
            if (_playerTransform.Position3D.X + _renderDist - transform.Position3D.X > _mazeCells.GetLength(0))
                max.X = _mazeCells.GetLength(0);
            else
                max.X = ((int)_playerTransform.Position3D.X) + _renderDist - (int)transform.Position3D.X;
            if (_playerTransform.Position3D.Y - _renderDist - transform.Position3D.Y < 0)
                min.Y = 0;
            else
                min.Y = ((int)_playerTransform.Position3D.Y) - _renderDist - (int)transform.Position3D.Y;
            if (_playerTransform.Position3D.Y + _renderDist - transform.Position3D.Y > _mazeCells.GetLength(0))
                max.Y = _mazeCells.GetLength(0);
            else
                max.Y = ((int)_playerTransform.Position3D.Y) + _renderDist - (int)transform.Position3D.Y;
            //draws floor and celling
            for (int x = min.X; x < max.X; x++)
            {
                for (int y = min.Y; y < max.Y; y++)
                {
                    DrawFloor(new Vector3(x + 0.5f,y + 0.5f,0) + transform.Position3D, new Vector3(0,0,0));
                    DrawCelling(new Vector3(x + 0.5f,y + 0.5f,2) + transform.Position3D, new Vector3(0,0,0));
                }
            }
            //outer walls spawning
            for (int x = min.X; x < max.X; x++)
            {
                if(x > 0)
                    DrawWall(transform.Position3D + new Vector3(x, 0, 0), transform.Rotation + new Vector3(0,0,270),0);
                //DrawWall(transform.Position3D + new Vector3(x, _mazeCells.GetLength(1), 0), transform.Rotation + new Vector3(0, 0, 270));
            }
            for (int y = min.Y; y < max.Y; y++)
            {
                DrawWall(transform.Position3D + new Vector3(0, y, 0), transform.Rotation + new Vector3(0, 0, 0), 0);
                //DrawWall(transform.Position3D + new Vector3(_mazeCells.GetLength(0), y, 0), transform.Rotation + new Vector3(0, 0, 0));
            }
            //inner walls spawning
            for (int x = min.X; x < max.X; x++)
            {
                for (int y = min.Y; y < max.Y; y++)
                {
                    if (_mazeCells[x, y].Walls[1])
                    {
                        DrawWall(transform.Position3D + new Vector3(x + 1, y + 1, 0), transform.Rotation + new Vector3(0, 0, 180), _mazeCells[x, y].wallmodel[1]);
                    }
                    if (_mazeCells[x, y].Walls[0])
                    {
                        DrawWall(transform.Position3D + new Vector3(x + 1, y + 1, 0), transform.Rotation + new Vector3(0, 0, 90), _mazeCells[x, y].wallmodel[0]);
                    }
                }
            }
        }

        /// <summary>
        /// draws floor at selected position at given rotation
        /// </summary>
        /// <param name="centerPosition">position og the floor</param>
        /// <param name="rotation">rotation of the floor</param>
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

        /// <summary>
        /// draws celling at selected position at given rotation
        /// </summary>
        /// <param name="centerPosition">position og the celling</param>
        /// <param name="rotation">rotation of the celling</param>
        private void DrawCelling(Vector3 centerPosition, Vector3 rotation)
        {
            foreach (ModelMesh mesh in _celing.Meshes)
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

        /// <summary>
        /// draws Wall at selected position at given rotation
        /// </summary>
        /// <param name="centerPosition">position og the Wall</param>
        /// <param name="rotation">rotation of the Wall</param>
        private void DrawWall(Vector3 wallCorner, Vector3 wallRotation, int modelNumber)
        {
            foreach (ModelMesh mesh in _wallModels[modelNumber].Meshes)
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
