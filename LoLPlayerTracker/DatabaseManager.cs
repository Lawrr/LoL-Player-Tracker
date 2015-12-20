using RiotSharp.CurrentGameEndpoint;
using RiotSharp.SummonerEndpoint;
using System;
using System.Data.SQLite;
using System.IO;

namespace LoLPlayerTracker {
    public class DatabaseManager {

        public static string PLAYERS_TABLE = "Players";

        public SQLiteConnection dbConnection;

        public DatabaseManager(string dbName, int dbVersion) {
            InitDatabase(dbName, dbVersion);
        }

        public int FindNumResults(string tableName, string key) {
            string command = "SELECT count(*) from " + tableName + " WHERE Key = '" + key + "';";
            int count = Convert.ToInt32(new SQLiteCommand(command, dbConnection).ExecuteScalar());
            return count;
        }

        public void AddGame(Summoner summoner, CurrentGame game) {
            foreach (Participant p in game.Participants) {
                if (p.SummonerName != Program.MainForm.GetSummonerName()) {
                    if (!KeyValueExists(PLAYERS_TABLE, p.SummonerId.ToString(), game.GameId.ToString())) {
                        InsertRow(PLAYERS_TABLE, p.SummonerId.ToString(), game.GameId.ToString());
                    }
                }
            }
        }

        private void InitDatabase(string dbName, int dbVersion) {
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
                CreateTable(PLAYERS_TABLE, "INT", "INT");
            }
        }

        private void CreateDatabase(string dbName) {
            SQLiteConnection.CreateFile(dbName);
        }

        private void CreateTable(string tableName, string keyType, string valueType) {
            string command = "CREATE TABLE " + tableName + " (Key " + keyType + ", Value " + valueType + ");";
            new SQLiteCommand(command, dbConnection).ExecuteNonQuery();
        }

        private void InsertRow(string tableName, string key, string value) {
            string command = "INSERT INTO " + tableName + " (Key, Value) values (" + key + ", " + value + ");";
            new SQLiteCommand(command, dbConnection).ExecuteNonQuery();
        }

        private SQLiteDataReader FindKey(string tableName, string key) {
            string command = "SELECT * from " + tableName + " WHERE Key = " + key + " ORDER BY key DESC;";
            SQLiteDataReader reader = new SQLiteCommand(command, dbConnection).ExecuteReader();
            return reader;
        }

        private bool KeyValueExists(string tableName, string key, string value) {
            string command = "SELECT * from " + tableName + " WHERE Key = " + key + " AND Value = " + value + ";";
            SQLiteDataReader reader = new SQLiteCommand(command, dbConnection).ExecuteReader();
            return reader.Read();
        }

    }
}
