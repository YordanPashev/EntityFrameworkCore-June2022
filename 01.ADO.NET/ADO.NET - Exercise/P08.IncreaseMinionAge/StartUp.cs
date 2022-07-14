namespace P08.IncreaseMinionAge
{
    using System;
    using System.Text;
    using System.Linq;
    using System.Data.SqlClient;
    using P08.IncreaseMinionAge.Readers;

    public class StartUp
    {
        public static void Main(string[] args)
        {
            string[] selectedMinionIds = Console.ReadLine()
                .Split(" ", StringSplitOptions.RemoveEmptyEntries)
                .ToArray();
            SqlConnection sqlConnection = new SqlConnection(Config.ConnectionString);
            sqlConnection.Open();

            foreach (var minionId in selectedMinionIds)
            {
                TextReader textReaderSelectIncreaseAgeByOne = new TextReader("IncreaseSelectedMinionsAgeByOne.sql");
                string UpdateAgeQuery = textReaderSelectIncreaseAgeByOne.Read();
                SqlCommand increaseAgeByOneCmd = new SqlCommand(UpdateAgeQuery, sqlConnection);
                increaseAgeByOneCmd.Parameters.AddWithValue("@SelectedIds", minionId);
                increaseAgeByOneCmd.ExecuteNonQuery();
            }
           
            TextReader textReaderSelectAllMinionsQuery = new TextReader("SelectAllMinions.sql");
            string SelectALlMinions = textReaderSelectAllMinionsQuery.Read();
            SqlCommand getAllMinionsCmd = new SqlCommand(SelectALlMinions, sqlConnection);

            using SqlDataReader minionsReader = getAllMinionsCmd.ExecuteReader();

           StringBuilder result = new StringBuilder();

            while (minionsReader.Read() == true)
            {
                result.AppendLine($"{minionsReader["Name"]} {minionsReader["Age"]}");
            }

            minionsReader.Close();
            sqlConnection.Close();

            Console.WriteLine(result.ToString());
        }
    }
}
