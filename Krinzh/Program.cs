using System.Net;
using Microsoft.EntityFrameworkCore;
using NodaTime;
using IdentityModel.Client;
using Npgsql.EntityFrameworkCore.PostgreSQL.NodaTime;
using Krinzh.Models;
using Krinzh.Contexts;
using Krinzh.Controllers;


var builder = WebApplication.CreateBuilder(

    new WebApplicationOptions() 
    {
        Args = args,
        ApplicationName = typeof(Program).Assembly.FullName,
        ContentRootPath = Directory.GetCurrentDirectory(),
        EnvironmentName = Environments.Staging,
        WebRootPath = "Public"
    }
);
var connectionString = Environment.GetEnvironmentVariable("POSTGRES_CONN_STRING");

// builder.Services.AddDbContext<PostgresContext>(opt =>
// {
//     opt.UseNpgsql(connectionString, o => o.UseNodaTime());
// });



// Add services to the container.
builder.Services.AddGrpc();
builder.Services.AddSignalR();
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddHttpsRedirection(options =>
{
    options.RedirectStatusCode = (int)HttpStatusCode.PermanentRedirect;
    options.HttpsPort = 443;
});

var app = builder.Build();


// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

   



app.UseHsts();
app.UseHttpsRedirection(); 
app.UseStaticFiles();
app.UseRouting();
app.UseGrpcWeb();
app.UseAuthorization();
app.MapControllers();
app.MapHub<TwitchHub>("/twitch-api");


app.Run();