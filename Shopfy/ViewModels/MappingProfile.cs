using AutoMapper;
using Shopfy.Models;
using Shopfy.ViewModels.Dtos;

namespace Shopfy.ViewModels
{
    public class MappingProfile:Profile
    {
       public MappingProfile()
        {
            #region mapping category object 
            #endregion

            CreateMap<CategoryDto, Category>();
            CreateMap<Category, CategoriesDto>();

            #region mapping Sub Categories object 
            #endregion
            CreateMap<SubCategory, SubCategoriesDto>();
            CreateMap<SubCategoryDto, SubCategory>();
            #region mapping product object 
            #endregion

            CreateMap<ProductDto, Product>()
                .ForMember(p => p.ProductImages,opt => opt.Ignore())
                .ForMember(p=> p.ProductThumbnail,opt => opt.Ignore());
            #region mapping order object 
            #endregion
            CreateMap<OrderDto, Order>();

        }
    }
}
