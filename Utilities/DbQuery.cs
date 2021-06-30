using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System;
using System.Data.SqlClient;

namespace ADL.PersonalizedTravel.Utilities
{
    public class DbQuery :Controller
    {
       
        public string GetDbResultSet(string userId)
        {
            string clusterId = "0";
            string sqlConnection = Startup.StaticConfig.GetConnectionString("AzureDbConnection");
           
            using (var connection = new SqlConnection(sqlConnection))
            {
                string column = string.Empty;
                if (User?.Identity?.IsAuthenticated ?? false)
                    column = "UserAuthenticatedId";
                else
                    column = "UserId";

                //TODO: remove this code, just for testing
                userId = "nrDJJrO7R9+wXSYKT8y6Sz";

                var sql = "SELECT TOP(1) * FROM traveldemoclusters where " + column + " = @userId";
                try
                {
                    connection.Open();
                    SqlCommand command = new SqlCommand(sql, connection);
                    command.Parameters.Add(new SqlParameter("@userId", userId));
                    SqlDataReader reader = command.ExecuteReader();
                    if (reader.Read())
                    {
                        clusterId = Convert.ToString(reader["Assignments"]);
                    }
                    return clusterId == "0" ? "1" : clusterId;
                }
                catch (Exception ex)
                {
                    throw;
                }

            }
        }
    }
}
