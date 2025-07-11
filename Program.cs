
using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using PetGrubBakcend.CloudinaryS;
using PetGrubBakcend.Data;
using PetGrubBakcend.Mappings;
using PetGrubBakcend.Repositories.AuthRepository;
using PetGrubBakcend.Repositories.Categ;
using PetGrubBakcend.Repositories.Prod;
using PetGrubBakcend.Services.Auth;
using PetGrubBakcend.Services.AuthServices;
using PetGrubBakcend.Services.Categ;
using PetGrubBakcend.Services.Prod;

namespace PetGrubBakcend
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            builder.Services.AddScoped<IAuthRepository, AuthRepository>();
            builder.Services.AddScoped<IAuthService, AuthService>();

            builder.Services.AddScoped<IJWTGenerator, JWTGenerator>();

            builder.Services.AddScoped<ICloudinaryService,CloudinaryService>();

            builder.Services.AddScoped<ICategoryRepository, CategoryRepository>();
            builder.Services.AddScoped<ICategoryService, CategoryService>();

            builder.Services.AddScoped<IProdRepository, ProdRepository>();
            builder.Services.AddScoped<IProdService, ProdService>();

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

            app.UseAuthentication();
            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}
