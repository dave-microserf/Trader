namespace Czarnikow.Trader.Api
{
    using Czarnikow.Trader.Application.Interfaces;
    using Czarnikow.Trader.Application.Services;
    using Czarnikow.Trader.Core.Interfaces;
    using Czarnikow.Trader.Infrastructure.Db;
    using Czarnikow.Trader.Infrastructure.Db.EntityFramework;
    using Microsoft.AspNetCore.Builder;
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.Extensions.Hosting;

    public class Startup
    {
        // Should be in infrastructure??
        public static readonly string ConnectionString = "Data Source=.;Initial Catalog=TraderDb;Integrated Security=True";

        public Startup(IConfiguration configuration)
        {
            this.Configuration = configuration;
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();            
            services.AddDbContext<TraderDbContext>(
                (serviceProvider, optionsBuilder) => serviceProvider.GetRequiredService<IDbContextOptionsStrategy>().Configure(optionsBuilder));
            
            services.AddScoped<IApplicationService, ApplicationService>();
            services.AddScoped<ITraderDbContext, TraderDbContext>();
            services.AddScoped<IUnitOfWork, UnitOfWork>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            // TODO: suitable for development only
            app.UseCors(builder => {
                builder.AllowAnyOrigin();
                builder.AllowAnyMethod();
                builder.AllowAnyHeader();
            });
            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }

        public IConfiguration Configuration { get; }
    }
}