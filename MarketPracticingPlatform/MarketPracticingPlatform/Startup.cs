using MarketPracticingPlatform.Authentication_token;
using MarketPracticingPlatform.Data.DataBaseConnection;
using MarketPracticingPlatform.Service.Interface;
using MarketPracticingPlatform.Service.Services;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using Serilog;

namespace MarketPracticingPlatform
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

            services.AddScoped<IUserDataService, UserDataService>();

            services.AddScoped<IProductDataService, ProductDataService>();
            services.AddScoped<ICategoryDataService, CategoryDataService>();


            services.AddDbContext<DBConnection>(options => options.UseMySQL(Configuration.GetConnectionString("MarketDatabase")));


            services.AddCors(options =>
            {
                options.AddDefaultPolicy(
                builder =>
                {
                    builder.AllowAnyHeader().AllowAnyMethod().AllowAnyOrigin();
                });

            });


            services.Configure<CookiePolicyOptions>(options =>
            {

                options.CheckConsentNeeded = context => true;
                options.MinimumSameSitePolicy = SameSiteMode.None;
            });

            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(options =>
            {
                options.RequireHttpsMetadata = false ;
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    
                    ValidateIssuer = true,
                   
                    ValidIssuer = AuthToken.ISSUER,
         
                    ValidateAudience = true,
                  
                    ValidAudience = AuthToken.AUDIENCE,
                  
                    ValidateLifetime = true,
              
                    IssuerSigningKey = AuthToken.GetSymmetricSecurityKey(),
                 
                    ValidateIssuerSigningKey = true,
                };
            });


            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);
        }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                app.UseHsts();
            }

            app.Use((context, next) =>
            {

                if (string.IsNullOrWhiteSpace(context.Request.Cookies["Token"]))
                {
                    context.Request.Headers["Authorization"] = "";
                }

                context.Request.Headers["Authorization"] = "Bearer " + context.Request.Cookies["Token"];
                return next.Invoke();
            });

            app.UseCors();
            loggerFactory.AddSerilog();

            app.UseAuthentication();

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseCookiePolicy();


            app.UseMvc(routes =>
            { 
                routes.MapRoute(
                name: "default",
                 template: "{controller=Home}/{action=Index}/{id?}");
            });

        }
    }
}
