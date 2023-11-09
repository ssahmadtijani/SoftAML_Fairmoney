using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace SoftAML_UpperLinkAPI.Models
{
    public class DapperContext
    {
        private readonly IDbConnection _connection;
        public DapperContext(string connectionString)
        {
            _connection = new SqlConnection(connectionString);
        }
        public IDbConnection Connection
        {
            get
            {
                //_connection.Open();
                return _connection;
            }
        }
    }
}