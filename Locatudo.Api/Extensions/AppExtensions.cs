using Locatudo.Domain.Handlers;
using Locatudo.Domain.Repositories;
using Locatudo.Infra.Data;
using Locatudo.Infra.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Locatudo.Api.Extensions
{
    public static class AppExtensions
    {
        public static void InjectConnection(this IServiceCollection services, string connectionString)
        {
            services.AddDbContext<LocatudoDataContext>(
                options =>
                    options.UseNpgsql(connectionString));
        }

        public static void InjectRepositories(this IServiceCollection services)
        {
            services.AddTransient<IDepartmentRepository, DepartmentRepository>();
            services.AddTransient<IEquipmentRepository, EquipmentRepository>();
        }

        public static void InjectHandlers(this IServiceCollection services)
        {
            services.AddTransient<CreateEquipmentHandler>();
        }
    }
}
