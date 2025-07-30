using Microsoft.EntityFrameworkCore;
using PersonApi.Application.Mappings;
using PersonApi.Domain.Repositories;
using PersonApi.Infrastructure.Data;
using PersonApi.Infrastructure.Repositories;
using PersonApi.Api.Configurations;

var builder = WebApplication.CreateBuilder(args);
DotNetEnv.Env.Load();

// Add DbContext (in-memory for tests)
builder.Services.AddDbContext<AppDbContext>(opt => opt.UseInMemoryDatabase("PersonDb"));

// Inject repository
builder.Services.AddScoped<IPersonRepository, PersonRepository>();

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
    app.UseExceptionHandler("/error");
}
else
{
    app.UseExceptionHandler("/error");
}

app.MapControllers();

app.UseHttpsRedirection();

app.Run();
