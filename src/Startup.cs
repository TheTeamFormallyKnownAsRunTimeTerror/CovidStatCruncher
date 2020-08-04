using CovidStatCruncher.Infrastructure.Data;
using CovidStatCruncher.Infrastructure.SecurityProvider;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using CovidStatCruncher.Ioc;
using CovidStatCruncher.Normalizer.Covid19Api.Services;
using CovidStatCruncher.Normalizer.Covid19Api.Settings;
using CovidStatCruncher.Normalizer.OwinDataSet.Services;
using CovidStatCruncher.Services;
using CovidStatCruncher.Settings.Deployment;
using Microsoft.EntityFrameworkCore;

namespace CovidStatCruncher
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
            services.AddLogging();

            // Add this to prevent object cycles 
            services.AddControllers().AddNewtonsoftJson(options =>
                options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore
            );
            services.AddSwaggerGen();

            services.AddDbContext<CovidStatCruncherContext>(
                options => options.UseMySql(Configuration.GetConnectionString("CovidData")),
                ServiceLifetime.Transient
            );
            services.Configure<DeploymentSettings>(Configuration.GetSection("Deployment"));

            services.AddCovid19ApiServices(Configuration);

            services.AddTransient<IAwsSecurityProvider, AwsSecurityProvider>();
            services.AddTransient<IAthenaDataService, AthenaDataService>();
            services.AddTransient<IAthenaUpdateService, AthenaUpdateService>();
            services.AddHostedService<OwinEnrichedDataNormalizingService>();
            services.AddHostedService<Covid19ApiNormalizingBackgroundService>();


        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseSwagger();
            app.UseSwaggerUI( c => 
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", $"{Constants.ApplicationName} v1");
            });

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
