using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Shopfy.Models;
using Shopfy.Models.Interfaces;
using Shopfy.Models.Repository;
using Shopfy.ViewModels.Dtos;

namespace Shopfy.Controllers
{
    [Route("api/subcategories")]
    [ApiController]
    public class SubCategoryController : ControllerBase
    {
        private readonly ISubCategoryRepository _subCategoryRepository;
        private readonly ILogger<SubCategoryController> _logger;
        private readonly IMapper _mapper;
        public SubCategoryController(
            ISubCategoryRepository subCategoryRepository,
            ILogger<SubCategoryController> logger,
            IMapper mapper
            )
        {
            _subCategoryRepository = subCategoryRepository;
            _logger = logger;
            _mapper = mapper;
        }
        #region ----- get all categories ----- 
        #endregion
        [HttpGet]
        public IActionResult GetAllSubCategories()
        {
            try
            {
                var allCategories = _subCategoryRepository.GetSubCategories();
                var SubCategoriesMap = _mapper.Map<IEnumerable<SubCategoriesDto>>(allCategories);
                if (SubCategoriesMap.Count() == 0)
                {
                    return NotFound();
                }
                else
                {
                    return Ok(SubCategoriesMap);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError("Not found any catgory : {ex}", ex);
                return StatusCode(500, "Internal server error");
            }
        }
        [HttpGet("subcategory/{subcategoryId}")]
        public IActionResult GetSubCategory(Guid Id)
        {
            try
            {
                var SubCategory = _subCategoryRepository.GetSubCategory(Id);
                var SubCategoryMap = _mapper.Map<SubCategoriesDto>(SubCategory);
                if (SubCategory == null)
                    return NotFound($"Category with id : {Id} , hasn't been found in db !");
                
                return Ok(SubCategoryMap);
                
            }
            catch (Exception ex)
            {
                _logger.LogError("Not found any catgory : {ex}", ex);
                return StatusCode(500, "Internal server error");
            }
        }
        [HttpGet("subcategory/{subcategoryId}/products")]
        public IActionResult GetAllProductsBySubCategory(Guid subcategoryId)
        {
            try
            {
                var products = _subCategoryRepository.GetAllProductsBySubCategoryId(subcategoryId);
                if (products == null)
                    return NotFound($"product with  subcategory id : {subcategoryId} , hasn't been found in db !");
                return Ok(products);

            }
            catch(Exception ex)
            {
                _logger.LogError("Not found any catgory : {ex}", ex);
                return StatusCode(500, "Internal server error");
            }
        }
        [HttpPost]
        [Route("subcategory",Name = "SubcategoryId")]
        public IActionResult CreateSubCategory(SubCategoryDto subCategory)
        {
            try
            {
                if(subCategory == null)
                {
                    _logger.LogError("subcategory object sent from client is null ");
                    return BadRequest("SubCategory object is null !");
                }
                if (!ModelState.IsValid)
                {
                    _logger.LogError("Invalid object sent from client ! ");
                    return BadRequest("Invalid Subategory object !");
                }
                var SubCategoryMap = _mapper.Map<SubCategory>(subCategory);
                var result = _subCategoryRepository.CreateSubCategory(SubCategoryMap);
                return CreatedAtRoute("SubcategoryId", new { SubcategoryId = result.Id }, SubCategoryMap);
            }
            catch (Exception ex)
            {
                _logger.LogError("Can't Create sub category : {ex}", ex);
                return StatusCode(500, "Internal server error");
            }
        }
        [HttpPut("{subcategoryId}")]
        public IActionResult UpdateSubcategory(Guid subcategoryId , SubCategoryDto subCategory)
        {
            try
            {
                if (subCategory is null)
                {
                    _logger.LogError("category object sent from client is null ");
                    return BadRequest("Category object is null !");
                }

                if (!ModelState.IsValid)
                {
                    _logger.LogError("Invalid object sent from client ! ");
                    return BadRequest("Invalid category object !");
                }
                var get_category = _subCategoryRepository.GetSubCategory(subcategoryId);

                if (get_category is null)
                {
                    _logger.LogError($"get_category with id: {subcategoryId}, hasn't been found in db.");
                    return NotFound();
                }
                var SubCategorMap = _mapper.Map<SubCategory>(subCategory);

                _subCategoryRepository.UpdateSubCategory(SubCategorMap);

                return Accepted(subCategory);
            }
            catch (Exception ex)
            {
                _logger.LogError("Can't update subcategory : {ex}", ex);
                return StatusCode(500, "Internal server error");
            }
        }

        [HttpDelete("subcategory/{subcategoryId}")]
        public IActionResult DeleteSubCategory(Guid Id)
        {
            try
            {
                var subCategory = _subCategoryRepository.GetSubCategory(Id);
                if (subCategory == null)
                    return NotFound($" delete SubCategory with id : {Id} , hasn't been found in db !");
                _subCategoryRepository.DeleteSubCategory(Id);
                return Accepted("subcategory is deleted successfully !");
            }catch(Exception ex)
            {
                _logger.LogError("Can't delete subcategory : {ex}", ex);
                return StatusCode(500, "Internal server error");
            }
        }


    }
}
