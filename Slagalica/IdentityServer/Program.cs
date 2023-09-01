using IdentityServer.Data;
using IdentityServer.Extensions;
using IdentityServer.Services;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();

builder.Services.AddTransient<IAuthenticationService, AuthenticationService>(); //

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// We replaced this with builder.Services.ConfigureJWT()
// builder.Services.AddAuthentication();

var serviceProvider = builder.Services.ConfigurePersistence(builder.Configuration);

builder.Services.ConfigureIdentity();
builder.Services.ConfigureJWT(builder.Configuration);
builder.Services.ConfigureMiscellaneousServices();

// angular
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAngularApp",
        builder =>
        {
            builder.WithOrigins("http://localhost:4200")
                .AllowAnyHeader()
                .AllowAnyMethod();
        });
});

var app = builder.Build();

// Migrate latest database changes during startup
using (var scope = serviceProvider.BuildServiceProvider().CreateScope())
{
    var dbContext = scope.ServiceProvider
        .GetRequiredService<ApplicationContext>();
    
    // Here is the migration executed
    dbContext.Database.Migrate();
}

app.UseCors("AllowAngularApp");

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();