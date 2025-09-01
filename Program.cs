// See https://aka.ms/new-console-template for more information
using HttpClientApp;
using Microsoft.EntityFrameworkCore;
using NuGet.Protocol.Core.Types;
using System.Net.Http.Headers;
using System.Text.Json;
using WebAPISandwich.Model;
using Microsoft.AspNetCore.Identity;

var builder = WebApplication.CreateBuilder(args);

// For Identity
builder.Services.AddAuthorization();

// For Entity Framework
builder.Services.AddIdentityApiEndpoints<IdentityUser>()
    .AddEntityFrameworkStores<AppDbContext>();

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddConnections(); 
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("AppDbConnection")));
builder.Services.AddDbContext<SandwichContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

app.MapIdentityApi<IdentityUser>(); //Its doing the same work as app.UseAuthentication() and app.UseAuthorization() by itself through extension method. //mapping identity users

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