namespace P02.VillainNames
{
    using System;
    using System.Data.SqlClient;
    using System.Text;

    public class Program
    {
        public static void Main(string[] args)
        {
            using SqlConnection sqlConnection = new SqlConnection(Config.ConnectionString);
            sqlConnection.Open();

            string query = @"SELECT v.[Name] AS [Villain Name],
                                    COUNT(m.[Id]) AS [Number of Minions]
                               FROM [Villains] AS v
                          LEFT JOIN [MinionsVillains] as mv
                                 ON v.[Id] = mv.[VillainId] 
                          LEFT JOIN [Minions] AS m
                                 ON mv.[MinionId] = m.[Id]
                           GROUP BY v.[Name]
                             HAVING COUNT(m.[Id]) > 3
                           ORDER BY COUNT(m.[Id]) DESC";

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
