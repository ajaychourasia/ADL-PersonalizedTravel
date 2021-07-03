using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System;
using System.Data.SqlClient;

namespace ADL.PersonalizedTravel.Utilities
{
    public static class DbQuery
    {
       
        public static string GetDbResultSet(string userId , bool isAuthenticated)
        {
            string clusterId = "0";
            string sqlConnection = Startup.StaticConfig.GetConnectionString("AzureDbConnection");
           
            using (var connection = new SqlConnection(sqlConnection))
            {
                string column = string.Empty;
                if (isAuthenticated)
                    column = "UserAuthenticatedId";
                else
                    column = "UserId";

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
                    //[NOTE: Throwing an Exception is being ignored to excute default workflow for Demo purpose]
                    //[Known Exception Type : Client IP address needs to be whitelisted in Azure portal for Azure SQL DB]
                    //throw;
                    return clusterId == "0" ? "1" : clusterId;
                }

            }
        }
    }
}
