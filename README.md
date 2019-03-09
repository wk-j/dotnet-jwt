## JWT Authorize

[![NuGet](https://img.shields.io/nuget/v/wk.JwtAuthorize.svg)](https://www.nuget.org/packages/wk.JwtAuthorize)

Simple JWT authentication wrapper for .NET MVC Core

- [x] 2.1
- [ ] 3.0

## Installation

```bash
dotnet add package wk.JwtAuthorize
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

Register `JwtRequestHandler` to service collections

```csharp
public void ConfigureServices(IServiceCollection services) {
    var handler = new JwtRequestHandler();
    services.AddSingleton<IJwtTokenRequestHandler>(handler);
    services.AddJwtAuthentication(handler.SecretKey)
}
```

Append `[Authorize]` attribute to REST method

```csharp
[Route("api/[controller]/[action]")]
public class HelloController : ControllerBase {
    [HttpGet, Authorize]
    public dynamic Hi() {
        return new {
            Message = "Hello, world!"
        };
    }
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