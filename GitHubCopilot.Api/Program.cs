var builder = WebApplication.CreateBuilder(args);


var app = builder.Build();


app.UseHttpsRedirection();

app.MapGet("/info", () => "Hello Copilot");

app.Run();
