using AutoMapper;
using CloudinaryDotNet;
using Microsoft.EntityFrameworkCore;
using PetGrubBakcend.ApiResponse;
using PetGrubBakcend.CloudinaryS;
using PetGrubBakcend.Data;
using PetGrubBakcend.DTOs;
using PetGrubBakcend.Entities;

namespace PetGrubBakcend.Repositories.Prod
{
    public class ProdRepository:IProdRepository
    {
        private readonly IMapper _mapper;
        private readonly AppDbContext _context;
        private readonly ICloudinaryService _cloudinary;
        public ProdRepository(AppDbContext context,IMapper mapper,ICloudinaryService cloudinary)
        {
            _context = context;
            _mapper = mapper;
            _cloudinary = cloudinary;
        }
        public async Task<ApiResponse<object>> AddProduct(ProductDto addProductDto)
        {
            try
            {
                var product = _mapper.Map<Product>(addProductDto);

                if (addProductDto.Image != null)
                {
                    product.ImageUrl = await _cloudinary.UploadImageAsync(addProductDto.Image);
                }

                _context.Products.Add(product);
                await _context.SaveChangesAsync();

                return new ApiResponse<object>
                {
                    StatusCode = 200,
                    Message = "Product added succesfully"
                };

            }
            catch (Exception ex)
            {
                return new ApiResponse<object>
                {
                    StatusCode = 500,
                    Message = "Something went wrong while adding product",
                    Error = ex.Message,
                };
            }


        }



    }
}
