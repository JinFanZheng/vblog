using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MySql.Data.MySqlClient;
using NPoco;
using VBlog.Services.Implements;
using VBlog.Services.Interfaces;
using Swashbuckle.AspNetCore.Swagger;

namespace VBlog
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
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);

            #region Ioc 
            var connStr = Configuration.GetConnectionString("DefaultConnectionString");
            services.AddScoped<IDatabase>((provider) =>
            {
                return new Database(connStr, DatabaseType.MySQL, MySqlClientFactory.Instance);
            });
            services.AddScoped<IUserService, UserService>();
            #endregion

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new Info
                {
                    Title = "VBlog API V1.0",
                    Version = "v1.0",
                    License = new License
                    {
                        Name = "MIT",
                        Url = ""
                    },
                    Contact=new Contact {
                        Email="zheng_jinfan@126.com",
                        Name="VanZ",
                        Url="https://www.zhengjinfan.cn"
                    }
                });
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseHsts();
            }
            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "VBlog API Document V1.0");
            });
            app.UseHttpsRedirection();
            app.UseMvc();
        }
    }
}
