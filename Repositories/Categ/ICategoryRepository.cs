using PetGrubBakcend.ApiResponse;
using PetGrubBakcend.DTOs;
namespace PetGrubBakcend.Repositories.Categ
{
    public interface ICategoryRepository
    {
        Task<ApiResponse<object>> AddCategory(AddCategoryDto addCategoryDto);
    }

}
