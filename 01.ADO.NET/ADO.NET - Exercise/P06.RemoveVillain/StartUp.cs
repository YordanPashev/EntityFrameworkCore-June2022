namespace P06.RemoveVillain
{
    using System;
    using System.Data.SqlClient;
    using P06.RemoveVillain.Readers;
    using System.Collections.Generic;

    public class StartUp
    {
        public static void Main(string[] args)
        {
            int villainId = int.Parse(Console.ReadLine());
            List<int> minionsIdToRemove = new List<int>();
            int removedMinionCounter = 0;
            string villainName = string.Empty;

            SqlConnection sqlConnection = new SqlConnection(Config.ConnectionString);
            sqlConnection.Open();
            SqlTransaction sqlTransaction = sqlConnection.BeginTransaction();

            TextReader textReaderSelectVillainInfo = new TextReader("GetVillainsInfo.sql");
            string selectedvillainInfoQuery = textReaderSelectVillainInfo.Read();
            SqlCommand getVillainInfo = new SqlCommand(selectedvillainInfoQuery, sqlConnection, sqlTransaction);
            getVillainInfo.Parameters.AddWithValue("@VillainId", villainId);

            SqlDataReader villainInfoReader = getVillainInfo.ExecuteReader();

            try
            {
                if (!villainInfoReader.HasRows)
                {
                    Console.WriteLine("No such villain was found.");
                }

                else
                {
                    while (villainInfoReader.Read() == true)
                    {
                        villainName = (string)villainInfoReader["VillainName"];
                        minionsIdToRemove.Add((int)villainInfoReader["MinionId"]);
                        removedMinionCounter++;
                    }

                    villainInfoReader.Close();

                    TextReader textReaderDeleteVillain = new TextReader("DeleteVillainFromDatabase.sql");
                    string deleteVillainQuery = textReaderDeleteVillain.Read();
                    SqlCommand removeVillainFromDatabaseCmd = new SqlCommand(deleteVillainQuery, sqlConnection, sqlTransaction);
                    removeVillainFromDatabaseCmd.Parameters.AddWithValue("@VillainId", villainId);

                    removeVillainFromDatabaseCmd.ExecuteNonQuery();

                    foreach (var minionId in minionsIdToRemove)
                    {
                        TextReader textReaderDeleteVillainsMinions = new TextReader("DeleteVillainsMinions.sql");
                        string deleteVillainMinionsQuery = textReaderDeleteVillainsMinions.Read();
                        SqlCommand removeVillainMinionsFromDatabaseCmd = new SqlCommand(deleteVillainMinionsQuery, sqlConnection, sqlTransaction);
                        removeVillainMinionsFromDatabaseCmd.Parameters.AddWithValue("@MinionId", minionId);

                        removeVillainMinionsFromDatabaseCmd.ExecuteNonQuery();
                    }

                    Console.WriteLine($"{villainName} was deleted.");
                    Console.WriteLine($"{removedMinionCounter} minions were released.");

                    sqlTransaction.Commit();
                }
            }

            catch (Exception ex)
            {
                sqlTransaction.Rollback();
                Console.WriteLine("You are trying to delete a minion which has more that 1 villain!");
                Console.WriteLine(ex.Message);
            }

            sqlConnection.Close();
        }
    }
}
