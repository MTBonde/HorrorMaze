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
            if (have_been_called == true)
            {

            }
            else
            {
                enemies.Add(enemy);
                Thread pathing = new Thread(adder);
                pathing.IsBackground = true;
                pathing.Start();
                have_been_called = true;
            }
        }
        static void adder()
        {
            bool addet = false;
            while (!addet)
            {
                if (!threads_lifeline)
                {
                    Thread pathing = new Thread(UpdatePathing);
                    pathing.IsBackground = true;
                    pathing.Start();
                    addet = true;
                }
                Thread.Sleep(1 * 100);
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
                        Thread.Sleep(1 * 1000);
                    else
                        Thread.Sleep(3 * 1000);
                }
            }
        }
        public static void KillÁllThreads()
        {
            //for (int i = 0; i < threads_lifeline.Count; i++)
            //{
            //    threads_lifeline[i] = true;
            //}
            threads_lifeline = true;
        }
        static void KillThreading()
        {
            //bool done = false;
            //bool check_done = true;
            //while (!done)
            //{
            //    check_done = true;
            //    for (int i = 0; i < threads_lifeline.Count; i++)
            //    {
            //        if (threads_lifeline[i] == true)
            //        {
            //            threads_lifeline.RemoveAt(i);
            //            check_done = false;
            //        }
            //    }
            //    if (check_done)
            //        done = true;
            //}
        }
    }
}
