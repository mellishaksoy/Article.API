using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Article.API.Infrastructure.Contexts.ArticleEntity;
using Article.API.Infrastructure.Extensions;
using Article.API.Infrastructure.Filters;
using Article.API.Infrastructure.Middlewares;
using Article.API.Settings;
using IdentityServer4.AccessTokenValidation;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Serilog;

namespace Article.API
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
            services.Configure<AppSettings>(Configuration.GetSection("AppSettings"));

            Log.Logger = new LoggerConfiguration().ReadFrom.ConfigurationSection(Configuration.GetSection("Serilog")).CreateLogger();


            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();

            services.AddHttpClient();
            services.AddLocalization(o =>
            {
                o.ResourcesPath = "Resources";
            });

            services.AddMvc(o =>
            {
                o.Filters.Add(typeof(ArticleHttpGobalExceptionFilter));
            }).SetCompatibilityVersion(CompatibilityVersion.Version_2_1);

            services.AddMvc().AddJsonOptions(
              options => options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore
          );

            var connString = Configuration.GetSection("ConnectionString").Value;
            services.AddDbContext<ArticlesContext>(
                (options) =>
                {
                    options.ConfigureDatabase(connString);
                });

            
            services.AddApplicationDependencies(Configuration);
            services.AddOptions();

            services.AddAuthorization();
            services.AddAuthentication(IdentityServerAuthenticationDefaults.AuthenticationScheme)
                .AddIdentityServerAuthentication(options =>
                {
                    options.Authority = Configuration.GetSection("IdentityServerSetting")["IdentityServerUrl"];
                    options.ApiName = Configuration["ApiName"];
                    options.RequireHttpsMetadata = false;
                    options.ApiSecret = Configuration.GetSection("IdentityServerSetting")["ClientSecret"]; 
                    options.SupportedTokens = SupportedTokens.Both;
                });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseHsts();
            }
            loggerFactory.AddSerilog();
            app.UseMiddleware<SerilogRequestLoggerMiddleware>();

            app.UseAuthentication();
            app.UseHttpsRedirection();
            app.UseMvc();
            
            using (var scope = app.ApplicationServices.GetService<IServiceScopeFactory>().CreateScope())
            {
                scope.ServiceProvider.GetRequiredService<ArticlesContext>().Database.EnsureCreated();
            }
            

           
        }
    }
}
