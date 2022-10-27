using Locatudo.Domain.Entities;
using Locatudo.Domain.Handlers;
using Locatudo.Domain.Queries.Handlers;
using Locatudo.Domain.Repositories;
using Locatudo.Infra.Data;
using Locatudo.Infra.Repositories;
using Locatudo.Shared.Repositories;
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
            services.AddTransient<IUserRepository, UserRepository>();
            services.AddTransient<IEmployeeRepository, EmployeeRepository>();
            services.AddTransient<IOutsourcedRepository, OutsourcedRepository>();
            services.AddTransient<IRentalRepository, RentalRepository>();
        }

        public static void InjectHandlers(this IServiceCollection services)
        {
            services.AddTransient<CreateEquipmentHandler>();
            services.AddTransient<CreateRentalHandler>();
            services.AddTransient<ApproveRentalHandler>();
            services.AddTransient<CancelRentalHandler>();
            services.AddTransient<DisapproveRentalHandler>();
            services.AddTransient<ChangeEquipmentManagerHandler>();
            services.AddTransient<DeleteOutsourcedHandler>();
        }
    }
}
