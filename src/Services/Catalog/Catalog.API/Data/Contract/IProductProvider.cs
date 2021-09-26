using Catalog.API.Entities;
using MongoDB.Driver;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Catalog.API.Data.Contract
{
    public interface IProductProvider
    {
        public Task AddAsyncSeed(CancellationToken cancellationToken = default);
        public Task<IEnumerable<Product>> GetProducts(CancellationToken cancellationToken = default);
        public Task<Product> CreateProduct(Product product, CancellationToken cancellationToken = default);
        public Task<bool> UpdateProduct(Product product, CancellationToken cancellationToken = default);
        public Task<bool> DeleteProduct(string id, CancellationToken cancellationToken = default);
        public Task<Product> GetProduct(string Id, CancellationToken cancellationToken = default);
        public Task<IEnumerable<Product>> GetProductbyName(string Name, CancellationToken cancellationToken = default);
        public Task<IEnumerable<Product>> GetProductbyCategory(string Category, CancellationToken cancellationToken);
    }
}
