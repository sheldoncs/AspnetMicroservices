using Catalog.API.Data.Contract;
using Catalog.API.Data.Database;
using Catalog.API.Entities;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Catalog.API.Data
{
    public class ProductProvider : IProductProvider
    {
        private readonly ICatalogContext _catalogContext;
        private IEnumerable<Product> ProductList { get; set; }
        public ProductProvider(ICatalogContext catalogContext)
        {
            _catalogContext = catalogContext;
        }

       
        public async Task AddAsyncSeed(CancellationToken cancellationToken)
        {
            
            bool existProduct = _catalogContext.GetMongoDbCollection().Find(p => true).Any();
            
            if (!existProduct)
            {

                await _catalogContext.saveManyChanges(GetPreconfiguredProducts(), cancellationToken);
            }

           
        }
        public async Task<IEnumerable<Product>> GetProducts(CancellationToken cancellationToken = default)
        {
            return await _catalogContext.GetProducts(cancellationToken);
        }
        public async Task<Product> GetProduct(string Id, CancellationToken cancellationToken = default)
        {
            return await _catalogContext.GetProduct(Id, cancellationToken);
        }
        public async Task<IEnumerable<Product>> GetProductbyName(string Name, CancellationToken cancellationToken = default)
        {
            return await _catalogContext.GetProductbyName(Name, cancellationToken);
            
        }
        public async Task<Product> CreateProduct(Product product, CancellationToken cancellationToken = default)
        {
            if (product == null)
            {
                throw new ArgumentNullException(nameof(product));
            }
            return await _catalogContext.CreateProduct(product, cancellationToken);
        }
        public async Task<bool> UpdateProduct(Product product, CancellationToken cancellationToken = default)
        {
            if (product == null)
            {
                throw new ArgumentNullException(nameof(product));
            }

            return await _catalogContext.UpdateProduct(product, cancellationToken);
        }
        public async Task<bool> DeleteProduct(string id, CancellationToken cancellationToken = default)
        {
            if (id == null)
            {
                throw new ArgumentNullException(nameof(id));
            }
            return await _catalogContext.DeleteProduct(id, cancellationToken);

        }
        public async Task<IEnumerable<Product>> GetProductbyCategory(string Category, CancellationToken cancellationToken)
        {
            if (Category == null)
            {
                throw new ArgumentNullException(nameof(Category));
            }
            return await _catalogContext.GetProductbyCategory(Category, cancellationToken);
        }
        private IEnumerable<Product> GetPreconfiguredProducts()
        {
            return new List<Product>()
            {
                new Product()
                {
                    Id = "602d2149e773f2a3990b47f5",
                    Name = "IPhone X",
                    Summary = "This phone is the company's biggest change to its flagship smartphone in years. It includes a borderless.",
                    Description = "Lorem ipsum dolor sit amet, consectetur adipisicing elit. Ut, tenetur natus doloremque laborum quos iste ipsum rerum obcaecati impedit odit illo dolorum ab tempora nihil dicta earum fugiat. Temporibus, voluptatibus. Lorem ipsum dolor sit amet, consectetur adipisicing elit. Ut, tenetur natus doloremque laborum quos iste ipsum rerum obcaecati impedit odit illo dolorum ab tempora nihil dicta earum fugiat. Temporibus, voluptatibus.",
                    ImageFile = "product-1.png",
                    Price = 950.00M,
                    Category = "Smart Phone"
                },
                new Product()
                {
                    Id = "602d2149e773f2a3990b47f6",
                    Name = "Samsung 10",
                    Summary = "This phone is the company's biggest change to its flagship smartphone in years. It includes a borderless.",
                    Description = "Lorem ipsum dolor sit amet, consectetur adipisicing elit. Ut, tenetur natus doloremque laborum quos iste ipsum rerum obcaecati impedit odit illo dolorum ab tempora nihil dicta earum fugiat. Temporibus, voluptatibus. Lorem ipsum dolor sit amet, consectetur adipisicing elit. Ut, tenetur natus doloremque laborum quos iste ipsum rerum obcaecati impedit odit illo dolorum ab tempora nihil dicta earum fugiat. Temporibus, voluptatibus.",
                    ImageFile = "product-2.png",
                    Price = 840.00M,
                    Category = "Smart Phone"
                },
                new Product()
                {
                    Id = "602d2149e773f2a3990b47f7",
                    Name = "Huawei Plus",
                    Summary = "This phone is the company's biggest change to its flagship smartphone in years. It includes a borderless.",
                    Description = "Lorem ipsum dolor sit amet, consectetur adipisicing elit. Ut, tenetur natus doloremque laborum quos iste ipsum rerum obcaecati impedit odit illo dolorum ab tempora nihil dicta earum fugiat. Temporibus, voluptatibus. Lorem ipsum dolor sit amet, consectetur adipisicing elit. Ut, tenetur natus doloremque laborum quos iste ipsum rerum obcaecati impedit odit illo dolorum ab tempora nihil dicta earum fugiat. Temporibus, voluptatibus.",
                    ImageFile = "product-3.png",
                    Price = 650.00M,
                    Category = "White Appliances"
                },
                new Product()
                {
                    Id = "602d2149e773f2a3990b47f8",
                    Name = "Xiaomi Mi 9",
                    Summary = "This phone is the company's biggest change to its flagship smartphone in years. It includes a borderless.",
                    Description = "Lorem ipsum dolor sit amet, consectetur adipisicing elit. Ut, tenetur natus doloremque laborum quos iste ipsum rerum obcaecati impedit odit illo dolorum ab tempora nihil dicta earum fugiat. Temporibus, voluptatibus. Lorem ipsum dolor sit amet, consectetur adipisicing elit. Ut, tenetur natus doloremque laborum quos iste ipsum rerum obcaecati impedit odit illo dolorum ab tempora nihil dicta earum fugiat. Temporibus, voluptatibus.",
                    ImageFile = "product-4.png",
                    Price = 470.00M,
                    Category = "White Appliances"
                },
                new Product()
                {
                    Id = "602d2149e773f2a3990b47f9",
                    Name = "HTC U11+ Plus",
                    Summary = "This phone is the company's biggest change to its flagship smartphone in years. It includes a borderless.",
                    Description = "Lorem ipsum dolor sit amet, consectetur adipisicing elit. Ut, tenetur natus doloremque laborum quos iste ipsum rerum obcaecati impedit odit illo dolorum ab tempora nihil dicta earum fugiat. Temporibus, voluptatibus. Lorem ipsum dolor sit amet, consectetur adipisicing elit. Ut, tenetur natus doloremque laborum quos iste ipsum rerum obcaecati impedit odit illo dolorum ab tempora nihil dicta earum fugiat. Temporibus, voluptatibus.",
                    ImageFile = "product-5.png",
                    Price = 380.00M,
                    Category = "Smart Phone"
                },
                new Product()
                {
                    Id = "602d2149e773f2a3990b47fa",
                    Name = "LG G7 ThinQ",
                    Summary = "This phone is the company's biggest change to its flagship smartphone in years. It includes a borderless.",
                    Description = "Lorem ipsum dolor sit amet, consectetur adipisicing elit. Ut, tenetur natus doloremque laborum quos iste ipsum rerum obcaecati impedit odit illo dolorum ab tempora nihil dicta earum fugiat. Temporibus, voluptatibus. Lorem ipsum dolor sit amet, consectetur adipisicing elit. Ut, tenetur natus doloremque laborum quos iste ipsum rerum obcaecati impedit odit illo dolorum ab tempora nihil dicta earum fugiat. Temporibus, voluptatibus.",
                    ImageFile = "product-6.png",
                    Price = 240.00M,
                    Category = "Home Kitchen"
                }
            };
        }

        
    }
}
