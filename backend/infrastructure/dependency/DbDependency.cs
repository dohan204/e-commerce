using infrastructure.identity;
using infrastructure.services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
//using System.Data;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using domain.interfaces;
using infrastructure.repositories;
using application.interfaces;
using Serilog;
using domain.entities;
using application.cases.Dtos;
using infrastructure.persistence.entities;

namespace infrastructure.dependency
{
    public static class DbDependency
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            Log.Logger = new LoggerConfiguration()
                .MinimumLevel.Debug()
                .WriteTo.Console()
                .WriteTo.File("logs/log-.txt", rollingInterval: RollingInterval.Hour)
                .CreateLogger();

            services.AddAutoMapper(cfg =>
            {
                cfg.CreateMap<CategoryEntity, CategoryViewDto>();
                cfg.CreateMap<Category, CategoryEntity>();
                cfg.CreateMap<CategoryEntity, Category>();

                cfg.CreateMap<Products, ProductEntity>();
                cfg.CreateMap<ProductEntity, Products>();
                cfg.CreateMap<Products, ProductViewDto>();

                cfg.CreateMap<Order, OrderEntity>();
                cfg.CreateMap<OrderEntity, Order>();
                cfg.CreateMap<OrderItem, OrderItemEntity>();
                cfg.CreateMap<OrderItemEntity, OrderItem>();

                cfg.CreateMap<CartEntity, Cart>();
                cfg.CreateMap<Cart, CartEntity>();
                cfg.CreateMap<CartItemEntity, CartItem>();
                cfg.CreateMap<CartItem, CartItemEntity>();
                

                cfg.CreateMap<Review, ReviewEntity>()
                    .ForMember(dest => dest.ProductEntityId, opt => opt.MapFrom(src => src.ProductId));

                cfg.CreateMap<ReviewEntity, Review>()
                    .ForMember(dest => dest.ProductId, opt => opt.MapFrom(src => src.ProductEntityId));

                cfg.CreateMap<Voucher, VoucherEntity>().ReverseMap();
            });
            services.AddDbContext<ApplicationDbContext>(options =>
            {
                options.UseSqlServer(configuration.GetConnectionString("DefaultConnection"));
            });

            services.AddIdentityCore<AppUser>()
                .AddRoles<AppRole>()
                .AddEntityFrameworkStores<ApplicationDbContext>()
                .AddSignInManager()
                .AddDefaultTokenProviders();

            services.Configure<IdentityOptions>(options =>
            {
                options.Password.RequireNonAlphanumeric = true;
                options.Password.RequiredLength = 6;
                options.Password.RequireLowercase = true;
                options.Password.RequireUppercase = true;
                options.Password.RequireDigit = true;
                // options.Password.

                // signgin 
                options.SignIn.RequireConfirmedEmail = true;
            });
            services.AddAuthentication(options =>
                {
                    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                    options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
                }
            ).AddJwtBearer(options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    ValidIssuer = configuration.GetValue<string>("Jwt:Issuer"),
                    ValidAudience = configuration.GetValue<string>("Jwt:Audience"),
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration.GetValue<string>("Jwt:Key") ?? string.Empty))
                };
            });

            services.Configure<EmailOptions>(
                configuration.GetSection("EmailSettings")
            );
            services.AddMemoryCache();
            services.AddScoped<IEmailSender, EmailSender>();
            services.AddScoped<ICurrentUser, CurrentUser>();
            // services.AddScoped<IUserRepository, UserRepository>();
            // services.AddScoped<IAuthRepository, AuthRepository>();
            // services.AddScoped<ICategoryRepository, CategoryRepository>();
            // services.AddScoped<IProductRepository, ProductRepository>();
            // services.AddScoped<IOrderRepository, OrderRepository>();
            // services.AddScoped<ICartRepository, CartRepository>();
            // services.AddScoped<IReviewRepository, ReviewRepository>();
            services.AddScoped<IFileStorageService, FileStorageService>();
            // services.AddScoped<IVoucherRepository, VoucherRepository>();

            // tự động quét và đăng ký tất cả các cặp interface - implements
            services.Scan(scan => scan
                .FromAssemblyOf<UserRepository>() // lấ một class trong assembly hiện tại làm đại diện
                .AddClasses(classes => classes.Where(e => e.Name.EndsWith("Repository")))  // lọc và lấy ra các class có đuôi là repository để đăng ký
                .AsImplementedInterfaces() // map với interface đang được class triển khai đó 
                .WithScopedLifetime()); // đăng ký vòng đời ở đây là scoped giống như đăng ký bình thường
            services.AddScoped<JwtToken>();
            services.AddSingleton<EmailSender>();
            return services;
        }
    }
}
