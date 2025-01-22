using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectUtilities.DataAccess
{
    public interface IDataConnections
    {
        string GetConnectionString(String Con_KeyName);
        SqlConnection OpenConnection(string connectionString);
        void CloseConnection(SqlConnection connection);
    }
}
