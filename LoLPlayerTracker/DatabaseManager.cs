using System;
using System.Data.SQLite;

namespace LoLPlayerTracker {
    class DatabaseManager {

        public SQLiteConnection dbConnection;

        public DatabaseManager(string dbName, int dbVersion) {
            Console.WriteLine("Hello i am the database manager");

            CreateDatabase(dbName);

            dbConnection = new SQLiteConnection("Data Source=" + dbName + ";Version=" + dbVersion.ToString() + ";");
            dbConnection.Open();

            CreateTable("players", "VARCHAR(16)", "INT");
            InsertRow("players", "'Faggot'", "90");
            ReadRows("players");
        }

        public void CreateDatabase(string dbName) {
            SQLiteConnection.CreateFile(dbName);
        }

        public void CreateTable(string tableName, string keyType, string valueType) {
            string command = "CREATE TABLE " + tableName + " (key " + keyType + ", value " + valueType + ")";
            new SQLiteCommand(command, dbConnection).ExecuteNonQuery();
        }

        public void InsertRow(string tableName, string key, string value) {
            string command = "INSERT INTO " + tableName + " (key, value) values (" + key + ", " + value + ")";
            new SQLiteCommand(command, dbConnection).ExecuteNonQuery();
        }

        public void ReadRows(string tableName) {
            string command = "SELECT * from " + tableName + " ORDER BY key DESC";
            SQLiteDataReader reader = new SQLiteCommand(command, dbConnection).ExecuteReader();
            while (reader.Read()) {
                Console.WriteLine("Key: " + reader["key"] + "\tValue: " + reader["value"]);
            }
        }
    }
}
