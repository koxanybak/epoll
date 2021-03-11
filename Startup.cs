using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using Microsoft.EntityFrameworkCore;
using epoll.Persistence.Contexts;
using epoll.Domain.Repositories;
using epoll.Persistence.Repositories;
using epoll.Domain.Services;
using epoll.Services;

namespace epoll
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<AppDbContext>(opt => opt.UseNpgsql(Configuration.GetConnectionString("Postgres")));

            // Determine what classes implement the interfaces given to constructors
            services.AddScoped<IPollRepository, PollRepository>();
            services.AddScoped<IPollService, PollService>();
            services.AddScoped<IUnitOfWork, UnitOfWork>();

            // Add auto mapper
            services.AddAutoMapper(typeof(Startup));


            services.AddControllers();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "epoll", Version = "v1" });
            });
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "epoll v1"));
            }

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
