using auth_demo_gateway_api.Consul;
using auth_demo_gateway_api.HttpClients;
using auth_demo_gateway_api.Policies;
using auth_demo_gateway_api.Services;
using auth_demo_gateway_api.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace auth_demo_gateway_api
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
            services.AddMvc()
                .SetCompatibilityVersion(CompatibilityVersion.Version_2_2);

            services.AddCors(options =>
            {
                options.AddPolicy("AllowAll", builder =>
                {
                    builder.AllowAnyHeader();
                    builder.AllowAnyMethod();
                    builder.AllowAnyOrigin();
                });
            });

            services.RegisterConsulServices(Configuration.GetConsulConfig());
            services.AddHttpContextAccessor();
            services.AddAuthorization(options =>
            {
                options.AddPolicy(nameof(Policy.Administrator),
                    policy => policy.Requirements.Add(new AdministratorRequirement()));
            });

            services.AddHttpClient<InternalHttpClient>();

            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IAuthorizationHandler, Administrator>();
            services.AddScoped<IUserColourService, UserColoursService>();
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
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseCors("AllowAll");
            app.UseMvc();
        }
    }
}
