using Microsoft.Practices.EnterpriseLibrary.Data;
using System.Data;
using System.Data.SqlClient;

namespace JewelryManagementSystem.DAL
{
    public class DALHelper
    {
        #region DataTable Connection String

        public static string myConnectionString = new ConfigurationBuilder().AddJsonFile("appsettings.Json").Build().GetConnectionString("myConnectionStrings");

        #endregion

        #region Execute Stored Procedure and return DataTable
        public static DataTable GetDataTable(string procedureName, params SqlParameter[] parameters)
        {
            using (SqlConnection conn = new SqlConnection(myConnectionString))
            {
                using (SqlCommand cmd = new SqlCommand(procedureName, conn))
                {
                    try
                    {
                        cmd.CommandType = CommandType.StoredProcedure;

                        // Add parameters if any
                        if (parameters != null && parameters.Length > 0)
                        {
                            cmd.Parameters.AddRange(parameters);
                        }

                        // Create DataTable to hold the results
                        DataTable dt = new DataTable();

                        // Create adapter and fill DataTable
                        using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                        {
                            conn.Open();
                            da.Fill(dt);
                        }

                        return dt;
                    }
                    catch (Exception ex)
                    {
                        // Log error here
                        throw new Exception($"Error executing stored procedure {procedureName}: {ex.Message}", ex);
                    }
                }
            }
        }
        #endregion

        public static DataSet GetDataSet(string procedureName, params SqlParameter[] parameters)
        {
            using (SqlConnection conn = new SqlConnection(myConnectionString))
            {
                using (SqlCommand cmd = new SqlCommand(procedureName, conn))
                {
                    try
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        // Add parameters if any
                        if (parameters != null && parameters.Length > 0)
                        {
                            cmd.Parameters.AddRange(parameters);
                        }

                        // Create DataSet to hold the results
                        DataSet ds = new DataSet();

                        // Create adapter and fill DataSet
                        using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                        {
                            conn.Open();
                            da.Fill(ds);
                        }
                        return ds;
                    }
                    catch (Exception ex)
                    {
                        // Log error here
                        throw new Exception($"Error executing stored procedure {procedureName}: {ex.Message}", ex);
                    }
                }
            }
        }

        // Execute Stored Procedure and return scalar value
        public static T ExecuteScalar<T>(string procedureName, params SqlParameter[] parameters)
        {
            using (SqlConnection conn = new SqlConnection(myConnectionString))
            {
                using (SqlCommand cmd = new SqlCommand(procedureName, conn))
                {
                    try
                    {
                        cmd.CommandType = CommandType.StoredProcedure;

                        if (parameters != null && parameters.Length > 0)
                        {
                            cmd.Parameters.AddRange(parameters);
                        }

                        conn.Open();
                        object result = cmd.ExecuteScalar();
                        return (T)Convert.ChangeType(result, typeof(T));
                    }
                    catch (Exception ex)
                    {
                        throw new Exception($"Error executing stored procedure {procedureName}: {ex.Message}", ex);
                    }
                }
            }
        }

        // Execute Stored Procedure with no return value (for Insert, Update, Delete)
        public static int ExecuteNonQuery(string procedureName, params SqlParameter[] parameters)
        {
            using (SqlConnection conn = new SqlConnection(myConnectionString))
            {
                using (SqlCommand cmd = new SqlCommand(procedureName, conn))
                {
                    try
                    {
                        cmd.CommandType = CommandType.StoredProcedure;

                        if (parameters != null && parameters.Length > 0)
                        {
                            cmd.Parameters.AddRange(parameters);
                        }

                        conn.Open();
                        return cmd.ExecuteNonQuery();
                    }
                    catch (Exception ex)
                    {
                        throw new Exception($"Error executing stored procedure {procedureName}: {ex.Message}", ex);
                    }
                }
            }
        }

    }
}
