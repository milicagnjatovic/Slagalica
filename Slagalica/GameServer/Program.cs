using GameServer.GrpcServices;
using GameServer.Hubs;
using GameServer.Repositories;
using WhoKnowsKnows.GRPC.Protos;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddGrpcClient<WhoKnowsKnowsProtoService.WhoKnowsKnowsProtoServiceClient>(
    options => options.Address = new Uri(builder.Configuration["GrpcSettings:WhoKnowsKnowsUrl"]));
builder.Services.AddScoped<WhoKnowsKnowsGrpcService>();

// Adding game repository
builder.Services.AddSingleton<IGameRepository>(new GameRepository());

// We are using SignalR for the server sockets
builder.Services.AddSignalR();

// CORS for angular
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAngularApp",
        builder =>
        {
            builder.WithOrigins("http://localhost:5274")
                .AllowAnyHeader()
                .AllowAnyMethod();
        });
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.MapHub<GameHub>("/gameServer");

app.UseAuthorization();

app.MapControllers();

app.Run();