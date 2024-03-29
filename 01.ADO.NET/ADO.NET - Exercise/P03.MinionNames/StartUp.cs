﻿namespace P03.MinionNames
{
    using System;
    using System.Text;
    using System.Data.SqlClient;
    using P03.MinionNames.Readers;

    public class StartUp
    {
        public static void Main(string[] args)
        {
            int villainId = int.Parse(Console.ReadLine());

            using SqlConnection sqlConnection = new SqlConnection(Config.ConnectionString);
            sqlConnection.Open();

            TextReader sqlQueryTextReaderForVillain = new TextReader("QueryForVillainName.sql");
            string getVillainsNameQuery = sqlQueryTextReaderForVillain.Read();
            SqlCommand getVillainsName = new SqlCommand(getVillainsNameQuery, sqlConnection);
            getVillainsName.Parameters.AddWithValue("@VillainId", villainId);

            string villainName = (string)getVillainsName.ExecuteScalar();

            TextReader sqlQueryTextReaderForVillainMinions = new TextReader("QueryForVillainMinions.sql");
            string getAllVillainMinionsQuery = sqlQueryTextReaderForVillainMinions.Read();
            SqlCommand getAllVillainMinions = new SqlCommand(getAllVillainMinionsQuery, sqlConnection);
            getAllVillainMinions.Parameters.AddWithValue("@VillainId", villainId);

            using SqlDataReader minionsInfoReader = getAllVillainMinions.ExecuteReader();

            if (villainName == null)
            {
                Console.WriteLine($"No villain with ID {villainId} exists in the database.");
            }

            else
            {
                DisplayVillainAndHisMinions(minionsInfoReader, villainName);
            }

            sqlConnection.Close();
        }

        private static void DisplayVillainAndHisMinions(SqlDataReader minionsInfoReader, string villainName)
        {
            StringBuilder result = new StringBuilder();
            result.AppendLine($"Villain: {villainName}");

            int count = 0;

            if (!minionsInfoReader.HasRows)
            {
                result.AppendLine("(no minions)");
            }

            else
            {
                while (minionsInfoReader.Read() == true)
                {
                    count++;
                    string minionName = (string)minionsInfoReader["Minion Name"];
                    int minionAge = (int)minionsInfoReader["Minion Age"];

                    result.AppendLine($"{count}. {minionName} {minionAge}");
                }
            }

            Console.WriteLine(result.ToString().TrimEnd());          
        }
    }
}
