namespace JwtAuthorize.Controllers {
    public interface IJwtTokenRequestHandler {
        string SecretKey { set; get; }
        int Expire { set; get; }
        HandleResult HandleRequest(JwtTokenRequest request);
    }
}