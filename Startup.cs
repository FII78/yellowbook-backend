using System;
using System.Text;
using AutoMapper;
using FindIt.Backend.Entities;
using FindIt.Backend.Helpers;
using FindIt.Backend.Middleware;
using FindIt.Backend.Services;
using FindIt.Backend.Services.Implementations;
using FindIt.Backend.Services.Interfaces;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;

namespace FindIt.Backend
{
    public class Startup
    {
        public IConfiguration Configuration { get; }
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public void ConfigureServices(IServiceCollection services)
        {

            services.AddHttpClient();
            services.AddControllers().AddJsonOptions(x => x.JsonSerializerOptions.IgnoreNullValues = true);
            services.Configure<Settings>(options =>
                    {
                        options.ConnectionString = Configuration.GetSection("MongoConnection:ConnectionString").Value;
                        options.DatabaseName = Configuration.GetSection("MongoConnection:DatabaseName").Value;
                    });

            services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
            services.AddAuthentication(options =>
                    {
                        options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
                    })
                    .AddJwtBearer(options =>
                    {
                        options.TokenValidationParameters = new TokenValidationParameters
                        {
                            ValidateIssuer = false,
                            ValidateAudience = false,
                            ValidateLifetime = true,
                            ValidateIssuerSigningKey = true,

                            ValidIssuer = Configuration["JWT:Site"],
                            ValidAudience = Configuration["JWT:Audience"],
                            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Configuration["JWT:key"])),
                            ClockSkew = TimeSpan.Zero
                        };
                    });

            services.AddScoped<IAccountService, AccountService>();
             
              

        }

      
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {


   

            app.UseMiddleware<ErrorHandlerMiddleware>();
            app.UseDeveloperExceptionPage();
            app.UseStaticFiles();
            app.UseAuthentication();
            app.UseRouting();
            app.UseAuthorization();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
          
        }
    }
}
