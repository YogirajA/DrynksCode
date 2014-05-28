using System.Collections.Generic;
using System.Data.SqlClient;
using Dapper;
using DrynksMe.DataAccess.Infrastructure;
using DrynksMe.DataAccess.Models;

namespace DrynksMe.Services
{
    public class BaseServices
    {
        protected readonly IDatabaseContext DatabaseContext;
       
        public BaseServices(IDatabaseContext databaseContext)
        {
            DatabaseContext = databaseContext;
        }

        public SqlConnection SqlConnection
        {
            get { return DatabaseContext.Connection as SqlConnection; }
        }

    }
}
