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

            SqlTransaction sqlTransaction = sqlConnection.BeginTransaction();           
            try
            {
                int townId = GetTownID(sqlConnection, minionTown, sqlTransaction);

                int villainId = GetVillainId(sqlConnection, villainName, sqlTransaction);

                AddMinionToDatabase(sqlConnection, minionName, minionAge, townId, sqlTransaction);

                int minionId = GetMinionId(sqlConnection, minionName, sqlTransaction);

                AddMinionToVillain(sqlConnection, villainId, minionId, minionName, villainName, sqlTransaction);

                sqlTransaction.Commit();
                sqlConnection.Close();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        private static int GetMinionId(SqlConnection sqlConnection, string minionName, SqlTransaction sqlTransaction)
        {
            TextReader textReaderForMinion = new TextReader("GetMinionId.sql");
            string minionIndQuery = textReaderForMinion.Read();
            SqlCommand getMinionId = new SqlCommand(minionIndQuery, sqlConnection, sqlTransaction);
            getMinionId.Parameters.AddWithValue("@MinionName", minionName);

            return (int)getMinionId.ExecuteScalar();
        }

        private static int GetVillainId(SqlConnection sqlConnection, string villainName, SqlTransaction sqlTransaction)
        {
            TextReader textReaderForVillainId = new TextReader("GetVillainId.sql");
            string villainIdQuery = textReaderForVillainId.Read();
            SqlCommand getVillainId = new SqlCommand(villainIdQuery, sqlConnection, sqlTransaction);
            getVillainId.Parameters.AddWithValue("@VillainName", villainName);

            object villainId = getVillainId.ExecuteScalar();
            if (villainId == null)
            {
                AddVillainToDatabase(villainName, sqlConnection, sqlTransaction);
                return GetVillainId(sqlConnection, villainName, sqlTransaction);
            }

            return (int)villainId;
        }

        private static int GetTownID(SqlConnection sqlConnection, string minionTown, SqlTransaction sqlTransaction)
        {
            TextReader textReaderForTownId = new TextReader("GetMinionTownId.sql");
            string InsertTownQuery = textReaderForTownId.Read();
            SqlCommand geTownId = new SqlCommand(InsertTownQuery, sqlConnection, sqlTransaction);
            geTownId.Parameters.AddWithValue("@MinionTown", minionTown);

            object towndId = geTownId.ExecuteScalar();
            if (towndId == null)
            {
                AddTownToDatabase(minionTown, sqlConnection, sqlTransaction);
                return GetTownID(sqlConnection, minionTown, sqlTransaction);
            }

            return (int)towndId;
        }

        private static void AddVillainToDatabase(string villainName, SqlConnection sqlConnection, SqlTransaction sqlTransaction)
        {
            TextReader textReaderForInsertingAVillain = new TextReader("InsertVillainIntoDatabaseQuery.sql");
            string insertVillainQuery = textReaderForInsertingAVillain.Read();
            SqlCommand insertVillainToDatabase = new SqlCommand(insertVillainQuery, sqlConnection, sqlTransaction);
            insertVillainToDatabase.Parameters.AddWithValue("@VillainName", villainName);

            insertVillainToDatabase.ExecuteNonQuery();
            Console.WriteLine($"Villain {villainName} was added to the database.");
        }

        private static void AddTownToDatabase(string minionTown, SqlConnection sqlConnection, SqlTransaction sqlTransaction)
        {
            TextReader textReaderForInsertingATown = new TextReader("InsertTownIntoDatabaseQuery.sql");
            string townIdQuery = textReaderForInsertingATown.Read();
            SqlCommand insertTownToDatabase = new SqlCommand(townIdQuery, sqlConnection, sqlTransaction);
            insertTownToDatabase.Parameters.AddWithValue("@TownName", minionTown);

            insertTownToDatabase.ExecuteNonQuery();
            Console.WriteLine($"Town {minionTown} was added to the database.");
        }

        private static void AddMinionToDatabase(SqlConnection sqlConnection, string minionName, int minionAge, int townId, SqlTransaction sqlTransaction)
        {
            TextReader textReaderForInsertingAminion = new TextReader("InsertMinionQuery.sql");
            string insertMinionQuery = textReaderForInsertingAminion.Read();
            SqlCommand insertMinionToDatabase = new SqlCommand(insertMinionQuery, sqlConnection, sqlTransaction);

            insertMinionToDatabase.Parameters.AddWithValue("@MinionName", minionName);
            insertMinionToDatabase.Parameters.AddWithValue("@MinionAge", minionAge);
            insertMinionToDatabase.Parameters.AddWithValue("@MinionTownId", townId);

            insertMinionToDatabase.ExecuteNonQuery();
        }

        private static void AddMinionToVillain(SqlConnection sqlConnection, int villainId, int minionId, string minionName, string villainName, SqlTransaction sqlTransaction)
        {
            TextReader textReaderForInsertVillainsMinion = new TextReader("InsertMinionToVillain.sql");
            string addMinionToVillainQuery = textReaderForInsertVillainsMinion.Read();
            SqlCommand insertMinionToVillain = new SqlCommand(addMinionToVillainQuery, sqlConnection, sqlTransaction);
            insertMinionToVillain.Parameters.AddWithValue("@VillainId", villainId);
            insertMinionToVillain.Parameters.AddWithValue("@MinionId", minionId);

            insertMinionToVillain.ExecuteNonQuery();
            Console.WriteLine($"Successfully added {minionName} to be minion of {villainName}");
        }
    }
}
