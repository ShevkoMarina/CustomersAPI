using HSEApiTraining.Controllers;
using HSEApiTraining.Models.Options;
using HSEApiTraining.Providers;
using HSEApiTraining.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;

namespace HSEApiTraining
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
            services.AddMvc(options => options.EnableEndpointRouting = false);

            services.Configure<DbConnectionOptions>(Configuration.GetSection(nameof(DbConnectionOptions)));

            services.AddSingleton<ICalculatorService, CalculatorService>();
            services.AddSingleton<IDummyService, DummyService>();
            services.AddSingleton<ICurrencyService, CurrencyService>();
            services.AddSingleton<ICustomerService, CustomerService>();
            services.AddSingleton<IBanService, BanService>();

            services.AddSingleton<ISQLiteConnectionProvider, SQLiteConnectionProvider>();

            services.AddSingleton<ICustomerRepository, CustomerRepository>();
            services.AddSingleton<IBanRepository, BanRepository>();

            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_3_0);
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "HSE Training App", Version = "SabiyatAndMarina" });
            });
        }

        [System.Obsolete]
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "HSE Training App");
                c.RoutePrefix = "swagger";
            });
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseMvc();
        }
    }
}
