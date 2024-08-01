using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using PersonalCollectionManager.Application.Interfaces.IRepository;
using PersonalCollectionManager.Application.Interfaces.IServices;
using PersonalCollectionManager.Application.Interfaces.Services;
using PersonalCollectionManager.Data.Repositories;
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
                .EnableSensitiveDataLogging()
                .LogTo(Console.WriteLine, LogLevel.Information),
                ServiceLifetime.Scoped);

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

            // Services
            services.AddScoped<IAdminServices, AdminService>();
            services.AddScoped<IAccountService, AccountService>();
            services.AddScoped<ICollectionService, CollectionService>();
            services.AddScoped<ICommentService, CommentService>();
            services.AddScoped<IItemService, ItemService>();
            services.AddScoped<ITagService, TagService>();

            services.AddScoped<AdminService>();
            services.AddScoped<AccountService>();
            services.AddScoped<CollectionService>();
            services.AddScoped<CommentService>();
            services.AddScoped<ItemService>();
            services.AddScoped<TagService>();


            return services;
        }

    }
}
