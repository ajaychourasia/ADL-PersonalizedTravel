using Microsoft.AspNetCore.Mvc;
using System;
using System.Data.SqlClient;

namespace ADL.PersonalizedTravel.Utilities
{
    public class DbQuery :Controller
    {
        public string GetDbResultSet(string userId)
        {
            string clusterId = "0";
            string SqlConnection = "Server=tcp:traveldemosqlserver.database.windows.net,1433;Initial Catalog=traveldemoapp;Persist Security Info=False;User ID=azureuser;Password={****}; MultipleActiveResultSets =False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;";
            using (var connection = new SqlConnection(SqlConnection))
            {
                string column = string.Empty;
                if (User?.Identity?.IsAuthenticated ?? false)
                    column = "UserAuthenticatedId";
                else
                    column = "UserId";

                //TODO: remove this code, just for testing
                //userId = "nrDJJrO7R9+wXSYKT8y6Sz";

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
