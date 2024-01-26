using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Shopfy.Models;
using Shopfy.Models.Interfaces;
using Shopfy.ViewModels.Dtos;
using System.Data;

namespace Shopfy.Controllers
{
    [Route("/api/categories")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        private readonly ICategoryRepository _categoryRepository;
        private readonly ILogger<CategoryController> _logger;
        private readonly IMapper _mapper;
        
        public CategoryController(
            ICategoryRepository categoryRepository,
            ILogger<CategoryController> logger ,
            IMapper mapper
            ) 
        {
            _categoryRepository = categoryRepository;
            _logger = logger;
            _mapper = mapper;
        }
        #region ----- get all categories ----- 
        #endregion
        [HttpGet]
        public IActionResult GetAllCategories()
        {
            try 
            {
                var allCategories = _categoryRepository.AllCategories();
                var CategoriesMap = _mapper.Map<IEnumerable<CategoriesDto>>(allCategories);
                if (CategoriesMap.Count() == 0 )
                {
                    return NotFound();
                }
                else
                {
                    return Ok(CategoriesMap);

                }
            }
            catch(Exception ex)
            {
                _logger.LogError("Not found any catgory : {ex}",ex);
                return StatusCode(500, "Internal server error");
            }
        }
        #region ----- get category using id ----
        #endregion

        [HttpGet]
        [Route("category/{categoryId}")]
        public IActionResult GetCategoy(Guid categoryId)
        {
            try
            { 
                var cateogry = _categoryRepository.GetCategoryById(categoryId);
                if (cateogry != null)
                {
                    return Ok(cateogry);

                }
                else
                {
                    return NotFound();
                }
            }
            catch (Exception ex)
            {
                _logger.LogError("Not found any catgory : {ex}", ex);
                return StatusCode(500, "Internal server error");
            }
        }
        [HttpGet]
        [Route("category/{categoryId}/subcategories")]
        public IActionResult GetAllSubCategoryByCategoryId(Guid categoryId)
        {
            try
            {
                var SubCategories = 
                    _categoryRepository.GetAllSubCategoriesByCategoryId(categoryId);
                if(SubCategories is null)
                    return NotFound();
                return Ok(SubCategories);
                
            }catch(Exception ex)
            {
                _logger.LogError("Not found any Subcatgory : {ex}", ex);
                return StatusCode(500, "Internal server error");
            }
        }
       
        [HttpGet]
        [Route("subcategories")]
        public IActionResult GetAllCategoriesWithSubCategories()
        {
            try
            {
                var AllCategoriesWithSubCategories =
                    _categoryRepository.GetAllCategoriesWithSubCategory();
                if (AllCategoriesWithSubCategories is null)
                    return NotFound();
                return Ok(AllCategoriesWithSubCategories);

            }
            catch (Exception ex)
            {
                _logger.LogError("Not found any catgory or Any SubCategory : {ex}", ex);
                return StatusCode(500, "Internal server error");
            }
        }

        [HttpGet]
        [Route("{categoryName}")]
        public IActionResult GetCategoryByName(string categoryName)
        {
            try
            {
                if(categoryName is null)
                {
                    _logger.LogError("category name sent from client is null ");
                    return BadRequest("Category name is Empty !");
                }
                return Ok(_categoryRepository.GetCategoryByName(categoryName));


            }
            catch (Exception ex)
            {
                _logger.LogError("Search by category name is something wrong !  : {ex}", ex);
                return StatusCode(500, "Internal server error");
            }
        }
        #region ----- create category ----- 
        #endregion
        [HttpPost]
        [Route("category",Name ="category/")]
        public IActionResult CreateCategory(CategoryDto category)
        {
            try
            {
                if(category is null)
                {
                    _logger.LogError("category object sent from client is null ");
                    return BadRequest("Category object is null !");
                }

                if (!ModelState.IsValid)
                {
                    _logger.LogError("Invalid object sent from client ! ");
                    return BadRequest("Invalid category object !");
                }
                var CategoryMap = _mapper.Map<Category>(category);
                var result =  _categoryRepository.CreateCategory(CategoryMap);
                /*return StatusCode(201, category);*/
                
                return CreatedAtRoute( new { result.Id }, CategoryMap);
            }
            catch (Exception ex)
            {
                _logger.LogError("Can't Create category : {ex}", ex);
                return StatusCode(500, "Internal server error");
            }
        }
        #region  ----- delete category ----- 
        #endregion 
        [HttpDelete]
        [Route("{categoryId}")]
        public IActionResult DeleteCategory(Guid categoryId)
        {
            try
            {
                var category = _categoryRepository.GetCategoryById(categoryId);
                if(category is null)
                {
                    _logger.LogError($"Category with id : {categoryId} , hasn't been found in db !");
                    return NotFound($"can not found category with id {categoryId} ");
                }
                _categoryRepository.DeleteCategoryById(categoryId);
                return Accepted("category is deleted successfully !");
            }
            catch (Exception ex)
            {
                _logger.LogError("Can't delete category : {ex}", ex);
                return StatusCode(500, "Internal server error");
            }
        }
        #region ----- update category -----
        #endregion
        [HttpPut]
        [Route("{categoryId}")]
        public IActionResult UpdateCategory(Guid categoryId,CategoryDto category)
        {
            try
            {
                if (category is null)
                {
                    _logger.LogError("category object sent from client is null ");
                    return BadRequest("Category object is null !");
                }

                if (!ModelState.IsValid)
                {
                    _logger.LogError("Invalid object sent from client ! ");
                    return BadRequest("Invalid category object !");
                }
                var get_category = _categoryRepository.GetCategoryById(categoryId);
                if (get_category is null)
                {
                    _logger.LogError($"get_category with id: {categoryId}, hasn't been found in db.");
                    return NotFound();
                }
                var CategoryMap = _mapper.Map<Category>(category);
                _categoryRepository.UpdateCategory(CategoryMap);
                
                return Accepted(category);
            }
            catch (Exception ex)
            {
                _logger.LogError("Can't update category : {ex}", ex);
                return StatusCode(500, "Internal server error");
            }
            
        }

    }
}
