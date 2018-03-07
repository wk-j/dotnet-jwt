using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;

namespace DotNetJwt {
    public class TokenRequest {
        public string User { set; get; }
        public string Password { set; get; }
    }

    [Route("api/[controller]/[action]")]
    public class AuthenController : ControllerBase {
        private string SecurityKey = "112233445566778899";

        [HttpPost]
        public IActionResult RequestToken([FromBody] TokenRequest request) {
            if (request.User == "wk") {

                var claims = new[] {
                    new Claim(ClaimTypes.Name, request.User)
                };

                var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(SecurityKey));
                var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
                var token = new JwtSecurityToken(
                    issuer: "bcircle.co.th",
                    audience: "bcircle.co.th",
                    claims: claims,
                    expires: DateTime.Now.AddMinutes(30),
                    signingCredentials: creds
                );
                return Ok(
                    new {
                        Token = new JwtSecurityTokenHandler().WriteToken(token)
                    }
                );
            }
            return BadRequest("Could not verify username and password");
        }
    }
}