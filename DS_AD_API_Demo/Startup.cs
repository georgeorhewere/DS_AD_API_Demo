using DS_AD_API_Demo.Repository;
using DS_AD_API_Demo.Service;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace DS_AD_API_Demo
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

            services.AddDbContext<DSDbContext>(options =>
                                        options.UseSqlServer(
                                                Configuration.GetConnectionString("DocstreamDatabase")), ServiceLifetime.Scoped);

            services.AddIdentity<IdentityUser, IdentityRole>(options =>
            {
                options.Password.RequiredLength = 8;
                options.Password.RequireNonAlphanumeric = false;

            }
                                            )
                                        .AddEntityFrameworkStores<DSDbContext>()
                                        .AddDefaultTokenProviders();          

            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(options =>
            {
                options.Authority = "https://login.microsoftonline.com/d75facc4-6467-47d9-ac73-f8fcb4ba0845";
                // options.Audience = "9eba0b88-c9e1-4471-841e-5191a80fe014";
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = false,
                    ValidAudience = "api://9eba0b88-c9e1-4471-841e-5191a80fe014",
                    ValidIssuer = "https://sts.windows.net/d75facc4-6467-47d9-ac73-f8fcb4ba0845/"
                };
            });

            services.AddAuthorization(options =>
            {
                options.AddPolicy("Access_DocStream_As_A_User", policy => policy.Requirements.Add(new HasDSADScope("Access_DocStream_As_A_User", "https://sts.windows.net/d75facc4-6467-47d9-ac73-f8fcb4ba0845/")));
            });

            // register the scope authorization handler
            services.AddSingleton<IAuthorizationHandler, HasDSADScopeHandler>();


            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);
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
                app.UseHsts();
            }
            
            app.UseAuthentication();
            // app.UseOpenIdConnectAuthentication();
            //app.Us();

            app.UseHttpsRedirection();
            app.UseMvc();
        }
    }
}
