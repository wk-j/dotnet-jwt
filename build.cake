var name = "JwtAuthorize";
var project = $"src/{name}/{name}.csproj";
var repo = "/Users/wk/NuGet";

Task("Pack").Does(() => {
    DotNetCorePack(project, new DotNetCorePackSettings {
        OutputDirectory = "publish",
        Configuration = "release"
    });
    DotNetCorePack(project, new DotNetCorePackSettings {
        OutputDirectory = repo, 
        Configuration = "release"
    });
});

Task("Publish-Nuget")
    .IsDependentOn("Pack")
    .Does(() => {
        var npi = EnvironmentVariable("npi");
        var nupkg = new DirectoryInfo("publish").GetFiles("*.nupkg").LastOrDefault();
        var package = nupkg.FullName;
        NuGetPush(package, new NuGetPushSettings {
            Source = "https://www.nuget.org/api/v2/package",
            ApiKey = npi
        });
});

var target = Argument("target", "default");
RunTarget(target);