using BankingApi.Authentication;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BankingApi.DAL;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using BankingApi.Services.Interfaces;
using BankingApi.Services.Implementation;

namespace BankingApi
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
            services.AddControllers();
            var a = Configuration.GetSection("ConnectionStrings").GetConnectionString("sqlConnection");
            var _jwtsetting = Configuration.GetSection("JWTSettings");
            

            services.AddDbContext<BankContext>(options=>options.UseSqlServer(Configuration.GetConnectionString("sqlConnection")));
            services.AddScoped<IUserService, UerService>();
            services.AddScoped<ITransaction, TransactionService>();
            services.Configure<JWTSettings>(_jwtsetting);

            var authKey = Configuration.GetValue<string>("JWTSettings:Seceretkey");
            services.AddAuthentication(auth => {             
                auth.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                auth.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(item=> {
                item.RequireHttpsMetadata = true;
                item.SaveToken = true;
                item.TokenValidationParameters = new TokenValidationParameters()
                {
                    ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(authKey)),
                ValidateIssuer =false,
                ValidateAudience =false              
                
                };
            });


            services.AddSwaggerGen(options=> {
                options.SwaggerDoc("api", new OpenApiInfo()
                {
                    Description = "Banking Api",
                    Title = "Customer",
                    Version = "1"

                });

            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            app.UseRouting();
            app.UseAuthentication();
            app.UseAuthorization();
            app.UseSwagger();
            app.UseSwaggerUI(options=> options.SwaggerEndpoint("api/swagger.json","Bank APiiii"));
            app.UseCors(builder =>
            {
                builder
                .AllowAnyOrigin()
                .AllowAnyMethod()
                .AllowAnyHeader();
            });

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
