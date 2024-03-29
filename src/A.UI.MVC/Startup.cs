using Application.Extensions;
using Application.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Server.Kestrel.Core;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace Application
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

            services.ConfigMultipleSingletonServices();
            services.ConfigMultipleScopedServices(Configuration);
            services.ConfigMultipleTransientServices();

            services.ConfigControllers();
            services.ConfigAuthenticationsAndAuthorizationServices();
            services.ConfigCustomDbContext();
            //services.ConfigCustomMongoContext();
            services.ConfigCustomSwagger();
            services.ConfigCustomValidators();

            services.ConfigAutoMapper();
            services.ConfigEmailService();
            services.ConfigGraphQL();
            services.ConfigStackExchangeRedis();
            services.ConfigMassTransit();

            services.Configure<KestrelServerOptions>(options =>
              {
                  options.AllowSynchronousIO = true;
              });

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "Reason Systems App v1"));
            }
            else
            {
                //app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production 
                //scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();

            }
            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();
            app.UseWebSockets();

            app.UseAuthorization();

            app.ConfigCustomExceptionMiddleware();

            app.UseEndpoints(endpoints =>
            {
                // endpoints.MapControllerRoute(
                //     name: "default",
                //     pattern: "{controller=Home}/{action=Index}/{id?}");
                //That´s the same as below
                endpoints.MapDefaultControllerRoute();
                endpoints.MapGraphQL("/GraphQl");
            });


        }
    }
}
