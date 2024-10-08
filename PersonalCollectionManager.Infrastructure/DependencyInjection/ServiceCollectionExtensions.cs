﻿using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using PersonalCollectionManager.Application.Interfaces.IAuthService;
using PersonalCollectionManager.Application.Interfaces.IRepository;
using PersonalCollectionManager.Application.Interfaces.IServices;
using PersonalCollectionManager.Application.Interfaces.Services;
using PersonalCollectionManager.Data.Repositories;
using PersonalCollectionManager.Infrastructure.AuthenticationServices;
using PersonalCollectionManager.Infrastructure.Data;
using PersonalCollectionManager.Infrastructure.Repositories;
using PersonalCollectionManager.Infrastructure.Services;
using PersonalCollectionManager.Shared.Helpers;

namespace PersonalCollectionManager.Infrastructure.DependencyInjection
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<AppDbContext>(options =>
                options.UseSqlServer(configuration.GetConnectionString("PersonalCollectionManagerDb"),
                b => b.MigrationsAssembly(typeof(ServiceCollectionExtensions).Assembly.FullName))
                .LogTo(Console.WriteLine, LogLevel.Information));

            services.AddSingleton<IDbContextFactory<AppDbContext>>(sp =>
            {
                var optionsBuilder = new DbContextOptionsBuilder<AppDbContext>();
                optionsBuilder.UseSqlServer(configuration.GetConnectionString("PersonalCollectionManagerDb"),
                    b => b.MigrationsAssembly(typeof(ServiceCollectionExtensions).Assembly.FullName))
                    .LogTo(Console.WriteLine, LogLevel.Information);

                return new PooledDbContextFactory<AppDbContext>(optionsBuilder.Options);
            });

            // Helpers
            services.AddAutoMapper(typeof(AutoMapperProfile));

            // Repositories
            services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
            services.AddScoped<ICollectionRepository, CollectionRepository>();
            services.AddScoped<ICommentRepository, CommentRepository>();
            services.AddScoped<IItemRepository, ItemRepository>();
            services.AddScoped<ILikeRepository, LikeRepository>();
            services.AddScoped<ITagRepository, TagRepository>();
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IItemTagRepository, ItemTagRepository>();
            services.AddScoped<IAuthRepository, AuthRepository>();
            services.AddScoped<ICustomFieldRepository, CustomFieldRepository>();
            services.AddScoped<ICustomFieldValueRepository, CustomFieldValueRepository>();
            services.AddScoped<ICategoryRepository, CategoryRepository>();

            // Services
            services.AddScoped<IAdminServices, AdminService>();
            services.AddScoped<IAccountService, AccountService>();
            services.AddScoped<ICollectionService, CollectionService>();
            services.AddScoped<ICommentService, CommentService>();
            services.AddScoped<IItemService, ItemService>();
            services.AddScoped<ITagService, TagService>();
            services.AddScoped<ILikeService, LikeService>();
            services.AddScoped<ICollectionService, CollectionService>();
            services.AddScoped<ICustomFieldService, CustomFieldService>();
            services.AddScoped<ICategoryService, CategoryService>();

            // Authentication Service
            services.AddScoped<IJwtTokenService, JwtTokenService>();

            // Algolia Search Service
            services.AddScoped<AlgoliaItemService>();


            return services;
        }
    }
}
