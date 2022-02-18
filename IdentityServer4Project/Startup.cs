using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace IdentityServer4Project
{
    public class Startup
    {
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddIdentityServer()
                .AddInMemoryClients(Config.GetClients())
                .AddInMemoryApiScopes(Config.Scopes)

                //.AddInMemoryIdentityResources(Config.GetIdentityResources())
                //.AddInMemoryApiResources(Config.GetApiResources())
                //.AddTestUsers(new List<TestUser>())
                
                .AddDeveloperSigningCredential();

            //services.AddCors(options =>
            //{
            //    options.AddPolicy("CorsPolicy", corsBuilder =>
            //    {
            //        corsBuilder.AllowAnyHeader()
            //        .AllowAnyMethod()
            //        .SetIsOriginAllowed(origin => origin == "http://localhost:7000")
            //        .AllowCredentials();
            //    });
            //});
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseRouting();
            //app.UseCors("CorsPolicy");
            app.UseIdentityServer(); 

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapGet("/", async context =>
                {
                    await context.Response.WriteAsync("Hello World!");
                });
            });
        }
    }
}
