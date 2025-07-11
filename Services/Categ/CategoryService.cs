using PetGrubBakcend.ApiResponse;
using PetGrubBakcend.DTOs;
using PetGrubBakcend.Repositories.Categ;

namespace PetGrubBakcend.Services.Categ
{
    public class CategoryService:ICategoryService
    {
        private readonly ICategoryRepository _repository;
        public CategoryService(ICategoryRepository categoryRepository)
        {
            _repository = categoryRepository;
        }

        public async Task<ApiResponse<object>> AddCategory(AddCategoryDto addCategoryDto)
        {
          return await _repository.AddCategory(addCategoryDto);
        }
    }
}
