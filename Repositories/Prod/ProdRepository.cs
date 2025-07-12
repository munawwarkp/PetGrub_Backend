
using PetGrubBakcend.Data;
using PetGrubBakcend.Entities;

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

        //public async Task<List<ProductDto>> GetAllProducts()
        //{
        //    try
        //    {
        //        var products = await _context.Products.ToListAsync();
        //        return products;                
        //    }
        //    catch (Exception ex)
        //    {

        //    }
        //}



    }
}
