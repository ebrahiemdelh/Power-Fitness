using Microsoft.Extensions.DependencyInjection;


namespace Power_Fitness.BLL
{
    public static class DI
    {
        public static IServiceCollection AddServices(this IServiceCollection services)
        {
            services.AddScoped<IPlansService, PlansService>();
            services.AddScoped<IMembersService, MembersService>();
            services.AddScoped<ISessionService, SessionService>();
            services.AddScoped<ITrainerService, TrainerService>();

            //services.AddAutoMapper(x=>x.AddProfile<MappingProfile>());
            services.AddAutoMapper(typeof(AssemblyReference).Assembly);
            return services;
        }
    }
}
