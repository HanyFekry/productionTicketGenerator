using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DBLayer
{
    public class ManufacturingConfiguration:DbConfiguration
    {
        public ManufacturingConfiguration ()
        {
            string _connectionString = "Data Source=10.1.18.10; Integrated Security=False; uid=sa;password=P@ssw0rd1; MultipleActiveResultSets=True";
            //"Data Source=uicit;Initial Catalog=plan_be;Integrated Security=False; uid=sa;password=P@ssw0rd1;"
            string _provider = "System.Data.SqlClient";
            SetDefaultConnectionFactory(new SqlConnectionFactory(_connectionString));
            this.SetProviderServices(_provider, System.Data.Entity.SqlServer.SqlProviderServices.Instance);
            this.SetDatabaseInitializer<ManufacturingContext>(new NullDatabaseInitializer<ManufacturingContext>());
        }
    }
}
