using Membership.Models;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Dependency injection server
var connectionString = builder.Configuration.GetConnectionString("MembershipApp");
builder.Services.AddDbContext<Context>(x => x.UseSqlServer(connectionString));

// Cors origin
var CorsOriginAllow = "_myAllowSpecificOrigins";

builder.Services.AddCors(options =>
{
    options.AddPolicy(name: CorsOriginAllow, policy =>
    {
        policy.WithOrigins("http://localhost:3000");
    });
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors(CorsOriginAllow);

app.UseAuthorization();

app.MapControllers();

app.Run();
