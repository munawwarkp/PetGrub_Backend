using AutoMapper;
using Microsoft.EntityFrameworkCore;
using PetGrubBakcend.ApiResponse;
using PetGrubBakcend.Data;
using PetGrubBakcend.DTOs;
using PetGrubBakcend.Entities;

namespace PetGrubBakcend.Repositories.Categ
{
    public class CategoryRepository:ICategoryRepository
    {
        private readonly AppDbContext _context;
        private readonly IMapper _mapper;
        public CategoryRepository(AppDbContext context,IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        public async Task<ApiResponse<object>> AddCategory(AddCategoryDto addCategoryDto)
        {
            try
            {
                var existingCat = await _context.Categories.FirstOrDefaultAsync(c => c.Name == addCategoryDto.Name);
                if (existingCat != null)
                {
                    return new ApiResponse<object> { StatusCode = 409, Message = "Category already exist" };
                }

                var category = _mapper.Map<Category>(addCategoryDto);
                _context.Categories.Add(category);
                await _context.SaveChangesAsync();
                return new ApiResponse<object>
                {
                    StatusCode = 200,
                    Message = "succesfully added category."
                };
            }
            catch (Exception ex)
            {
                return new ApiResponse<object>
                {
                    StatusCode = 500,
                    Message = ex.Message
                };
            }
        }

    }
}
