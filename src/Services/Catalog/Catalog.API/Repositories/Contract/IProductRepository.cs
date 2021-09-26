using Catalog.API.Entities;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Catalog.API.Repositories
{
    public interface IProductRepository
    {
        public Task AddAsyncSeed(CancellationToken cancellationToken);
        public Task<IEnumerable<Product>> GetProducts(CancellationToken cancellationToken);
        public Task<Product> GetProduct(string Id, CancellationToken cancellationToken);
        public Task<IEnumerable<Product>> GetProductbyName(string Name, CancellationToken cancellationToken);
        public Task<IEnumerable<Product>> GetProductbyCategory(string Category, CancellationToken cancellationToken);
        public Task<Product> CreateProduct(Product product, CancellationToken cancellationToken);
        public Task<bool> UpdateProduct(Product product, CancellationToken cancellationToken);
        public Task<Product> DeleteProduct(string id, CancellationToken cancellationToken);


    }
}
