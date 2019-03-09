using System.Linq;
using JwtAuthorize.Controllers;

namespace JwtAuthorize.Service {
    public class JwtHandler : IJwtTokenRequestHandler {

        private Context context;

        public string SecretKey { set; get; } = "abcdefghijklmnopqrstusssssssssssssssssssssssssssssss";
        public int Expire { set; get; } = 30;

        public JwtHandler(Context context) {
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
}
