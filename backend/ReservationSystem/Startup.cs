using System;
using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using ReservationSystem.DataAccess;
using ReservationSystem.DataAccess.Entities;
using ReservationSystem.DataAccess.Repositories;
using ReservationSystem.Services;

namespace ReservationSystem
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
            services.AddEndpointsApiExplorer();
            services.AddSwaggerGen(options =>
            {
                options.SwaggerDoc("v1", new OpenApiInfo
                {
                    Version = "v1",
                    Title = "Dormitory services and items reservation system",
                    Description = "API for managing dormitory services and items",
                });
            });

            services.AddDbContext<ReservationDbContext>(options => options.UseSqlServer("name=ConnectionStrings:Database"));
            services.AddIdentity<User, IdentityRole<Guid>>(options =>
                    options.Password = new PasswordOptions
                    {
                        RequireDigit = true,
                        RequiredLength = 8,
                        RequireLowercase = true,
                        RequireUppercase = false,
                        RequireNonAlphanumeric = false,
                    })
                .AddEntityFrameworkStores<ReservationDbContext>()
                .AddDefaultTokenProviders();

            ConfigureAuth(services);
            
            services.AddCors(options =>
            {
                options.AddPolicy("AllowAllHeaders",
                    policyBuilder =>
                    {
                        policyBuilder.AllowAnyOrigin()
                            .AllowAnyHeader()
                            .AllowAnyMethod()
                            .WithExposedHeaders("Authorization");
                    });
            });

            services.AddControllers();

            services.AddScoped<UsersRepository>();
            services.AddScoped<AuthService>();
            services.AddScoped<JwtService>();
            services.AddScoped<DormitoriesService>();
            services.AddScoped<UsersService>();
            services.AddScoped<ServicesService>();
            services.AddScoped<ReservationsService>();
            services.AddScoped<RoomsService>();
            services.AddScoped<ServicesRepository>();
            services.AddScoped<DormitoriesRepository>();
            services.AddScoped<ReservationsRepository>();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI(options => { options.SwaggerEndpoint("/swagger/v1/swagger.json", "v1"); });
            }

            app.UseCors("AllowAllHeaders");

            app.UseAuthentication();
            app.UseRouting();
            app.UseAuthorization();
            app.UseEndpoints(endpoints => { endpoints.MapControllers(); });
        }

        public virtual void ConfigureAuth(IServiceCollection services)
        {
            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(options =>
            {
                options.SaveToken = true;
                options.RequireHttpsMetadata = false;
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Configuration["JWT:Secret"])),
                    ValidateAudience = true,
                    ValidAudience = Configuration["JWT:ValidAudience"],
                    ValidateIssuer = true,
                    ValidIssuer = Configuration["JWT:ValidIssuer"],
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    ClockSkew = TimeSpan.Zero
                };
            });

        }
    }
}