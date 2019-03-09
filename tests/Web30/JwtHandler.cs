using JwtAuthorize.Controllers;

namespace HelloWeb {
    public class JwtHandler : IJwtTokenRequestHandler {

        public string SecretKey { get; } = "11111111111111111111111111111111111111111111111111111111111111111";
        public int Expire { get; } = 30;

        public HandleResult HandleRequest(JwtTokenRequest request) {
            if (request.User == "admin" && request.Password == "admin") {
                return new HandleResult { Success = true };
            } else {
                return new HandleResult { Success = false, Message = "Invalid user/password" };
            }
        }
    }
}
