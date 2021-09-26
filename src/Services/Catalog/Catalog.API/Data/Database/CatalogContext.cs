using Catalog.API.Configuration;
using Catalog.API.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Data;
using System.Threading;
using System.Threading.Tasks;

namespace Catalog.API.Data.Database
{
    public class CatalogContext : ICatalogContext
    {
        private readonly IMongoDatabaseConfiguration _mongoDatabaseConfiguration;
        private readonly IMongoClient _mongoClient;
        private readonly ILogger<CatalogContext> _logger;

        IMongoCollection<Product> Products { get; }
        public CatalogContext(IMongoDatabaseConfiguration mongoDatabaseConfiguration, IMongoClient mongoClient, ILogger<CatalogContext> logger) 
        {
            
            _mongoDatabaseConfiguration = mongoDatabaseConfiguration;
            _mongoClient = mongoClient;
            _logger = logger;

            var database = _mongoClient.GetDatabase(_mongoDatabaseConfiguration.ToDatabaseNameString());
            Products = database.GetCollection<Product>(_mongoDatabaseConfiguration.ToCollectionNameString());
            
        }
        public async Task saveManyChanges(IEnumerable<Product> ProductList, CancellationToken cancellationToken)
        {
            Console.WriteLine("Connection String = " + _mongoDatabaseConfiguration.ToDatabaseNameString());
            await Products.InsertManyAsync(ProductList);
        }

        public IMongoCollection<Product> GetMongoDbCollection()
        {
            return Products;
        }
        public async Task<IEnumerable<Product>> GetProducts(CancellationToken cancellationToken)
        {
            return await Products.Find(p => true).ToListAsync();
            
        }
        public async Task<Product> GetProduct(string Id, CancellationToken cancellationToken)
        {
            return await Products.Find(p => p.Id == Id).FirstOrDefaultAsync();
        }

        public async Task<IEnumerable<Product>> GetProductbyName(string Name, CancellationToken cancellationToken)
        {
            FilterDefinition<Product> filter = Builders<Product>.Filter.Eq(p => p.Name, Name);
            return await Products.Find(filter).ToListAsync();
            
        }

        public async Task<IEnumerable<Product>> GetProductbyCategory(string Category, CancellationToken cancellationToken)
        {
            FilterDefinition<Product> filter = Builders<Product>.Filter.Eq(p => p.Category, Category);
            return await Products.Find(filter).ToListAsync();
        }

        public async Task<Product> CreateProduct(Product product, CancellationToken cancellationToken = default)
        {
            if (product == null)
            {
                throw new ArgumentNullException(nameof(product));
            }
            await Products.InsertOneAsync(product);

            return product;
        }

        public async Task<bool> UpdateProduct(Product product, CancellationToken cancellationToken = default)
        {
            if (product == null)
            {
                throw new ArgumentNullException(nameof(product));
            }
            var updateResult = await Products.ReplaceOneAsync(filter: h => h.Id == product.Id, replacement: product);

            return updateResult.IsAcknowledged && updateResult.ModifiedCount > 0;


        }

        public async Task<bool> DeleteProduct(string Id, CancellationToken cancellationToken = default)
        {
            if (Id == null)
            {
                throw new ArgumentNullException(nameof(Id));
            }
            FilterDefinition<Product> filter = Builders<Product>.Filter.Eq(p => p.Id, Id);

            DeleteResult deleteResult = await Products.DeleteOneAsync(filter);

            return deleteResult.IsAcknowledged && deleteResult.DeletedCount > 0;
            
        }
    }
}
