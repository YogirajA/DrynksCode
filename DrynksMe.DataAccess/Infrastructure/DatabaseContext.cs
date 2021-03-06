﻿using System.Data;
using System.Data.SqlClient;

namespace DrynksMe.DataAccess.Infrastructure
{

    public class DatabaseContext : IDatabaseContext
    {
        private readonly string _connectionString;
        private SqlConnection _connection;

        public DatabaseContext(string connectionString)
        {
            _connectionString = connectionString;
        }

        public IDbConnection Connection
        {
            get
            {
                if (_connection == null)
                    _connection = new SqlConnection(_connectionString);

                if (string.IsNullOrEmpty(_connection.ConnectionString))
                    _connection.ConnectionString = _connectionString;

                if (_connection.State != ConnectionState.Open)
                    _connection.Open();

                return _connection;
            }
        }
        //public IDbConnection ProfiledConnection
        //{
        //    get
        //    {
        //        if (_connection == null)
        //            _connection = new SqlConnection(_connectionString);

        //        if (string.IsNullOrEmpty(_connection.ConnectionString))
        //            _connection.ConnectionString = _connectionString;

        //        if (_connection.State != ConnectionState.Open)
        //            _connection.Open();

        //        return new ProfiledDbConnection(_connection, MiniProfiler.Current);
        //    }

        //}
    }
    
}
