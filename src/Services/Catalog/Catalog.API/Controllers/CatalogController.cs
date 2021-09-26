using Catalog.API.Entities;
using Catalog.API.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading;
using System.Threading.Tasks;

namespace Catalog.API.Controller
{
    //controller = Catalog
    [ApiController]
    [Route("api/v1/[controller]")]
    public class CatalogController : ControllerBase
    {
        private readonly IProductRepository _productRepository;
        private readonly ILogger<CatalogController> _logger;

        public CatalogController(IProductRepository productRepository, ILogger<CatalogController> logger)
        {
            _productRepository = productRepository ?? throw new ArgumentNullException(nameof(productRepository));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }
        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<Product>), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<IEnumerable<Product>>> GetProducts(CancellationToken cancellationToken)
            
        {
            var products = await _productRepository.GetProducts(cancellationToken);


            return Ok(products);
        }
        [HttpGet("{id:length(24)}", Name = "GetProduct")]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType(typeof(Product), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<Product>> GetProductById(string id, CancellationToken cancellationToken = default)
        {
            var product = await _productRepository.GetProduct(id, cancellationToken);
            if (product == null)
            {
                _logger.LogError($"Product with id: {id}, not found.");
                return NotFound();
            }
            return Ok(product);
        }
        [HttpGet]

        //action = GetProductByCategory
        [Route("[action]/{category}", Name = "GetProductByCategory")]
        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<Product>), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<IEnumerable<Product>>> GetProductByCategory(string category, CancellationToken cancellationToken = default)
        {
            var products = await _productRepository.GetProductbyCategory(category, cancellationToken);
            return Ok(products);
        }
        
        [Route("[action]/seed")]
        [HttpPost]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        public async Task<IActionResult> AddAsyncSeed(CancellationToken cancellationToken = default)
        {
            await _productRepository.AddAsyncSeed(cancellationToken);
            return Ok();
        }
        
        [HttpPost]
        [ProducesResponseType(typeof(Product), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<Product>> CreateProduct([FromBody] Product product, CancellationToken cancellationToken = default)
        {
            await _productRepository.CreateProduct(product, cancellationToken);

            return CreatedAtRoute("GetProduct", new { id = product.Id }, product);
        }
        [HttpPut]
        [ProducesResponseType(typeof(Product), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> UpdateProduct([FromBody] Product product, CancellationToken cancellationToken = default)
        {
            return Ok(await _productRepository.UpdateProduct(product, cancellationToken));
        }
        //IActionResult if there is no specific respunse you want returned
        [HttpDelete("{id:length(24)}", Name = "DeleteProduct")]
        [ProducesResponseType(typeof(Product), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> DeleteProductById(string id, CancellationToken cancellationToken = default)
        {
            return Ok(await _productRepository.DeleteProduct(id, cancellationToken));
        }
    }
}
