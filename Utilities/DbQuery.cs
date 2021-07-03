using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System;
using System.Data.SqlClient;

namespace ADL.PersonalizedTravel.Utilities
{
    public static class DbQuery
    {
        //We used Azure SQL DB (traveldemoclusters) to export out the users data with clusters (output of Azure ML pipeline after clustering algorithm)
        //Refer provided spreadsheet for sample data
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
                }
                catch (Exception ex)
                {
                    //[NOTE: Throwing an Exception is being ignored to execute default workflow for Demo purpose]
                    //[Known Exception Type : Client IP address needs to be whitelisted in Azure portal for Azure SQL DB]
                    //throw;
                }
                //Return default cluster in-case no Azure SQL DB configured (fallback option)
                return clusterId == "0" ? "1" : clusterId;
            }
        }
    }
}
