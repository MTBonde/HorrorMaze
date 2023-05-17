using System.Data.SQLite;
using System.Diagnostics;

namespace HorrorMaze
{
    public class SQLManager
    {

        private SQLiteConnection conn;

        public SQLManager(string databaseName)
        {
            conn = new SQLiteConnection("Data Source=" + databaseName + ".db; Version=3; New=true");
        }

        public void CreateTable(string tableName, string[] collumNames, TypeCode[] dataType)
        {
            OpenConn();
            string cmd = "CREATE TABLE IF NOT EXISTS " + tableName + " (ID INTEGER PRIMARY KEY AUTOINCREMENT, ";
            for (int i = 0; i < collumNames.Length; i++)
            {
                cmd += collumNames[i] + " ";
                switch (dataType[i])
                {
                    case TypeCode.String:
                        cmd += "VARCHAR(100)";
                        break;
                    case TypeCode.Int32:
                        cmd += "INTEGER";
                        break;
                }
                if (collumNames.Length - 1 > i)
                    cmd += ",";
            }
            cmd += ");";
            SQLiteCommand sqlcmd = new SQLiteCommand(cmd, conn);
            sqlcmd.ExecuteNonQuery();
            CloseConn();
            Debug.WriteLine("done");
        }

        public bool AddToTable(string tableName, string[] valueNames, object[] values)
        {
            if (valueNames.Length == values.Length)
            {
                OpenConn();
                string cmd = "INSERT INTO " + tableName + " (";
                for (int i = 0; i < values.Length; i++)
                {
                    cmd += valueNames[i];
                    if (valueNames.Length - 1 > i)
                        cmd += ", ";
                }
                cmd += ") VALUES(";
                for (int i = 0; i < values.Length; i++)
                {
                    switch (Type.GetTypeCode(values[i].GetType()))
                    {
                        case TypeCode.String:
                            cmd += "'" + (string)values[i] + "'";
                            break;
                        case TypeCode.Int32:
                            cmd += (int)values[i];
                            break;
                    }
                    if (values.Length - 1 > i)
                        cmd += ",";
                }
                cmd += ")";

                SQLiteCommand sqlcmd = new SQLiteCommand(cmd, conn);
                sqlcmd.ExecuteNonQuery();
                CloseConn();
                return true;
            }
            return false;
        }

        public object[] GetSortedAfter(string tableName, string sortCollumName)
        {
            OpenConn();
            string cmd = "SELECT * FROM " + tableName + " ORDER BY " + sortCollumName + " Limit 10";
            SQLiteCommand sqlcmd = new SQLiteCommand(cmd, conn);
            SQLiteDataReader reader = sqlcmd.ExecuteReader();

            object[] values = new object[reader.FieldCount];
            while (reader.Read())
            {
                int fieldCount = reader.GetValues(values);
            }

            CloseConn();
            return values;
        }

        public object[] GetValuesByID(string tableName, long id)
        {
            OpenConn();
            string cmd = "SELECT * from " + tableName + " WHERE ID = " + id;
            SQLiteCommand sqlcmd = new SQLiteCommand(cmd, conn);
            SQLiteDataReader reader = sqlcmd.ExecuteReader();

            object[] values = new object[reader.FieldCount];
            while (reader.Read())
            {
                int fieldCount = reader.GetValues(values);
            }


            CloseConn();
            return values;
        }

        private void OpenConn()
        {
            conn.Open();
        }

        private void CloseConn()
        {
            conn.Close();
        }
    }
}
