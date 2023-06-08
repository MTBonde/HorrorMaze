
using System.Threading;


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
            enemies.Add(enemy);
            if (have_been_called == false)
            {
                Thread update = new Thread(UpdateEnemy);
                update.IsBackground = true;
                update.Start();
                have_been_called = true;
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
                        for (int i = 0; i < enemies.Count; i++)
                            if (!threads_lifeline)
                                if (enemies[i].GetComponent<Enemy>().enabled)
                                    enemies[i].GetComponent<Enemy>().Update_();
                }
                Thread.Sleep(50);
            }
        }
        public static void StopThreads()
        {
            threads_lifeline = true;
        }
    }
}
