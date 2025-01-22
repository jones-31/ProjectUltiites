using Dapper;
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
    public class DapperDataAccess: IDapperDataAccess
    {
        private readonly String _connectionkey;
        private readonly IDataConnections _dataconnection;
        private readonly ICustomLogger _logger;

        public DapperDataAccess(IDataConnections dataconnection, String connectionkey, ICustomLogger logger)
        {
            _dataconnection = dataconnection;
            _connectionkey = connectionkey;
            _logger = logger;
        }

        public DataTable CreateErrorDataTable(string statusCode, string message)
        {
            DataTable errorTable = new DataTable();
            errorTable.Columns.Add("Code", typeof(string));
            errorTable.Columns.Add("Mesg", typeof(string));

            DataRow errorRow = errorTable.NewRow();
            errorRow["Code"] = statusCode;
            errorRow["Mesg"] = message;

            errorTable.Rows.Add(errorRow);
            return errorTable;
        }

        public int Execute(String storedProcedureName, DynamicParameters parameters = null)
        {
            int rowsAffected = 0;

            try
            {
                var connectionString = _dataconnection.GetConnectionString(_connectionkey);
                using (var connection = _dataconnection.OpenConnection(connectionString))
                {
                    var reader = connection.Execute(storedProcedureName, parameters, commandType: CommandType.StoredProcedure);
                }
            }
            catch (Exception ex)
            {

            }

            return rowsAffected;
        }

        public DataTable Return_dt(String storedProcedureName, DynamicParameters parameters = null)
        {
            DataTable dataTable = new DataTable();
            try
            {
                var connectionString = _dataconnection.GetConnectionString(_connectionkey);
                using (var connection = _dataconnection.OpenConnection(connectionString))
                {
                    using (var reader = connection.ExecuteReader(storedProcedureName, parameters, commandType: CommandType.StoredProcedure))
                    {
                        dataTable.Load(reader);
                        return dataTable;
                    }
                }
            }
            catch (Exception ex)
            {
                dataTable = CreateErrorDataTable("404", ex.Message);
            }
            return dataTable;
        }

        public DataSet GetDataSet(string storedProcedureName, DynamicParameters parameters = null)
        {
            DataSet dataSet = new DataSet();

            try
            {
                var connectionString = _dataconnection.GetConnectionString(_connectionkey);
                using (var connection = _dataconnection.OpenConnection(connectionString))
                {
                    using (var adapter = new SqlDataAdapter())
                    {
                        var command = new SqlCommand(storedProcedureName, connection)
                        {
                            CommandType = CommandType.StoredProcedure
                        };

                        if (parameters != null)
                        {
                            foreach (var param in parameters.ParameterNames)
                            {
                                command.Parameters.AddWithValue(param, parameters.Get<object>(param));
                            }
                        }

                        adapter.SelectCommand = command;
                        adapter.Fill(dataSet);
                    }
                }
            }
            catch (SqlException sx)
            {
                var errorTable = CreateErrorDataTable("400", sx.Message);
                dataSet.Tables.Add(errorTable);
            }
            catch (Exception ex)
            {
                var errorTable = CreateErrorDataTable("400", ex.Message);
                dataSet.Tables.Add(errorTable);
            }

            return dataSet;
        }
    }
}
