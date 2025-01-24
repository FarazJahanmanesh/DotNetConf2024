using Microsoft.AspNetCore.Mvc;
using Octokit;

var builder = WebApplication.CreateBuilder(args);

var appName = "demo";
var app = builder.Build();


app.UseHttpsRedirection();

app.MapGet("/info", () => "Hello Copilot");
app.MapGet("/callback", () => "you may close this window");

app.MapPost("/", async ([FromHeader(Name = "X-GitHub-Token")] string gitHubToken,
        [FromBody] Payload payload) =>
    {
        //process the GitHub Copilot request
        Console.WriteLine("payload");

        GitHubClient OctoKitClient = new GitHubClient(new ProductHeaderValue(appName))
        {
            Credentials = new Credentials(gitHubToken)
        };

        User user = await OctoKitClient.User.Current();

        Console.WriteLine($"User : {user.Login}");

        payload.Messages.Insert(0, new Message
        {
            Role = "system",
            Content = $"start every response with user's name , which is @{user.Login}"
        });

        HttpClient httpClient = new HttpClient();
        httpClient.DefaultRequestHeaders.Authorization =
        new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", gitHubToken);
        payload.Stream = true;

        HttpResponseMessage copilotLLMResponse = await httpClient.PostAsJsonAsync
        ("https://api.githubcopilot.com/chat/completions ", payload);

        var responseStream = await copilotLLMResponse.Content.ReadAsStreamAsync();
        return Results.Stream(responseStream,"application/json");
    });

app.Run();

internal class Message
{
    public required string Role { get; set; }
    public required string Content { get; set; }
}
internal class Payload
{
    public bool Stream { get; set; }
    public List<Message> Messages { get; set; }
}