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
        static bool have_been_called = false;
        public static void Startup(GameObject enemy)
        {
            if(have_been_called)
            {
                KillÁllThreads();
                threads_lifeline[0] = true;
            }
            enemies.Add(enemy);
            bool pathings = false;
            threads_lifeline.Add(pathings);
            Thread pathing = new Thread(UpdatePathing);
            pathing.IsBackground = true;
            pathing.Start();
            have_been_called = true;
        }
        static void UpdatePathing()
        {
            while (!threads_lifeline[0])
            {
                SceneManager.GetGameObjectByName("Enemy").GetComponent<Enemy>().GetPath();
                Thread.Sleep(5 * 1000);
            }
        }
        public static void KillÁllThreads()
        {
            for (int i = 0; i < threads_lifeline.Count; i++)
            {
                threads_lifeline[i] = true;
            }
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
