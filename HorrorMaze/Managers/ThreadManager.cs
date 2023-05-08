using Microsoft.VisualBasic.ApplicationServices;
using SharpDX.Direct2D1.Effects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace HorrorMaze
{
    public class ThreadManager
    {
        static bool kill_thread = false;
        static List<GameObject> enemies = new List<GameObject>();
        public static void Startup(GameObject enemy)
        {
            enemies.Add(enemy);
            Thread pathing = new Thread(UpdatePathing);
            pathing.IsBackground = true;
            pathing.Start();
        }
        static void UpdatePathing()
        {
            while (!kill_thread)
            {
                enemies[0].Start();
                Thread.Sleep(10 * 1000);
            }
        }
    }
}
