using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Shopfy.Models;
using Shopfy.Models.Interfaces;
using Shopfy.Utils;
using Shopfy.ViewModels.Dtos;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.Xml;

namespace Shopfy.Controllers
{
    [Route("api/products")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IProductRepository _productRepository;
        private readonly IStorageRepository _storageRepository;
        private readonly IProductImageRepository _productImageRepository;
        private readonly ILogger<ProductController> _logger;
        private readonly IMapper _mapper;
        public ProductController(
            ILogger<ProductController> logger,
            IProductRepository productRepository,
            IStorageRepository storageRepository,
            IProductImageRepository productImageRepository,
        IMapper mapper)
        {
            _logger = logger;
            _mapper = mapper;
            _productRepository = productRepository;
            _storageRepository = storageRepository;
            _productImageRepository = productImageRepository;
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
        public async Task<IActionResult> CreateProduct([FromForm] ProductDto product)
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
                // check the thumbnailfile is image or not 
                if (!product.ProductThumbnail.IsImage())
                    return BadRequest("product Thumbnail is not image !");
                // check the number of image uploaded 
                if (!(product.ProductImages.Length < 5))
                    return BadRequest("Too image uploaded for product ! , maximum image is 5 images !");
                // check the files is images or not
                foreach (var img in product.ProductImages)
                {
                    if (!img.IsImage())
                        return BadRequest($"this file is not image ${img.FileName}");
                }

                var ProductMap = _mapper.Map<Product>(product);
                
                //var result = _productRepository.CreateProduct(ProductMap);
                // upload thumbnail image to storage 
                var ProductThumbUrl = await _storageRepository.AddImage(product.ProductThumbnail, "ProductsThumbnails");
                // store the access url of image 
                ProductMap.ProductThumbnail = ProductThumbUrl;



                List<ProductImage> productImages = new List<ProductImage>();
                ProductMap.ProductImages = new List<ProductImage>();
                foreach (var img in product.ProductImages)
                {
                    var url= await _storageRepository.AddImage(img, "ProductsImages");
                    _logger.LogInformation(url.ToString());
                    ProductImage image = new ProductImage
                    {
                        
                        ImageUrl = url
                    };
                    _logger.LogInformation(image.ImageUrl.ToString());
                   
                    ProductMap.ProductImages.Add(image);



                }
                 var Createdproduct = _productRepository.CreateProduct(ProductMap);
                       
                return Ok(Createdproduct);
            } catch (Exception ex)
            {
                _logger.LogError("Can not create product : {ex}", ex);
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
