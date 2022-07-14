namespace P02.VillainNames
{
    using System;
    using System.Text;
    using System.Data.SqlClient;
    using P02.VillainNames.Readers;

    public class Program
    {
        public static void Main(string[] args)
        {
            using SqlConnection sqlConnection = new SqlConnection(Config.ConnectionString);
            sqlConnection.Open();

            SQLQueryReader sqlQueryReader = new SQLQueryReader("Query");
            string query = sqlQueryReader.ReadFileText();

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
