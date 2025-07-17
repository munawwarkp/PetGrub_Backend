
using Microsoft.EntityFrameworkCore;
using PetGrubBakcend.Data;
using PetGrubBakcend.Entities;
using PetGrubBakcend.Result;

namespace PetGrubBakcend.Repositories.Prod
{
    public class ProdRepository:IProdRepository
    {
        //private readonly IMapper _mapper;
        private readonly AppDbContext _context;
        private readonly ILogger<ProdRepository> _logger;
        //private readonly ICloudinaryService _cloudinary;
        public ProdRepository(AppDbContext context,ILogger<ProdRepository> logger)
        {
            _context = context;
            _logger = logger;
            //_mapper = mapper;
            //_cloudinary = cloudinary;
        }
        public async Task<Product> AddProductAsync(Product product)
        {
            try
            {

                 _context.Products.Add(product);
                await _context.SaveChangesAsync();
                return product;


                //var product = _mapper.Map<Product>(addProductDto);

                //if (addProductDto.Image != null)
                //{
                //    product.ImageUrl = await _cloudinary.UploadImageAsync(addProductDto.Image);
                //}

                //_context.Products.Add(product);
                //await _context.SaveChangesAsync();

                //return new ApiResponse<object>
                //{
                //    StatusCode = 200,
                //    Message = "Product added succesfully"
                //};

            }
            catch (Exception ex)
            {
                //return new ApiResponse<object>
                //{
                //    StatusCode = 500,
                //    Message = "Something went wrong while adding product",
                //    Error = ex.Message,
                //};
                _logger.LogError ("failed to add product to database : {ex}", ex);
                throw;
            }


        }

        public async Task<List<Product>> GetProducts()
        {
            try
            {
                var products = await  _context.Products.ToListAsync();
                await _context.SaveChangesAsync();
                return products;
            }
            catch (Exception ex)
            {
                throw;
            }
        }


        public async Task<Product> GetProductById(int id)
        {
            try
            {
                var product = await _context.Products.FirstOrDefaultAsync(p => p.Id == id);
                await _context.SaveChangesAsync();
                return product ;

            }
            catch (Exception ex)
            {
                throw;
            }
            
        }

        public async Task<List<Product>> GetProductsByCategoryName(string name)
        {
            try
            {
                var productsByCat = await _context.Products.Include(p => p.Category).Where(c => c.Category.Name == name).ToListAsync();
                await _context.SaveChangesAsync();
                return productsByCat;
            }
            catch(Exception ex)
            {
                throw;
            }
        }

        public async Task DeleteProductAsync(Product product)
        {
           _context.Products.Remove(product);
            await _context.SaveChangesAsync();
        }

        public async Task<List<Product>> SearchProducts(string str)
        {
            //if (string.IsNullOrEmpty(str))
            //{
            //    return 
            //}
            try
            {
                var products = await _context.Products
                    .Include(p => p.Category)
                    .Where(pe =>
                        pe.Category.Name.ToLower().Contains(str) ||
                        pe.Title.ToLower().Contains(str))
                    .ToListAsync();
                if(products.Count == 0)
                {
                    throw new InvalidOperationException("product not found");
                }
                return products;
            }
            catch
            {
                throw;
            }
            
        }


    }
}
