using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using Swagger_aspNetCore.Models;
using Swagger_aspNetCore.Services;
using System;
using System.IO;
using System.Reflection;

namespace Swagger_aspNetCore
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
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo
                {
                    Title = "WebApi Swagger",
                    Version = "v1",
                    Contact = new OpenApiContact
                    {
                        Name = "Christ",
                        Email = "christzhangowner@gmail.com",
                        Url = new Uri("https://github.com/Nightstars")
                    }
                }); 
                // 获取xml文件名
                var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                // 获取xml文件路径
                var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
                // 添加控制器层注释，true表示显示控制器注释
                c.IncludeXmlComments(xmlPath, true);

                #region 启用swagger验证功能
                c.AddSecurityRequirement(
                    new OpenApiSecurityRequirement()
                    {
                        {
                            new OpenApiSecurityScheme
                            {
                                Reference = new OpenApiReference {
                                    Type = ReferenceType.SecurityScheme,
                                    Id = "dotnet5 Web API"
                                }
                            },
                            new string[] { }
                        }
                    }
                );

                c.AddSecurityDefinition("dotnet5 Web API", new OpenApiSecurityScheme
                {
                    Description = "Basic Authorization in Swagger",
                    Name = "Authorization",
                    Scheme= "basic",
                    In = ParameterLocation.Header,
                    Type = SecuritySchemeType.Http
                });
                #endregion

            });

            services.AddAuthentication("BasicAuthentication")
                  .AddScheme<AuthenticationSchemeOptions, BasicAuthenticationHandler>("BasicAuthentication", null);

            services.AddControllers();
            services.AddScoped<IUserService, UserService>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "dotnet5 Web API v1");
            });           

            app.UseHttpsRedirection();          

            app.UseRouting();    

            // 启用认证中间件
            app.UseAuthentication();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{Controller=swagger}/{action=Index}/{id?}"
                );
            });
        }
    }
}
