
using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using PetGrubBakcend.CloudinaryS;
using PetGrubBakcend.CustomMiddleware;
using PetGrubBakcend.Data;
using PetGrubBakcend.Mappings;
using PetGrubBakcend.Repositories.Address;
using PetGrubBakcend.Repositories.AuthRepository;
using PetGrubBakcend.Repositories.Cart;
using PetGrubBakcend.Repositories.Categ;
using PetGrubBakcend.Repositories.Ord;
using PetGrubBakcend.Repositories.Prod;
using PetGrubBakcend.Repositories.Usr;
using PetGrubBakcend.Repositories.wishlist;
using PetGrubBakcend.Services.Address;
using PetGrubBakcend.Services.Auth;
using PetGrubBakcend.Services.AuthServices;
using PetGrubBakcend.Services.Cart;
using PetGrubBakcend.Services.Categ;
using PetGrubBakcend.Services.Ord;
using PetGrubBakcend.Services.Prod;
using PetGrubBakcend.Services.Razorpay;
using PetGrubBakcend.Services.Usr;
using PetGrubBakcend.Services.wishlist;

namespace PetGrubBakcend
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            //Add CORS

            builder.Services.AddCors(option =>
            {
                option.AddPolicy("AllowReactApp",
                    policy =>
                    {
                        policy.WithOrigins("http://localhost:5173")
                        .AllowAnyHeader()
                        .AllowAnyMethod()
                        .AllowCredentials();
                    });
            });

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen(option =>
            {
                option.AddSecurityDefinition("Bearer", new Microsoft.OpenApi.Models.OpenApiSecurityScheme
                {
                    Name = "Authorization",
                    Type = SecuritySchemeType.ApiKey,
                    Scheme = "Bearer",
                    BearerFormat = "JWT",
                    In = ParameterLocation.Header,
                    Description = "JWT Authorization header using the Bearer scheme."
                });
                option.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    {
                        new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference
                            {
                                Id = "Bearer",
                                Type = ReferenceType.SecurityScheme
                            },
                            Scheme = "Bearer",
                            Name = "Authorization",
                            In = ParameterLocation.Header
                        },
                        new List<string>()
                    }
                });
            });

            builder.Services.AddScoped<IAuthRepository, AuthRepository>();
            builder.Services.AddScoped<IAuthService, AuthService>();

            builder.Services.AddScoped<IJWTGenerator, JWTGenerator>();

            builder.Services.AddScoped<ICloudinaryService,CloudinaryService>();

            builder.Services.AddScoped<ICategoryRepository, CategoryRepository>();
            builder.Services.AddScoped<ICategoryService, CategoryService>();

            builder.Services.AddScoped<IProdRepository, ProdRepository>();
            builder.Services.AddScoped<IProdService, ProdService>();

            builder.Services.AddScoped<IWishlistRepository, WishlistRepository>();
            builder.Services.AddScoped<IWishlistService, WishlistService>();

            builder.Services.AddScoped<ICartRepesitory, CartRepository>();
            builder.Services.AddScoped<ICartService, CartService>();

            builder.Services.AddScoped<IAddressRepository,AddressRepository>();
            builder.Services.AddScoped<IAddressService, AddressService>();

            builder.Services.AddScoped<IProdRepository,ProdRepository>();
            builder.Services.AddScoped<IProdService, ProdService>();

            builder.Services.AddScoped<IUserRepository, UserRepository>();
            builder.Services.AddScoped<IUserService, UserService>();

            builder.Services.AddScoped<IOrderRepository, OrderRepository>();
            builder.Services.AddScoped<IOrderService, OrderService>();

            builder.Services.AddScoped<IRazorPayService, RazorpayService>();
            //connection string
            string connectionString = builder.Configuration.GetConnectionString("DefaultConnection")
                ?? throw new InvalidOperationException("Error occured at 'DefaultConnection'");

            //configure database    
            builder.Services.AddDbContext<AppDbContext>(option =>
                option.UseSqlServer(connectionString)
            );

            //register automapper
            builder.Services.AddAutoMapper(typeof(AutoMapperProfile));

            //authentication service registration

            var key = Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"]);

            builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(options =>
                {
                    //define token validatoin parameters to ensure tokens are valid and trustworthy
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuer = true,
                        ValidIssuer = builder.Configuration["Jwt:Issuer"],
                        ValidateAudience = false,
                        ValidateLifetime = true,
                        ValidateIssuerSigningKey = true,
                        ClockSkew = TimeSpan.Zero,
                        IssuerSigningKey = new SymmetricSecurityKey(key)
                    };
                });

            //builder.Services.AddAuthorization(options =>
            //{
            //    options.AddPolicy("AdminOnly", policy => policy.RequireRole("admin"));
            //    options.AddPolicy("UserOnly", policy => policy.RequireRole("user"));
            //});

            builder.Services.AddAuthorization(options =>
            {
                options.AddPolicy("AdminOnly", policy => policy.RequireRole("admin"));
                options.AddPolicy("UserOnly", policy => policy.RequireRole("user"));
            });
                
            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseCors("AllowReactApp");

            app.UseAuthentication();
            app.UseMiddleware<CustomMIddleware>();
            app.UseAuthorization();

            app.MapControllers();

            app.Run();
        }
    }
}
