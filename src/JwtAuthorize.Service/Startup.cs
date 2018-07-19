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

        private Context context;

        public string SecretKey { set; get; } = "abcdefghijklmnopqrstuvwzyz";
        public int Expire { set; get; } = 30;

        public JwtRequestHandler(Context context) {
            this.context = context;
        }

        public HandleResult HandleRequest(JwtTokenRequest request) {
            var user = context.Users.Where(x => x.UserName == request.User && x.Password == request.Password).FirstOrDefault();
            if (user != null) {
                return new HandleResult { Success = true };
            }
            return new HandleResult { Success = false, Message = "Invalid user / password" };
        }
    }

    public class User {
        public string UserName { set; get; }
        public string Password { set; get; }
    }

    public class Context {
        public List<User> Users { set; get; } = new List<User> {
            new User { UserName = "wk", Password = "wk" }
        };
    }

    public class Startup {
        public Startup(IConfiguration configuration) {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services) {
            var context = new Context();
            var handler = new JwtRequestHandler(context);
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
