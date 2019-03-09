using System.Collections.Generic;

namespace JwtAuthorize.Service {
    public class Context {
        public List<User> Users { set; get; } = new List<User> {
            new User { UserName = "admin", Password = "admin" }
        };
    }
}
