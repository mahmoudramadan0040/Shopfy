using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Shopfy.Models;
using Shopfy.Models.Interfaces;
using Shopfy.ViewModels.Dtos;

namespace Shopfy.Controllers
{
    [Route("api/products")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IProductRepository _productRepository;
        private readonly ILogger<ProductController> _logger;
        private readonly IMapper _mapper;
        public ProductController(
            ILogger<ProductController> logger,
            IProductRepository productRepository,
            IMapper mapper)
        {
            _logger = logger;
            _mapper = mapper;
            _productRepository = productRepository;
        }
        #region Get all Product 
        #endregion
        [HttpGet]
        public IActionResult GetAllProducts()
        {
            try
            {
                var allProducts = _productRepository.GetAllProduct();
                if (allProducts.Count() == 0)
                {
                    return NotFound();
                }
                else
                {
                    return Ok(allProducts);
                }
            }
            catch(Exception ex)
            {
                _logger.LogError("Not found any Product : {ex}", ex);
                return StatusCode(500, "Internal server error");
            }
           
        }
        
        [HttpGet("product/{productId}")]
        public IActionResult GetProduct(Guid productId)
        {
            try
            {
                var product = _productRepository.GetProductById(productId);
                if(product != null)
                {
                    return Ok(product);
                }
                else
                {
                    return NotFound();
                }
            }catch(Exception ex)
            {
                _logger.LogError("Not found any Product : {ex}", ex);
                return StatusCode(500, "Internal server error");
            }
        }
        [HttpPost]
        public IActionResult CreateProduct(ProductDto product)
        {
            try
            {
                if(product is null)
                {
                    _logger.LogError("product object sent from client is null ");
                    return BadRequest("product object is null !");
                }

                if (!ModelState.IsValid)
                {
                    _logger.LogError("Invalid object sent from client ! ");
                    return BadRequest("Invalid product object !");
                }

                var ProductMap = _mapper.Map<Product>(product);
                var result = _productRepository.CreateProduct(ProductMap);
                return Ok(result);
            } catch (Exception ex)
            {
                _logger.LogError("Not found any Product : {ex}", ex);
                return StatusCode(500, "Internal server error");
            }
        }

        [HttpDelete("product/{productId}")]
        public IActionResult DeleteProduct(Guid productId)
        {
            try
            {
                var product = _productRepository.GetProductById(productId);
                if(product is null)
                {
                    _logger.LogError($"product with id : {productId} , hasn't been found in db !");
                    return NotFound($"product with id : {productId} , hasn't been found in db !");
                }
                _productRepository.DeleteProduct(product);
                return Accepted("product is deleted successfully");
            }
            catch (Exception ex)
            {
                _logger.LogError("Can't delete product : {ex}", ex);
                return StatusCode(500, "Internal server error");
            }
        }

        [HttpPut("{productId}")]
        public IActionResult UpdateProduct(Guid productId,ProductDto product)
        {
            try
            {
                if(product is null)
                {
                    _logger.LogError("Invalid object sent from client ! ");
                    return BadRequest("Invalid product object !");
                }
                if (!ModelState.IsValid)
                {
                    _logger.LogError("Invalid object sent from client ! ");
                    return BadRequest("Invalid product object !");
                }
                var _product = _productRepository.GetProductById(productId);
                if(_product is null)
                {
                    _logger.LogError($"product with id : {productId} , hasn't been found in db !");
                    return NotFound($"product with id : {productId} , hasn't been found in db !");
                }
                
                var ProductMap = _mapper.Map<Product>(product);
                _productRepository.UpdateProduct(ProductMap);
                return Accepted(product);

            }
            catch (Exception ex)
            {
                _logger.LogError("Can't update product : {ex}", ex);
                return StatusCode(500, "Internal server error");
            }
        }
    }

}
