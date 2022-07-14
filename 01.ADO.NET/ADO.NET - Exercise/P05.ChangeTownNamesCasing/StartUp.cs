namespace P05.ChangeTownNamesCasing
{
    using System;
    using System.Data.SqlClient;
    using P05.ChangeTownNamesCasing.Readers;
    using System.Collections.Generic;

    public class StartUp
    {
        public static void Main(string[] args)
        {
            string inputCountry = Console.ReadLine();

            SqlConnection sqlConnection = new SqlConnection(Config.ConnectionString);
            sqlConnection.Open();

            TextReader textReaderForGettingAllTowns = new TextReader("SelectAllCitiesInCountry.sql");
            string getAllTowns = textReaderForGettingAllTowns.Read();
            SqlCommand getAllTownsCmd = new SqlCommand(getAllTowns, sqlConnection);
            getAllTownsCmd.Parameters.AddWithValue("@CountryName", inputCountry);

            using SqlDataReader townsReader = getAllTownsCmd.ExecuteReader();

            if (!townsReader.HasRows)
            {
                Console.WriteLine("No town names were affected.");
                Environment.Exit(0);
            }

            List<string> namesChangedList = new List<string>();
            int changedNamesCount = 0;
            int countryCode = 0;

            while (townsReader.Read() == true )
            {
                countryCode = (int)townsReader["CountryCode"];
                namesChangedList.Add($"{townsReader["Town Name"].ToString().ToUpper().TrimEnd()}");
                changedNamesCount++;
            }
            townsReader.Close();

            TextReader textReaderUpdateNames = new TextReader("UpdateTownsNames.sql");
            string updateAllTownsNames = textReaderUpdateNames.Read();
            SqlCommand updateTownsNamesCmd = new SqlCommand(updateAllTownsNames, sqlConnection);
            updateTownsNamesCmd.Parameters.AddWithValue("@CountryCode", countryCode);
            updateTownsNamesCmd.ExecuteNonQuery();

            Console.WriteLine($"{changedNamesCount} town names were affected.");
            Console.WriteLine($"[{string.Join(", ", namesChangedList)}]");
        }
    }
}
