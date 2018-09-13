using AutoMapper;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Text;
using ToDoServer.Data;
using ToDoServer.Models;
using ToDoServer.Persistence.RepositoriesI;
using ToDoServer.Persistence.RepositoriesImpl;

namespace ToDoServer
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
            services.AddMvc();

            services.AddAutoMapper();

            services.AddDbContext<ToDoListContext>();

            services.AddIdentity<ApplicationUser, ApplicationRole>(
                options =>
                {
                    options.Password.RequireDigit = false;
                    options.Password.RequiredLength = 1;
                    options.Password.RequiredUniqueChars = 0;
                    options.Password.RequireLowercase = false;
                    options.Password.RequireUppercase = false;
                    options.Password.RequireNonAlphanumeric = false;
                })
                .AddEntityFrameworkStores<ToDoListContext>();

            services.AddScoped<IUnitOfWork, UnitOfWork>();

            services.AddCors(options =>
            {
                options.AddPolicy("AllowAll",
                      builder =>
                      {
                          builder.AllowAnyOrigin()
                                 .AllowAnyHeader()
                                 .AllowAnyMethod()
                                 .AllowCredentials();
                      });
            });

            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;

            }).AddJwtBearer(options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateAudience = false,
                    //ValidAudience = "http://localhost:4200",
                    ValidateIssuer = false,
                    //ValidIssuer = "http://localhost:4200",

                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("JWT SECRET KEY EXAMPLE")),

                    ValidateLifetime = true, //validate the expiration and not before values in the token
              
                    ClockSkew = TimeSpan.FromMinutes(15) //15 minute tolerance for the expiration date
                };

                //options.Authority = "http://localhost:53666";
                //options.Audience = "http://localhost:4200"; options.RequireHttpsMetadata = false;
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseStaticFiles();

            app.UseAuthentication();

            app.UseCors("AllowAll");

            app.UseMvc();

        }
    }
}
