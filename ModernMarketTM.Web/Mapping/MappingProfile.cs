using AutoMapper;
using ModernMarketTM.Models;
using ModernMarketTM.Web.Areas.Admin.Models.BindingModels;
using ModernMarketTM.Web.Areas.Admin.Models.ViewModels;
using ModernMarketTM.Web.Models;

namespace ModernMarketTM.Web.Mapping
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<User, UserViewModel>();
            CreateMap<User, ChangePasswordViewModel>();
            CreateMap<User, UserDetailsViewModel>();
            CreateMap<AddCategorieBindingModel, Category>();
            CreateMap<Category, CategoriesViewModel>();
            CreateMap<Category, RemoveCategoriesViewModel>();
            CreateMap<AddInstanceCategoryBindingModel, CategoryInstance>();
            CreateMap<CategoryInstance, CategoryInstanceViewModel>();
            CreateMap<Type, TypesViewModel>();
        }
    }
}
