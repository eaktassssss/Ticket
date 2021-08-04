using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;
using Ticket.Business.Abstract;
using Ticket.Business.Concrete;
using Ticket.Configuration.Mongo;
using Ticket.Core.Caching.Redis;
using Ticket.DataAccess.Abstract;
using Ticket.DataAccess.Concrete;
using Ticket.Entities.Context;
using Ticket.Helper;
using Ticket.Extensions.Redis;
using Microsoft.EntityFrameworkCore;
using Ticket.Publishers.Tickets;
using Ticket.MessageBroker.RabbitMQ;
using FluentValidation.AspNetCore;
using Ticket.Validators;
using Ticket.Core.Mapping.AutoMapper;

namespace Ticket.Web
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
            services.AddControllersWithViews().AddRazorRuntimeCompilation();
            services.AddFluentValidation(f => f.RegisterValidatorsFromAssemblyContaining<DelegateObject>());
            services.AddDbContext<TicketContext>(options => options.UseSqlServer(Configuration.GetConnectionString("TicketConString")));
            services.AddScoped<IUserService, UserManager>();
            services.AddScoped<IUserDataAccess, UserDataAccess>();
            services.AddScoped<IRoleService, RoleManager>();
            services.AddScoped<IRoleDataAccess, RoleDataAccess>();
            services.AddScoped<ITicketDataAccess, TicketDataAccess>();
            services.AddScoped<ITicketService, TicketManager>();
            services.AddScoped<ICustomerDataAccess, CustomerDataAccess>();
            services.AddScoped<ICustomerService, CustomerManager>();
            services.AddScoped<IHashService, HashService>();
            services.AddScoped<IUserRoleDataAccess, UserRoleDataAccess>();
            services.AddScoped<IUserRoleService, UserRoleManager>();
            services.AddScoped<IRedisCacheService, RedisCacheService>();
            services.AddScoped<IMessagePublisher, TicketMessagePublisher>();
            services.AddScoped<IRedisCacheService, RedisCacheService>();
            services.AddScoped<IRabbitMQMessageBroker, RabbitMQMessageBroker>();
            services.AddScoped<IMongoConfiguration, MongoConfiguration>();
            services.Configure<MongoConfiguration>(Configuration.GetSection("MongoConfiguration"));
            services.AddSingleton<IMongoConfiguration>(x => x.GetRequiredService<IOptions<MongoConfiguration>>().Value);
            services.AddRedisDistributedCacheExtension(Configuration.GetSection("Redis").GetValue<string>("ServerName"));
            services.AddAutoMapper(x =>
            {
                x.AddProfile(new MappingProfile());
            });
        }


        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");

                app.UseHsts();
            }
            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                   name: "default",
                   pattern: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
