using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.EntityFrameworkCore;
using WMS.UserManagement.DTO;
using WMS.UserManagement.Model.Db;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using WMS.UserManagement.Model.Notifications;
using WMS.UserManagement.Model.Services;

namespace WMS.UserManagement
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
            services.AddControllers().AddNewtonsoftJson(options =>
                options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore
            );
            services.AddDbContext<WarehouseManagementSystemDataContext>(o => o.UseSqlServer(Configuration.GetConnectionString("WarehouseManagementSystemDB")));
            services.AddIdentity<User, Role>()
                .AddEntityFrameworkStores<WarehouseManagementSystemDataContext>()
                .AddDefaultTokenProviders();
            services.AddScoped<RoleManager<Role>>();

            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(options =>
            {
                options.SaveToken = true;
                options.RequireHttpsMetadata = false;
                options.TokenValidationParameters = new TokenValidationParameters()
                {
                    ValidateIssuer = false,
                    ValidateAudience = false,
                    ValidateLifetime = true,
                    //ValidIssuer = Configuration["Authentication:ValidIssuer"],
                    //ValidAudience = Configuration["Authentication:ValidAudience"],
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Configuration["Authentication:Secret"]))
                };
            });
            
            var emailConf = new EmailConfiguration
            {
                Host = Configuration["EmailConfiguration:Host"],
                Username = Configuration["EmailConfiguration:Username"],
                Password = Configuration["EmailConfiguration:Password"],
                Port = Configuration.GetValue<int>("EmailConfiguration:SmtpPort")
            };

            services.AddScoped<IEmailConfiguration, EmailConfiguration>();
            services.AddScoped<IUserService, UserService>();
            services.AddSingleton(emailConf);
        }


        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseCors(builder => builder
                .AllowAnyOrigin()
                .AllowAnyMethod()
                .AllowAnyHeader()
            );

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthentication();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
