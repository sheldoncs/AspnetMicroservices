using Catalog.API.Entities;
using MongoDB.Driver;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Catalog.API.Data.Database
{
    public interface ICatalogContext
    {
     
        public Task saveManyChanges(IEnumerable<Product> ProductList, CancellationToken cancellationToken = default);
        public IMongoCollection<Product> GetMongoDbCollection();
        public Task<IEnumerable<Product>> GetProducts(CancellationToken cancellationToken);
        public Task<Product> GetProduct(string Id, CancellationToken cancellationToken = default);
        public Task<IEnumerable<Product>> GetProductbyName(string Name, CancellationToken cancellationToken = default);
        public Task<IEnumerable<Product>> GetProductbyCategory(string Category, CancellationToken cancellationToken = default);
        public Task<Product> CreateProduct(Product product, CancellationToken cancellationToken = default);
        public Task<bool> DeleteProduct(string Id, CancellationToken cancellationToken = default);
        public Task<bool> UpdateProduct(Product product, CancellationToken cancellationToken = default);

    }
}
