using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace HelloWeb.Controllers {
    [ApiController]
    [Route("api/[controller]/[action]")]
    public class Hello : ControllerBase {

        [HttpGet, Authorize]
        public dynamic Hi() {
            return new {
                A = 100,
                B = 200
            };
        }
    }
}