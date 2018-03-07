using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DotNetJwt {

    [Route("api/[controller]/[action]")]
    public class HelloController : ControllerBase {

        [HttpGet]
        [Authorize]
        [Produces("applicaton/json")]
        public dynamic Hi() {
            return new {
                Message = "Hello, world!"
            };
        }
    }
}