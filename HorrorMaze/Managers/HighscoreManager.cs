using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HorrorMaze
{
    public static class HighscoreManager
    {

        private static SQLManager manager = new SQLManager("HorrorMaze");

        public static void Setup()
        {
            manager.CreateTable("highscore",new string[2] { "names", "scores" }, new TypeCode[2] { TypeCode.String, TypeCode.Int32 });
            Debug.WriteLine(GetScoreboard());
        }

        public static void AddScore(string name, int score)
        {
            manager.AddToTable("highscore",new string[2] { "names", "scores" }, new object[2] { name, score });
        }

        public static string GetScoreboard() 
        {
            string scoreboard = "";
            object[] scores = manager.GetSortedAfter("highscore", "scores");
            for (int i = 0; i < scores.Length; i++)
            {
                if (i % 2 == 0)
                    scoreboard += scores[i].ToString() + ":   ";
                else
                    scoreboard += scores[i].ToString() + "\n";
            }
            return scoreboard;
        }
    }
}
