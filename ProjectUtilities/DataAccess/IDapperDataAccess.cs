using Dapper;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectUtilities.DataAccess
{
    public interface IDapperDataAccess
    {
        DataTable CreateErrorDataTable(string statusCode, string message);

        /// <summary>
        /// Executes the storedprocedure and returns the number of rows affected.
        /// </summary>
        /// <param name="storedProcedureName"></param>
        /// <param name="parameters"></param>

        /// <param name="storedProcedureName"></param>
        /// <param name="parameters"></param>
        /// <returns> An integer value that represents the number of rows affected</returns>
        int Execute(String storedProcedureName, DynamicParameters parameters = null);

        /// <summary>
        /// Returns a Datatable
        /// </summary>
        /// <param name="storedProcedureName"></param>
        /// <param name="parameters"></param>
        /// <returns>
        /// A <see cref="DataTable"/> containing the results of the stored procedure execution.
        /// </returns>
        /// <exception cref="SqlException">
        /// Thrown when there is an issue executing the stored procedure.
        /// </exception>
        /// <exception cref="InvalidOperationException">
        /// Thrown when the connection state is invalid for executing commands.
        /// </exception>
        DataTable Return_dt(String storedProcedureName, DynamicParameters parameters = null);

        /// <summary>
        /// Returns a Dataset 
        /// </summary>
        /// <param name="storedProcedureName"></param>
        /// <param name="parameters"></param>
        /// <returns>
        /// A <see cref="DataSet"/> containing the results of the stored procedure execution.
        /// </returns>
        /// <exception cref = "InvalidOperationException">
        /// Thrown when the connection state is invalid for executing commands.
        /// </exception>
        DataSet GetDataSet(string storedProcedureName, DynamicParameters parameters = null);
    }
}
