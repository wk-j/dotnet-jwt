#! "netcoreapp2.2"
#r "nuget:Newtonsoft.Json,12.0.1"

using System.Net.Http;
using System.Net.Http.Headers;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

var request = new {
    user = "admin",
    password = "admin"
};


async Task<string> GetToken() {
    var content = new StringContent(JsonConvert.SerializeObject(request), Encoding.UTF8, "application/json");

    using (var client = new HttpClient()) {

        client.DefaultRequestHeaders
          .Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

        var result = await client.PostAsync("http://localhost:5000/api/authen/requestToken", content);

        var body = await result.Content.ReadAsStringAsync();
        Console.WriteLine(result.StatusCode);

        dynamic response = JObject.Parse(body);
        var token = response.token;
        return token;
    }
}

async Task Hi() {
    using (var client = new HttpClient()) {
        var token = await GetToken();

        client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        client.DefaultRequestHeaders.Authorization =
            new AuthenticationHeaderValue("Bearer", token);

        var response = await client.GetAsync("http://localhost:5000/api/hello/hi");
        var body = await response.Content.ReadAsStringAsync();

        Console.WriteLine(response.StatusCode);
        Console.WriteLine(body);
    }
}

await Hi();