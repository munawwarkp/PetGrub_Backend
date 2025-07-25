﻿using PetGrubBakcend.ApiResponse;
using PetGrubBakcend.DTOs;
using PetGrubBakcend.Entities;
using PetGrubBakcend.Result;

namespace PetGrubBakcend.Repositories.Prod
{
    public interface IProdRepository
    {
        Task<Product?> GetExistingProduct(int productId);
        Task<Product> AddProductAsync(Product product);
        Task<List<Product>> GetProducts();
        Task UpdateProduct(Product product);
        Task<Category> GetExistingCategory(int categoryId);  

        Task<Product> GetProductById(int id);
        Task<List <Product>> GetProductsByCategoryName(string name);


        Task DeleteProductAsync(Product product);
        Task<List<Product>> SearchProducts(string str);

    }
}
