using EF.Generic_Query.API.Data;
using EF.Generic_Query.API.Data.Repositories;
using EF.Generic_Query.API.Data.Repositories.Interfaces;
using EF.Generic_Query.API.Data.Uow;
using EF.Generic_Query.API.Data.Uow.Interfaces;
using EF.Generic_Query.API.Services;
using EF.Generic_Query.API.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace EF.Generic_Query.API
{
    public class Startup
    {
        public IConfiguration Configuration { get; }

        public Startup(IConfiguration configuration) =>
            Configuration = configuration;

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<AppDbContext>(options =>
                options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));

            services.AddControllers();
            services.AddEndpointsApiExplorer();
            services.AddSwaggerGen();

            services.AddScoped<AppDbContext>();
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddScoped<ICategoryServices, CategoryService>();
            services.AddScoped<ICategoryRepository, CategoryRepository>();
        }

        public void Configure(WebApplication app, IWebHostEnvironment env)
        {
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            //app.UseHttpsRedirection();

            app.UseAuthorization();

            app.MapControllers();
        }
    }
}