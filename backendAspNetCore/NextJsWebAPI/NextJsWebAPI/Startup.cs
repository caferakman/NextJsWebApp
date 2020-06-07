using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.SpaServices;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.Authorization;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens;
using Microsoft.EntityFrameworkCore.SqlServer;
using Microsoft.EntityFrameworkCore;
using NextJsWebAPI.Models;
using NextJsWebAPI.Entities;
using Microsoft.AspNetCore.Identity;
using NextJsWebAPI.Services;
using AutoMapper;
using Microsoft.EntityFrameworkCore.Design;
using System.Text;

namespace NextJsWebAPI
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
            services.AddDbContext<DataContext>(options =>
                options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));

            services.AddCors(options =>
            {
                options.AddPolicy("ReactCorsPolicy", builder =>
                {
                    builder
                      .AllowAnyHeader()
                      .AllowAnyMethod()
                      .AllowCredentials()
                      .WithOrigins("http://localhost:3000");
                });
            });

            services.AddAutoMapper(typeof(Startup));

            //uses: Microsoft.AspNetCore.Mvc.NewtonsoftJson
            services.AddControllers().AddNewtonsoftJson();


            services.AddAuthentication(o =>
            {
                o.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                o.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                o.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(opt =>
                {
                    opt.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuerSigningKey = true,
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("KeyForSignInSecret@1234")),
                        ValidateAudience = true,
                        ValidAudience = "http://localhost:5000",
                        ValidateIssuer = true,
                        ValidIssuer = "http://localhost:5000",
                        ValidateLifetime = true
                    };
                });

            /*services.AddIdentity<User, Role>(options =>
            {
                options.User.RequireUniqueEmail = false;
            })
                .AddEntityFrameworkStores<DataContext>()
                .AddDefaultTokenProviders();*/

            services.AddScoped<IUserService, UserService>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            //app.UseMiddleware<ErrorHandlingMiddleware>(); //for handling exceptions

            if (env.IsDevelopment())
            {
                //app.UseDeveloperExceptionPage();
            }

            
            //app.UseStaticFiles();
            //app.UseSpaStaticFiles();

            app.UseRouting();
            app.UseCors("ReactCorsPolicy");

            app.UseAuthentication();
            app.UseAuthorization();

            //app.UseHttpsRedirection();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

        }
    }
}
