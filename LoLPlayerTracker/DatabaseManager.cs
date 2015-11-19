using System;
using System.Data.SQLite;
using System.IO;

namespace LoLPlayerTracker {
    public class DatabaseManager {

        public SQLiteConnection dbConnection;

        public DatabaseManager(string dbName, int dbVersion) {
            InitDb(dbName, dbVersion);
        }

        public void InitDb(string dbName, int dbVersion) {
            // Create database if it does not exist
            bool newDb = false;
            if (!File.Exists(dbName)) {
                newDb = true;
                CreateDatabase(dbName);
            }

            // Create connection
            dbConnection = new SQLiteConnection("Data Source=" + dbName + ";Version=" + dbVersion.ToString() + ";");
            dbConnection.Open();

            // Create tables if they do not exist
            if (newDb) {
                CreateTable("user", "VARCHAR(16)", "VARCHAR(16)");
                CreateTable("players", "VARCHAR(16)", "INT");
            }

            // TODO temp test
            InsertRow("players", "'RandomPlayer2'", "90");
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
