using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Discount.Grpc.Configuration
{
    public class DatabaseConfiguration
    {
        private readonly IConfiguration _configuration;

        public DatabaseConfiguration(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        
        public string PostGresConnectionString()
        {
            var host = _configuration.GetValue<string>("db_host") ?? "localhost";
            var port = _configuration.GetValue<string>("POSTGRES_PORT") ?? "5432";
            var usersDataBase = _configuration.GetValue<string>("POSTGRES_DB") ?? _configuration.GetConnectionString("POSTGRES_DB");
            var userid = _configuration.GetValue<string>("POSTGRES_USER") ?? _configuration.GetConnectionString("POSTGRES_USER");
            var password = _configuration.GetValue<string>("POSTGRES_PASSWORD") ?? _configuration.GetConnectionString("POSTGRES_PASSWORD");
            var connString = $"Server={host};Port={port};Database={usersDataBase};User Id={userid};Password={password}";
          
            return connString;
        }
    }
}
