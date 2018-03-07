using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;

namespace JwtAuthorize.Controllers {

    [Route("api/[controller]/[action]")]
    public class AuthenController : ControllerBase {
        private IJwtTokenRequestHandler handler = null;
        public AuthenController(IJwtTokenRequestHandler handler) {
            this.handler = handler;
        }

        public ObjectResult RequestToken([FromBody] JwtTokenRequest request) {
            var result = handler.HandleRequest(request);
            if (result.Success) {
                var claim = new[] { new Claim(ClaimTypes.Name, request.User) };
                var bytes = Encoding.UTF8.GetBytes(this.handler.SecretKey);
                var key = new SymmetricSecurityKey(bytes);
                var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
                var token = new JwtSecurityToken(
                    issuer: "bcircle.co.th",
                    audience: "bcircle.co.th",
                    claims: claim,
                    expires: DateTime.Now.AddMinutes(handler.Expire == 0 ? 30 : handler.Expire),
                    signingCredentials: creds
                );
                return Ok(new {
                    Token = new JwtSecurityTokenHandler().WriteToken(token)
                });
            } else {
                return BadRequest(result.Message);
            }
        }
    }
}