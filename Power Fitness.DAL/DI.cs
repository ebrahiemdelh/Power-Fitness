using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Power_Fitness.DAL
{
    public static class DI
    {
        public static IServiceCollection AddDBs(this IServiceCollection services, IConfiguration config)
        {
            services.AddDbContext<GymDbContext>(options => options.UseSqlServer(config.GetConnectionString("DefaultConnection")));

            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddScoped(typeof(IRepository<>), typeof(Repository<>)); // Use this line for real database repository
            services.AddScoped<IMemberRepository, MemberRepository>(); // Use this line for real database repository
            //services.AddScoped(typeof(IPlansRepository<>), typeof(PlansMockRepository<>)); // Use this line for mock repository

            services.AddScoped<IHealthRecordRepository, HealthRecordRepository>();
            services.AddScoped<IPlanRepository, PlanRepository>();
            services.AddScoped<ISessionRepository, SessionRepository>();
            return services;
        }
    }
}
