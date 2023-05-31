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
        static List<GameObject> enemies = new List<GameObject>();
        static bool threads_lifeline = false;
        static bool have_been_called = false;
        public static void Startup(GameObject enemy)
        {
            threads_lifeline = false;
            if (have_been_called == false)
            {
                enemies.Add(enemy);
                Thread pathing = new Thread(UpdatePathing);
                pathing.IsBackground = true;
                pathing.Start();
                Thread update = new Thread(UpdateEnemy);
                update.IsBackground = true;
                update.Start();
                have_been_called = true;
            }
        }
        static void UpdatePathing()
        {
            while (true)
            {
                while (!threads_lifeline)
                {
                    SceneManager.GetGameObjectByName("Enemy").GetComponent<Enemy>().GetPath();
                    if (SceneManager.GetGameObjectByName("Enemy").GetComponent<Enemy>().Hunting())
                        Thread.Sleep(1 * 200);
                    else
                        Thread.Sleep(3 * 1000);
                }
            }
        }
        static void UpdateEnemy()
        {
            while (true)
            {
                while (!threads_lifeline)
                {
                    Thread.Sleep(16);
                    if (!threads_lifeline)
                        SceneManager.GetGameObjectByName("Enemy").GetComponent<Enemy>().Update_();
                }
            }
        }
        public static void StopThreads()
        {
            threads_lifeline = true;
        }
    }
}
