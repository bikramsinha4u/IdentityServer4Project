using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens;

namespace WebApiProject
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
            services.AddControllers();
            services
                .AddMvcCore()
                .AddAuthorization();

            //services.AddCors(options =>
            //{
            //    options.AddPolicy("AllRequests", builder =>
            //    {
            //        builder.AllowAnyHeader()
            //        .AllowAnyMethod()
            //        .SetIsOriginAllowed(origin => origin == "https://localhost:44357")
            //        .AllowCredentials();
            //    });
            //});

            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(options =>
            {
                options.Authority = "http://localhost:5000";
                options.RequireHttpsMetadata = false;
                //options.Audience = "api1";
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ////////////////////////////////////////////////////////
                    // The following made the difference.  
                    ////////////////////////////////////////////////////////
                    ValidateAudience = false,
                    ValidateIssuer = false
                };
            });

            services.AddAuthorization(options =>
            {
                options.AddPolicy("ApiScope", policy =>
                {
                    policy.RequireAuthenticatedUser();
                    policy.RequireClaim("scope", "api1");
                });
            });

        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            //app.UseCors("AllRequests");
            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization(); // put UseAuthorization after UseAuthentication

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers()
                .RequireAuthorization("ApiScope");

            });
        }
    }
}
