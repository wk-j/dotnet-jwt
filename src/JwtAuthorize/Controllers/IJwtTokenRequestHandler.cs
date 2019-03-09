namespace JwtAuthorize.Controllers {
    public interface IJwtTokenRequestHandler {
        string SecretKey { get; }
        int Expire { get; }
        HandleResult HandleRequest(JwtTokenRequest request);
    }
}