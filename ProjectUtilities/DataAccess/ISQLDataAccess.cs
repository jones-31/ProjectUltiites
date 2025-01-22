using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectUtilities.DataAccess
{
    public interface ISQLDataAccess
    {
        void ExecuteQuery(string query);
        void ExecuteStoredProcedure(String storedProcedureName);
        /// <summary>
        /// Returns a Datatable
        /// </summary>
        /// <param name="storedProcedureName"></param>
        /// <returns>
        /// A <see cref="DataTable"/> containing the results of the stored procedure execution.
        /// </returns>
        /// <exception cref="SqlException">
        /// Thrown when there is an issue executing the stored procedure.
        /// </exception>
        /// <exception cref="InvalidOperationException">
        /// Thrown when the connection state is invalid for executing commands.
        /// </exception>
        DataTable return_dt(String storedProcedureName);
        /// <summary>
        /// Returns a Dataset 
        /// </summary>
        /// <param name="storedProcedureName"></param>
        /// <returns>
        /// A <see cref="DataSet"/> containing the results of the stored procedure execution.
        /// </returns>
        /// <exception cref = "InvalidOperationException">
        /// Thrown when the connection state is invalid for executing commands.
        /// </exception>
        DataSet return_ds(String storedProcedureName);
    }
}
