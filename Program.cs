// See https://aka.ms/new-console-template for more information
using HttpClientApp;
using Microsoft.EntityFrameworkCore;
using NuGet.Protocol.Core.Types;
using System.Net.Http.Headers;
using System.Text.Json;
using WebAPISandwich.Model;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddConnections();
builder.Services.AddDbContext<SandwichContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();



await ShowSandwiches();


static async Task<List<HttpClientApp.Repository>> ProcessRepositoriesAsync(HttpClient client)
{

    await using Stream stream =
        await client.GetStreamAsync("https://localhost:7250/api/Sandwich");
    var repositories =
        await JsonSerializer.DeserializeAsync<List<HttpClientApp.Repository>>(stream);
    return repositories ?? new();// Initialize a new instance of List<Repository> class
                                 // which is empty and has default initial capacity.
}

static async Task ShowSandwiches()
{
    using (HttpClient client = new HttpClient())
    {
        client.DefaultRequestHeaders.Accept.Clear();// clear all previosu header data.


        var repositories = await ProcessRepositoriesAsync(client);

        foreach (var repository in repositories)
        {
            Console.WriteLine($"{repository.Id}  {repository.Name}    {repository.Price}");
        }
    }
}