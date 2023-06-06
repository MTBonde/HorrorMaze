using System.Data.SQLite;

namespace HorrorMaze
{
    /// <summary>
    /// A manager that handles the highscore
    /// Thorbjørn
    /// </summary>
    public class HighscoreManager
    {
        // readonly IDbConnection nnection;
        public static void Starter()
        {
            //DropTable();
            Create_standarTable();
        }
        public static void New_Score(string new_name, int time)
        {
            List<string[]> scores = CommandRead();
            for (int i = 0; i < scores.Count; i++)
            {
                if (int.Parse(scores[i][2]) > time)
                {
                    SortScore(new_name, time, scores, i);
                    return;
                }
            }
            if (scores.Count < 10)
                CommandInsertLast(new_name, time);
        }
        static void SortScore(string new_name, int time, List<string[]> scores, int i)
        {
            for (int j = 0; j < scores.Count - i; j++)
            {
                CommandDelete(int.Parse(scores[i + j][0]));
            }
            for (int j = 0; j < scores.Count - i + 1; j++)
            {
                if (j == 0)
                    CommandInsertLast(new_name, time);
                else
                    if (i + 1 + j < 11)
                    CommandInsertLast(scores[i + j - 1][1], int.Parse(scores[i + j - 1][2]));

            }
        }
        static void CommandInsertLast(string name, int values)
        {
            var connection = Connection();
            connection.Open();
            var command = new SQLiteCommand($"INSERT INTO Highscore (Name, Score) VALUES ('{name}', '{values}');", connection);
            command.ExecuteNonQuery();
            connection.Close();
        }
        static void CommandDelete(int key)
        {
            if (key == 3)
            {
                List<string[]> hey = CommandRead();
            }
            var connection = Connection();
            connection.Open();
            string text = "DELETE FROM Highscore Where Id = " + key + ";"; // $"DELETE FROM Highscore Where Id = '{key}';"
            var command = new SQLiteCommand(text, connection);
            command.ExecuteNonQuery();
            connection.Close();

        }
        public static List<string[]> CommandRead()
        {
            var connection = Connection();
            connection.Open();
            var command = new SQLiteCommand("SELECT * FROM Highscore", connection);
            var result = command.ExecuteReader();
            List<string[]> all_results = new List<string[]>();

            while (result.Read())
            {
                string id = result.GetInt32(0).ToString();
                string name = result.GetString(1);
                string score = result.GetInt32(2).ToString();
                string[] results = new string[3] { id, name, score };
                all_results.Add(results);
            }
            connection.Close();
            return all_results;
        }
        static void Create_standarTable()
        {
            var connection = Connection();
            connection.Open();
            var command = new SQLiteCommand("CREATE TABLE IF NOT EXISTS Highscore (Id INTEGER PRIMARY KEY, Name VARCHAR(50), Score INTEGER);", connection);
            command.ExecuteNonQuery();
            connection.Close();
        }
        static void DropTable()
        {
            var connection = Connection();
            connection.Open();
            var command = new SQLiteCommand("DROP TABLE Highscore", connection);
            command.ExecuteNonQuery();
            connection.Close();
        }
        static SQLiteConnection Connection()
        {
            var connection = new SQLiteConnection("Data Source=Highscores.db;Version=3;New=true");
            return connection;
        }
    }
}
