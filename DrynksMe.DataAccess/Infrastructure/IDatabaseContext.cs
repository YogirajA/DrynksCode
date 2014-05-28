using System;
using System.Data;

namespace DrynksMe.DataAccess.Infrastructure
{
    public interface IDatabaseContext
    {
        IDbConnection Connection { get; }
    }
}