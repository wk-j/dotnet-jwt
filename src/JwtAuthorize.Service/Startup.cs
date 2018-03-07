using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using JwtAuthorize;
using JwtAuthorize.Controllers;

namespace JwtAuthorize.Service {
    public class JwtRequestHandler : IJwtTokenRequestHandler {
        public string SecretKey { set; get; } = "abcdefghijklmnopqrstuvwzyz";
        public int Expire { set; get; } = 30;
        public HandleResult HandleRequest(JwtTokenRequest request) {
            if (request.User == "wk" && request.Password == "wk") {
                return new HandleResult { Success = true };
            }
            return new HandleResult { Success = false, Message = "Invalid user / password" };
        }
    }

    public class Startup {
        public Startup(IConfiguration configuration) {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services) {
            var handler = new JwtRequestHandler();
            services.AddSingleton<IJwtTokenRequestHandler>(handler);
            services.AddJwtAuthentication(handler.SecretKey);

            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);
        }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env) {
            if (env.IsDevelopment()) {
                app.UseDeveloperExceptionPage();
            } else {
                app.UseHsts();
            }

            // app.UseHttpsRedirection();

            app.UseAuthentication();
            app.UseMvc();
        }
    }
}
