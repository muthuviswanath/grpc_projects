using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IdentityServer
{
    public class Startup
    {

        public void ConfigureServices(IServiceCollection services) {
            services.AddIdentityServer()
                    .AddInMemoryClients(Config.Clients)
                    .AddInMemoryApiScopes(Config.ApiScopes)
                    .AddDeveloperSigningCredential();
        }
        // This method gets called by the runtime. Use this method to add services to the container.
       

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
           

           app.UseRouting();
            app.UseIdentityServer();
     

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapGet("/", async context =>
                {
                    await context.Response.WriteAsync("Hey Guys");
                }
                );
            });
        }
    }
}
