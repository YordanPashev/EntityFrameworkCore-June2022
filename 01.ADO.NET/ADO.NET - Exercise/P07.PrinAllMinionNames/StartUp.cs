namespace P07.PrintAllMinionNames
{
    using System;
    using System.Data.SqlClient;
    using P07.PrintAllMinionNames.Readers;
    using System.Collections.Generic;

    public class StartUp
    {
        public static void Main(string[] args)
        {
            SqlConnection sqlConnection = new SqlConnection(Config.ConnectionString);
            sqlConnection.Open();

            TextReader textReaderSelectAllMinionsQuery = new TextReader("SelectAllMinions.sql");
            string SelectALlMinions = textReaderSelectAllMinionsQuery.Read();
            SqlCommand getAllMinionsCmd = new SqlCommand(SelectALlMinions, sqlConnection);

            using SqlDataReader minionsReader = getAllMinionsCmd.ExecuteReader();

            List<string> minions = new List<string>();

            while (minionsReader.Read() == true)
            {
                minions.Add($"{minionsReader["Name"]}");
            }
            minionsReader.Close();

            int firstMinionIndex = 0;

            while (minions.Count > 1)
            {
                int lastMinionIndex = minions.Count - 1;
                Console.WriteLine(minions[firstMinionIndex]);
                Console.WriteLine(minions[lastMinionIndex]);
                minions.RemoveAt(lastMinionIndex);
                minions.RemoveAt(firstMinionIndex);
            }

            if (minions.Count == 1)
            {
                Console.WriteLine(minions[0]);
            }

            sqlConnection.Close();
        }
    }
}
