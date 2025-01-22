using Microsoft.Data.SqlClient;
using ProjectUtilities.Logger;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectUtilities.DataAccess
{
    public class SQLDataAccess : ISQLDataAccess
    {
        private readonly ICustomLogger _logger;
        private readonly IDataConnections _dataConnections;
        private readonly String _connectionkey;

        public SQLDataAccess(IDataConnections dataConnections, String ConnectionKey, ICustomLogger logger)
        {
            _dataConnections = dataConnections;
            _connectionkey = ConnectionKey;
            _logger = logger;

        }

        public void ExecuteQuery(string query)
        {
            try
            {
                var connectionString = _dataConnections.GetConnectionString(_connectionkey);

                using (var connection = _dataConnections.OpenConnection(connectionString))
                {
                    SqlCommand command = new SqlCommand(query, connection);
                    command.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {

            }

        }

        public void ExecuteStoredProcedure(String storedProcedureName)
        {
            var connectionString = _dataConnections.GetConnectionString(_connectionkey);

            try
            {
                using (var connection = _dataConnections.OpenConnection(connectionString))
                {

                    using (var command = new SqlCommand(storedProcedureName, connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;

                        // Add parameters if needed, e.g.:
                        // command.Parameters.AddWithValue("@ParamName", value);

                        command.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception ex)
            {

            }
        }

        public DataTable return_dt(String storedProcedureName)
        {
            DataTable dt = new DataTable();

            var connectionString = _dataConnections.GetConnectionString(_connectionkey);

            using (SqlConnection objSqlConnection = _dataConnections.OpenConnection(connectionString))
            {
                SqlCommand command = new SqlCommand(storedProcedureName, objSqlConnection);
                command.CommandType = CommandType.StoredProcedure;
                using (SqlDataAdapter DataAdapter = new SqlDataAdapter())
                {
                    try
                    {
                        DataAdapter.SelectCommand = command;
                        DataAdapter.Fill(dt);
                    }
                    catch (Exception ex)
                    {

                    }
                    finally
                    {
                        if (objSqlConnection.State == ConnectionState.Open)
                        {
                            _dataConnections.CloseConnection(objSqlConnection);
                            objSqlConnection.Close();
                            command.Dispose();
                        }

                    }
                }
            }
            return dt;
        }

        public DataSet return_ds(String storedProcedureName)
        {
            DataSet ds = new DataSet();

            var connectionString = _dataConnections.GetConnectionString(_connectionkey);

            using (SqlConnection objSqlConnection = _dataConnections.OpenConnection(connectionString))
            {
                SqlCommand command = new SqlCommand(storedProcedureName, objSqlConnection);
                command.CommandType = CommandType.StoredProcedure;
                using (SqlDataAdapter DataAdapter = new SqlDataAdapter())
                {
                    try
                    {
                        DataAdapter.SelectCommand = command;
                        DataAdapter.Fill(ds);
                    }
                    catch (Exception ex)
                    {

                    }
                    finally
                    {
                        if (objSqlConnection.State == ConnectionState.Open)
                        {
                            _dataConnections.CloseConnection(objSqlConnection);
                            objSqlConnection.Close();
                            command.Dispose();
                        }

                    }
                }
            }
            return ds;
        }
    }
}
