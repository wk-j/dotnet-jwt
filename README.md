## JWT Authorize

Simple JWT authentication wrapper

## Installation

```
dotnet add BC.JwtAuthorize
```

## Usage

Open namespace

```csharp
using JwtAuthorize;
using JwtAuthorize.Controllers;
```

Implement `IJwtTokenRequestHandler`

```csharp
public class JwtRequestHandler : IJwtTokenRequestHandler {
    public string SecretKey { set; get; } = "abcdefghijklmnopqrstuvwzyz";
    public int Expire { set; get; } = 30;
    public HandleResult HandleRequest(JwtTokenRequest request) {
        // change this implementation
        if (request.User == "wk" && request.Password == "wk") {
            return new HandleResult { Success = true };
        }
        return new HandleResult { Success = false, Message = "Invalid user / password" };
    }
}
```

Register to service collections

```csharp
public void ConfigureServices(IServiceCollection services) {
    var handler = new JwtRequestHandler();
    services.AddSingleton<IJwtTokenRequestHandler>(handler);
    services.AddJwtAuthentication(handler.SecretKey)
}
```

## Test with REST client

Get JWT token

```
POST http://localhost:5000/api/authen/requestToken
Content-Type: application/json
{ 
    "user": "wk",
    "password": "wk"
}
```

Call API

```
GET http://localhost:5000/api/hello/hi
Accept: application/json
Authorization: Bearer <ReturnToken>
```