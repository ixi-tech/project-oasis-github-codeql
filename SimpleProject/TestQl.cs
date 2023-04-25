using System;
using System.Data.SqlClient;

namespace SimpleProject
{
    public class TestQl
    {
        public void CodeQlShouldFail(string[] args)
        {
            Console.WriteLine("Enter your user ID:");
            string userId = Console.ReadLine();

            SqlConnection connection = new SqlConnection("your_connection_string");
            connection.Open();

            // SQL injection vulnerability  
            string query = "SELECT * FROM Users WHERE UserId = " + userId;
            SqlCommand command = new SqlCommand(query, connection);
            SqlDataReader reader = command.ExecuteReader();

            while (reader.Read())
            {
                Console.WriteLine("User: {0}, Email: {1}", reader["Name"], reader["Email"]);
            }

            reader.Close();
            connection.Close();
        }
    }
}
