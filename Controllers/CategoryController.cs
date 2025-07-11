using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PetGrubBakcend.DTOs;
using PetGrubBakcend.Services.Categ;
using PetGrubBakcend.ApiResponse;

namespace PetGrubBakcend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        private readonly ICategoryService _categoryService;
        public CategoryController(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }

        [HttpPost("Add-Category")]
        public async Task<ApiResponse<object>> AddCateg(AddCategoryDto categoryDto)
        {
            return await _categoryService.AddCategory(categoryDto);
        }
    }
}
