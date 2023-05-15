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
        static List<bool> threads_lifeline = new List<bool>();
        public static void Startup(GameObject enemy)
        {
            enemies.Add(enemy);
            bool pathings = false;
            threads_lifeline.Add(pathings);
            Thread pathing = new Thread(UpdatePathing);
            pathing.IsBackground = true;
            pathing.Start();
        }
        static void UpdatePathing()
        {
            while (!threads_lifeline[0])
            {
                enemies[0].Start();
                Thread.Sleep(10 * 1);
            }
        }
        static void KillÁllThreads()
        {
            for (int i = 0; i < threads_lifeline.Count; i++)
            {
                threads_lifeline[i] = true;
            }
            Thread killing = new Thread(KillThreading);
            killing.IsBackground = true;
            killing.Start();
        }
        static void KillThreading()
        {
            bool done = false;
            bool check_done = true;
            while (!done)
            {
                check_done = true;
                for (int i = 0; i < threads_lifeline.Count; i++)
                {
                    if (threads_lifeline[i] == true)
                    {
                        threads_lifeline.RemoveAt(i);
                        check_done = false;
                    }
                }
                if (check_done)
                    done = true;
            }
        }
    }
}
