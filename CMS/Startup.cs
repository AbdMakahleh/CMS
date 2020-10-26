
using Business.CommandParams;
using DataBase.Context;
using DataBase.Interfaces;
using DataBase.Locater;
using DataBase.Repository;
using DataBase.UnitOfWork;
using Infrastructure.Database;
using Infrastructure.Interfaces;
using Infrastructure.Proxies;
using Infrastructure.Repository;
using Infrastructure.UnitOfWork;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace CMS
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();
            services.AddLogging();
            services.AddMvc().AddNewtonsoftJson();
            var connectionString = Configuration.GetConnectionString("DefaultConnection");
            QueryExecuter.ConnectionString = connectionString;
            services.AddDbContext<CMSContext>(x => x.
UseNpgsql(connectionString, b => b.MigrationsAssembly("CMS"))
.ConfigureWarnings(warnings => warnings.Ignore(CoreEventId.IncludeIgnoredWarning)));
            services.AddMemoryCache();
            services.AddScoped<IUnitOfWork<CMSContext>, UnitOfWork<CMSContext>>();
            services.AddScoped<IProxyLocater, ProxyLocater>();
            services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
            services.AddScoped<IDBMangerLocator, DBMangerLocator>();
            services.AddScoped<ICommandParam, CommandParam>();
            //services.addD
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
