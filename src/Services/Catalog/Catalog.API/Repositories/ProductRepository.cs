using Catalog.API.Data.Contract;
using Catalog.API.Entities;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Catalog.API.Repositories
{
    public class ProductRepository : IProductRepository
    {
        private readonly IProductProvider _productProvider;

        public ProductRepository(IProductProvider productProvider)
        {
            _productProvider = productProvider;
        }
        public async Task AddAsyncSeed(CancellationToken cancellationToken)
        {
            await _productProvider.AddAsyncSeed();
        }

        public async Task<Product> CreateProduct(Product product, CancellationToken cancellationToken = default)
        {
            if (product == null)
            {
                throw new ArgumentNullException(nameof(product));
            }
            var result = await _productProvider.CreateProduct(product, cancellationToken);

            return result;
        }

        

        public Task<Product> DeleteProduct(string id, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public async Task<Product> GetProduct(string Id, CancellationToken cancellationToken)
        {
            return await _productProvider.GetProduct(Id);
        }

        public async Task<IEnumerable<Product>> GetProductbyCategory(string Category, CancellationToken cancellationToken)
        {
            if (Category == null)
            {
                throw new ArgumentNullException(nameof(Category));
            }
            return await _productProvider.GetProductbyCategory(Category, cancellationToken);
        }

        public async Task<IEnumerable<Product>> GetProductbyName(string Name, CancellationToken cancellationToken)
        {
            if (Name == null)
            {
                throw new ArgumentNullException(nameof(Name));
            }
            return await _productProvider.GetProductbyName(Name);
        }

        public async Task<IEnumerable<Product>> GetProducts(CancellationToken cancellationToken = default)
        {
            return await _productProvider.GetProducts();
        }

        public async Task<bool> UpdateProduct(Product product, CancellationToken cancellationToken = default)
        {

            if (product == null)
            {
                throw new ArgumentNullException(nameof(product));
            }
            return await _productProvider.UpdateProduct(product);
            
        }
        public Task<Product> DeleteProduct(string id)
        {
            throw new NotImplementedException();
        }
    }
}
