using PetGrubBakcend.ApiResponse;
using PetGrubBakcend.DTOs;

namespace PetGrubBakcend.Services.Categ
{
    public interface ICategoryService
    {
        Task<ApiResponse<object>> AddCategory(AddCategoryDto addCategoryDto);
    }
}
