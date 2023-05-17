using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HorrorMaze
{
    public class BackupEnemy : Component
    {

        List<Vector2> path = new List<Vector2>();
        float _speed = 1f;

        public void Start()
        {
            Random rnd = new Random();
            path = gameObject.GetComponent<BackupPathing>().GetPath(
                new Point((int)transform.Position.X, (int)transform.Position.Y), 
                new Point(
                    rnd.Next(0, gameObject.GetComponent<BackupPathing>()._mazeCells.GetLength(0)), 
                    rnd.Next(0, gameObject.GetComponent<BackupPathing>()._mazeCells.GetLength(1))));
        }

        public void Update()
        {
            if (path != null && path.Count > 0)
            {
                Vector2 dir = path[0] - transform.Position;
                dir.Normalize();
                Vector2 minLocation = transform.Position;
                Vector2 maxLocation = path[0];
                if (transform.Position.X > path[0].X)
                {
                    minLocation.X = path[0].X;
                    maxLocation.X = transform.Position.X;
                }
                if (transform.Position.Y > path[0].Y)
                {
                    minLocation.Y = path[0].Y;
                    maxLocation.Y = transform.Position.Y;
                }
                transform.Position = Vector2.Clamp(transform.Position + (dir * _speed * Globals.DeltaTime), minLocation, maxLocation);
                Debug.WriteLine(MathF.Atan2(-dir.X, dir.Y));
                transform.Rotation = new Vector3(0, 0, MathHelper.ToDegrees(MathF.Atan2(-dir.X, dir.Y)));
                if (transform.Position == path[0])
                {
                    path.Remove(path[0]);
                }
            }
            else
            {
                Random rnd = new Random();
                path = gameObject.GetComponent<BackupPathing>().GetPath(
                    new Point((int)(transform.Position.X - 0.5f), (int)(transform.Position.Y - 0.5f)), 
                    new Point(
                        rnd.Next(0, gameObject.GetComponent<BackupPathing>()._mazeCells.GetLength(0)), 
                        rnd.Next(0, gameObject.GetComponent<BackupPathing>()._mazeCells.GetLength(1))));
            }
        }

        public void OnCollision(GameObject go)
        {
            if (go.name == "Player")
                //SceneManager.LoadScene(3);
                SceneManager.LoadScene(6);
        }
    }
}
