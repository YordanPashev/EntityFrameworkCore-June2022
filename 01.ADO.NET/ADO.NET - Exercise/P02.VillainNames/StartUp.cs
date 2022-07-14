namespace P02.VillainNames
{
    using System;
    using System.Text;
    using System.Data.SqlClient;
    using P02.VillainNames.Readers;

    public class StartUp
    {
        public static void Main(string[] args)
        {
            using SqlConnection sqlConnection = new SqlConnection(Config.ConnectionString);
            sqlConnection.Open();

            TextReader sqlQueryTextReader = new TextReader("Query.sql");

            string query = sqlQueryTextReader.Read();

            SqlCommand selecAllVilianWithTheCOUntOfTheirMinions = new SqlCommand(query, sqlConnection);

            using SqlDataReader villianSINfoReader = selecAllVilianWithTheCOUntOfTheirMinions.ExecuteReader();
            StringBuilder result = new StringBuilder();

            while (villianSINfoReader.Read() == true)
            {
                string villianName = (string)villianSINfoReader["Villain Name"];
                int numberOfMinions = (int)villianSINfoReader["Number of Minions"];

                result.AppendLine(new string($"{villianName} - {numberOfMinions}"));
            }

            Console.WriteLine(result.ToString().TrimEnd());

            sqlConnection.Close();
        }
    }
}
