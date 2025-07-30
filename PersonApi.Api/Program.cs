using Microsoft.EntityFrameworkCore;
using PersonApi.Application.Mappings;
using PersonApi.Domain.Repositories;
using PersonApi.Infrastructure.Data;
using PersonApi.Infrastructure.Repositories;
using PersonApi.Api.Configurations;
using PersonApi.Api.Services.Authentication;

var builder = WebApplication.CreateBuilder(args);
DotNetEnv.Env.Load();

builder.Configuration
    .AddEnvironmentVariables();

// Add DbContext (in-memory for tests)
builder.Services.AddDbContext<AppDbContext>(opt => opt.UseInMemoryDatabase("PersonDb"));

// Inject person repository
builder.Services.AddScoped<IPersonRepository, PersonRepository>();
// Inject auth service
builder.Services.AddScoped<IAuthService, AuthService>();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Add support to controllers
builder.Services.AddControllers();

builder.Services.AddAutoMapper(typeof(PersonProfile).Assembly);

builder.Services.AddJwtAuthentication(builder.Configuration, builder.Environment);

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
    app.UseDeveloperExceptionPage();
}
else
{
    app.UseExceptionHandler("/error");
}

app.MapControllers();

app.UseHttpsRedirection();

app.Run();
