using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectUtilities.DataAccess
{
    public class DataConnections: IDataConnections
    {
        private readonly IConfiguration _configuration;
        private String Conn;

        public DataConnections(IConfiguration Configuration)
        {
            _configuration = Configuration;
        }

        public string GetConnectionString(String Con_KeyName)
        {
            Conn = _configuration.GetConnectionString(Con_KeyName);
            return Conn ?? string.Empty;
        }

        public SqlConnection OpenConnection(string connectionString)
        {
            var sqlConnection = new SqlConnection(connectionString);
            sqlConnection.Open();
            return sqlConnection;
        }


        public void CloseConnection(SqlConnection connection)
        {
            connection?.Close();
        }
    }
}
