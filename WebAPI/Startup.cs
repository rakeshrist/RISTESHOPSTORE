using System.Net;
using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using WebAPI.Data;
using WebAPI.Extensions;
using WebAPI.Helpers;
using WebAPI.Interfaces;

namespace WebAPI
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
            var builder = new SqlConnectionStringBuilder(
                Configuration.GetConnectionString("Default")
            );
            builder.Password = Configuration.GetSection("DBPassword").Value;

            var connectionString = builder.ConnectionString;

            services.AddDbContext<StoreDataContext>(options =>
             options.UseSqlServer(connectionString));
            services.AddControllers().AddNewtonsoftJson();
            services.AddCors();//Allow cross platform requests
            services.AddAutoMapper(typeof(AutoMapperProfiles).Assembly);
            services.AddScoped<IUnitOfWork, UnitOfWork>();

            var secretKey = Configuration.GetSection("AppSettings:Key").Value;
            var key = new SymmetricSecurityKey(Encoding.UTF8.
                        GetBytes(secretKey));
            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
            .AddJwtBearer(opt =>
            {
                opt.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    ValidateIssuer = false,
                    ValidateAudience = false,
                    IssuerSigningKey = key
                };
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.ConfigureExceptionHandler(env);//Custom
            //app.UseMiddleware<ExceptionMiddleware>();
            
            app.UseRouting();

            app.UseHsts();//Newely added
            app.UseHttpsRedirection();//Newly added

            app.UseCors(p => p.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod());//Newly added

            app.UseAuthentication();//Newly added
            app.UseAuthorization();

            app.UseDefaultFiles();//Newly added 
            app.UseStaticFiles();//Newly added access wwwroot

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
