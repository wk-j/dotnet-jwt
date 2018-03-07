using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DotNetJwt {

    [Route("api/[controller]/[action]")]
    public class HelloController : ControllerBase {

        [HttpGet]
        [Authorize]
        public dynamic Hi() {
            return new {
                Message = "Hello, world!"
            };
        }

        [HttpGet]
        [Authorize]
        public dynamic Who() {
            var name = this.User.Identity.Name;
            return new {
                Name = name
            };
        }
    }
}