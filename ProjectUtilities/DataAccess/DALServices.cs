using ProjectUtilities.Logger;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectUtilities.DataAccess
{
    public class DALServices
    {
        public Func<string, ISQLDataAccess> SqlDataAccessFactory { get; }
        public Func<string, IDapperDataAccess> DapperDataAccessFactory { get; }

        public IDataConnections DataConnections { get; }
        public ICustomLogger Logger { get; }
        public IHashing Hashing { get; }

        public DALServices(Func<string, ISQLDataAccess> sqlDataAccessFactory,
                           Func<string, IDapperDataAccess> dapperDataAccessFactory,
                           IDataConnections dataConnections,
                           ICustomLogger logger,
                           IHashing hashing)
        {
            SqlDataAccessFactory = sqlDataAccessFactory;
            DapperDataAccessFactory = dapperDataAccessFactory;
            DataConnections = dataConnections;
            Logger = logger;
            Hashing = hashing;
        }
    }
}
