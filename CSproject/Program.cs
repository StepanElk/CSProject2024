using CSproject.Application.Hubs;
using CSproject.Domain;
using CSproject.Infrastructure;
using Ninject;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddMemoryCache();

//DI контейнер ASP 
builder.Services.AddTransient(typeof(UserRepository), typeof(UserRepository));
builder.Services.AddTransient(typeof(EFContext), typeof(EFContext));
builder.Services.AddTransient(typeof(ConnectionsRepository), typeof(ConnectionsRepository));
builder.Services.AddTransient(typeof(MessagesRepository), typeof(MessagesRepository));


builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(policy =>
    {
        policy.WithOrigins("http://localhost:3000")
        .AllowAnyHeader()
        .AllowAnyMethod()
        .AllowCredentials();
    });
});

builder.Services.AddSignalR();

var app = builder.Build();

app.UseCors();
app.MapHub<ChatHub>("/chat");

app.Run();
    