using System;
using System.Data.SqlClient;

namespace Demo
{
    public class Program
    {
        public static void Main(string[] args)
        {
            using SqlConnection sqlConnection= new SqlConnection(Config.ConnectionString);
            sqlConnection.Open();

            string employeeCountQuery = $"SELECT COUNT(*) AS [EmployeeCount]" +
                              "FROM [Employees]";

            SqlCommand employeeCountCommand = new SqlCommand(employeeCountQuery, sqlConnection);

            int employeeCount = (int)employeeCountCommand.ExecuteScalar();

            Console.WriteLine($"Total employees: {employeeCount}.");

            string selectedJobTitle = Console.ReadLine();
            string slsecEployeeByJObTItleQuery = @"SELECT [JobTitle],
                                                          CONCAT([FirstName], ' ', LastName) AS [Full Name]
                                                     FROM [Employees]
                                                    WHERE [JobTitle] = @jobTitle";

            SqlCommand eployeesByJobTitle = new SqlCommand(slsecEployeeByJObTItleQuery, sqlConnection);
            eployeesByJobTitle.Parameters.AddWithValue("@jobTitle", selectedJobTitle);

            using SqlDataReader employeeByJobTItleReader = eployeesByJobTitle.ExecuteReader();

            string[] eployeesInfoByJobTitle = new string[employeeCount];
            int emplyeeIndex = 0;

            while (employeeByJobTItleReader.Read() == true)
            {

                string department = (string)employeeByJobTItleReader["JobTitle"];
                string fullName = (string)employeeByJobTItleReader["Full Name"];

                eployeesInfoByJobTitle[emplyeeIndex++] = $"#{emplyeeIndex + 1}. {department}  ->  {fullName}.";
            }

            foreach (string employee in eployeesInfoByJobTitle)
            {
                Console.WriteLine(employee);
            }
        }
    }
}
