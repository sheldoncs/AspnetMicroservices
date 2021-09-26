using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


namespace Catalog.API.Configuration
{
    public class MongoDatabaseConfiguration : IMongoDatabaseConfiguration
    {
        private readonly string _connectionString;
        private readonly string _databaseName;
        private readonly string _collectionName;
      
        IConfiguration _configuration;
        public MongoDatabaseConfiguration(IConfiguration configuration)
        {
            _configuration = configuration;
            _connectionString = _configuration.GetValue<string>("DatabaseSettings_ConnectionString");
            _databaseName = _configuration.GetValue<string>("DatabaseSettings_DatabaseName");
            _collectionName = _configuration.GetValue<string>("DatabaseSettings_CollectionName");
        }
       public String ToConnectionString()
        {
            return _connectionString;
        }
        public String ToDatabaseNameString()
        {
            Console.WriteLine("Database Name String = " + _databaseName);
            return _databaseName;
        }
        public String ToCollectionNameString()
        {
            return _collectionName;
        }
    }
}
