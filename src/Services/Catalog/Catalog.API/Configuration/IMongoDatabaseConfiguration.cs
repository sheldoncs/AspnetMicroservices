using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Catalog.API.Configuration
{
    public interface IMongoDatabaseConfiguration
    {
        public String ToConnectionString();
        public String ToDatabaseNameString();
        public String ToCollectionNameString();
    }
}
