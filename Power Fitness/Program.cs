using Microsoft.EntityFrameworkCore;
using Power_Fitness.BLL.Contracts;
using Power_Fitness.BLL.Services;
using Power_Fitness.DAL.Context;
using Power_Fitness.DAL.Contracts;
using Power_Fitness.DAL.Repositories;

namespace Power_Fitness
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.Services.AddDbContext<GymDbContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

            builder.Services.AddScoped<IPlansService, PlansService>();
            builder.Services.AddScoped<IMembersService, MembersService>();

            builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
            builder.Services.AddScoped(typeof(IRepository<>), typeof(Repository<>)); // Use this line for real database repository
            builder.Services.AddScoped<IMemberRepository, MemberRepository>(); // Use this line for real database repository
            //builder.Services.AddScoped(typeof(IPlansRepository<>), typeof(PlansMockRepository<>)); // Use this line for mock repository

            builder.Services.AddScoped<IPlanRepository, PlanRepository>();
            builder.Services.AddScoped<IHealthRecordRepository, HealthRecordRepository>();

            // Add services to the container.
            builder.Services.AddControllersWithViews();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseRouting();

            app.UseAuthorization();

            app.MapStaticAssets();
            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}")
                .WithStaticAssets();

            app.Run();
        }
    }
}
