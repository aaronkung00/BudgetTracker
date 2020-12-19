using AaronKung.BudgetTracker.Core.Entities;
using AaronKung.BudgetTracker.Core.RepositoryInterfaces;
using AaronKung.BudgetTracker.Core.ServiceInterfaces;
using AaronKung.BudgetTracker.Infrastructure.Data;
using AaronKung.BudgetTracker.Infrastructure.Repositories;
using AaronKung.BudgetTracker.Infrastructure.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AaronKung.BudgetTracker.API
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
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "AaronKung.BudgetTracker.API", Version = "v1" });
            });



            // Add Db Service
            services.AddDbContext<BudgetTrackerDbContext>(options =>
             options.UseSqlServer(Configuration.GetConnectionString(("BudgetTrackerDbConnection"))));

            // Dependency Injections
            services.AddScoped<IAsyncRepository<User>, EF_Repository<User>>();
            services.AddScoped<IUserService,UserService>();

            services.AddScoped<IAsyncRepository<Income>, EF_Repository<Income>>();
            services.AddScoped<IIncomeService, IncomeService>();

            services.AddScoped<IAsyncRepository<Expenditure>, EF_Repository<Expenditure>>();
            services.AddScoped<IExpenseService,ExpenseService>();


        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "AaronKung.BudgetTracker.API v1"));
            }

            app.UseCors(builder =>
            {
                builder.WithOrigins(Configuration.GetValue<string>("clientSPAUrl")).AllowAnyHeader()
                    .AllowAnyMethod()
                    .AllowCredentials();
            });

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
