namespace P09.IncreaseAgeStoredProcedure
{
    using System;
    using System.Data.SqlClient;

    public class StartUp
    {
        public static void Main(string[] args)
        {
            int minionId = int.Parse(Console.ReadLine());

            SqlConnection sqlConnection = new SqlConnection(Config.ConnectionString);
            sqlConnection.Open();

            try
            {
                string increadeMinionAgeQurey = @"EXEC dbo.[usp_IncreaseMinionsAgeByOne] @minionId";

                SqlCommand increaseMinionsAgeCmd = new SqlCommand(increadeMinionAgeQurey, sqlConnection);
                increaseMinionsAgeCmd.Parameters.AddWithValue("@MinionId", minionId);
                SqlDataReader increaseMinionsAgeReader = increaseMinionsAgeCmd.ExecuteReader();

                if (!increaseMinionsAgeReader.HasRows)
                {
                    throw new InvalidOperationException("The Miniond ID does not exist!");
                }

                increaseMinionsAgeReader.Read();
 
                Console.WriteLine($"{increaseMinionsAgeReader["Name"]} - {increaseMinionsAgeReader["Age"]} years old");
            }

            catch (InvalidOperationException ex)
            {
                Console.WriteLine(ex.Message);
            }

            sqlConnection.Close();
        }
    }
}
