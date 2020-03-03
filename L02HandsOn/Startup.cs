using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;

namespace Lesson2_HandsOn
{
    public class Startup
    {
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.Run(async (context) =>
            {
                string name = "";
                if(context.Request.Cookies["name"] != null) {
                    name = context.Request.Cookies["name"];
                }
                foreach (var query in context.Request.Query)
                {
                    if(query.Key == "name") {
                        name = query.Value;
                    }
                }
                if(name != "") {
                    DateTime now = DateTime.Now;
                    DateTime expires = now + TimeSpan.FromMinutes(1);
                    context.Response.Cookies.Append(   
                        "name",
                        name,
                        new CookieOptions{
                            Path = "/",
                            HttpOnly = false,
                            Secure = false,
                            Expires = expires
                        }
                    );
                } else {
                    name = "Bryan Top";
                }
               

                await context.Response.WriteAsync(name);
            });
        }
    }
}
