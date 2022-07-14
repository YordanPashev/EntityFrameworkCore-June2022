namespace P04.AddMinion
{
    using System;
    using System.Linq;
    using System.Data.SqlClient;
    using P04.AddMinion.Readers;

    public class StartUp
    {
        public static void Main(string[] args)
        {
            string[] minionsInfo = Console.ReadLine()
                .Split(" ", StringSplitOptions.RemoveEmptyEntries)
                .ToArray();
            string[] villianInfo = Console.ReadLine()
                .Split(" ", StringSplitOptions.RemoveEmptyEntries)
                .ToArray();

            string minionName = minionsInfo[1];
            int minionAge = int.Parse(minionsInfo[2]);
            string minionTown = minionsInfo[3];
            string villainName = villianInfo[1];

            using SqlConnection sqlConnection = new SqlConnection(Config.ConnectionString);
            sqlConnection.Open();

            int townId = GetTownID(sqlConnection, minionTown);

            int villainId = GetVillainId(sqlConnection, villainName);

            AddMinionToDatabase(sqlConnection, minionName, minionAge, townId);

            int minionId = GetMinionId(sqlConnection, minionName);

            AddMinionToVillain(sqlConnection, villainId, minionId, minionName, villainName);

            sqlConnection.Close();
        }

        private static int GetMinionId(SqlConnection sqlConnection, string minionName)
        {
            TextReader textReaderForMinion = new TextReader("GetMinionId.sql");
            string minionIndQuery = textReaderForMinion.Read();
            SqlCommand getMinionId = new SqlCommand(minionIndQuery, sqlConnection);
            getMinionId.Parameters.AddWithValue("@MinionName", minionName);

            return (int)getMinionId.ExecuteScalar();
        }

        private static int GetVillainId(SqlConnection sqlConnection, string villainName)
        {
            TextReader textReaderForVillainId = new TextReader("GetVillainId.sql");
            string villainIdQuery = textReaderForVillainId.Read();
            SqlCommand getVillainId = new SqlCommand(villainIdQuery, sqlConnection);
            getVillainId.Parameters.AddWithValue("@VillainName", villainName);

            object villainId = getVillainId.ExecuteScalar();
            if (villainId == null)
            {
                AddVillainToDatabase(villainName, sqlConnection);
                return GetVillainId(sqlConnection, villainName);
            }

            return (int)villainId;
        }

        private static int GetTownID(SqlConnection sqlConnection, string minionTown)
        {
            TextReader textReaderForTownId = new TextReader("GetMinionTownId.sql");
            string InsertTownQuery = textReaderForTownId.Read();
            SqlCommand geTownId = new SqlCommand(InsertTownQuery, sqlConnection);
            geTownId.Parameters.AddWithValue("@MinionTown", minionTown);

            object towndId = geTownId.ExecuteScalar();
            if (towndId == null)
            {
                AddTownToDatabase(minionTown, sqlConnection);
                return GetTownID(sqlConnection, minionTown);
            }

            return (int)towndId;
        }

        private static void AddVillainToDatabase(string villainName, SqlConnection sqlConnection)
        {
            TextReader textReaderForInsertingAVillain = new TextReader("InsertVillainIntoDatabaseQuery.sql");
            string insertVillainQuery = textReaderForInsertingAVillain.Read();
            SqlCommand insertVillainToDatabase = new SqlCommand(insertVillainQuery, sqlConnection);
            insertVillainToDatabase.Parameters.AddWithValue("@VillainName", villainName);

            insertVillainToDatabase.ExecuteNonQuery();
            Console.WriteLine($"Villain {villainName} was added to the database.");
        }

        private static void AddTownToDatabase(string minionTown, SqlConnection sqlConnection)
        {
            TextReader textReaderForInsertingATown = new TextReader("InsertTownIntoDatabaseQuery.sql");
            string townIdQuery = textReaderForInsertingATown.Read();
            SqlCommand insertTownToDatabase = new SqlCommand(townIdQuery, sqlConnection);
            insertTownToDatabase.Parameters.AddWithValue("@TownName", minionTown);

            insertTownToDatabase.ExecuteNonQuery();
            Console.WriteLine($"Town {minionTown} was added to the database.");
        }

        private static void AddMinionToDatabase(SqlConnection sqlConnection, string minionName, int minionAge, int townId)
        {
            TextReader textReaderForInsertingAminion = new TextReader("InsertMinionQuery.sql");
            string insertMinionQuery = textReaderForInsertingAminion.Read();
            SqlCommand insertMinionToDatabase = new SqlCommand(insertMinionQuery, sqlConnection);

            insertMinionToDatabase.Parameters.AddWithValue("@MinionName", minionName);
            insertMinionToDatabase.Parameters.AddWithValue("@MinionAge", minionAge);
            insertMinionToDatabase.Parameters.AddWithValue("@MinionTownId", townId);

            insertMinionToDatabase.ExecuteNonQuery();
        }

        private static void AddMinionToVillain(SqlConnection sqlConnection, int villainId, int minionId, string minionName, string villainName)
        {
            TextReader textReaderForInsertVillainsMinion = new TextReader("InsertMinionToVillain.sql");
            string addMinionToVillainQuery = textReaderForInsertVillainsMinion.Read();
            SqlCommand insertMinionToVillain = new SqlCommand(addMinionToVillainQuery, sqlConnection);
            insertMinionToVillain.Parameters.AddWithValue("@VillainId", villainId);
            insertMinionToVillain.Parameters.AddWithValue("@MinionId", minionId);

            insertMinionToVillain.ExecuteNonQuery();
            Console.WriteLine($"Successfully added {minionName} to be minion of {villainName}");
        }
    }
}
