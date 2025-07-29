using Microsoft.EntityFrameworkCore;
using PersonApi.Domain.Entities;
using PersonApi.Domain.Repositories;
using PersonApi.Infrastructure.Data;
using PersonApi.Infrastructure.Repositories;

var builder = WebApplication.CreateBuilder(args);

// Add DbContext (in-memory for tests)
builder.Services.AddDbContext<AppDbContext>(opt => opt.UseInMemoryDatabase("PersonDb"));

// Inject repository
builder.Services.AddScoped<IPersonRepository, PersonRepository>();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Add support to controllers
builder.Services.AddControllers();

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

// app.MapGet("/people", async (IPersonRepository repo) => await repo.GetAllAsync());
// app.MapGet("/people/{id}", async (int id, IPersonRepository repo) => await repo.GetByIdAsync(id));
// app.MapPost("/people", async (Person p, IPersonRepository repo) =>
// {
//     await repo.AddAsync(p);
//     return Results.Created($"/people/{p.Id}", p);
// });
// app.MapPut("/people/{id}", async (int id, Person p, IPersonRepository repo) =>
// {
//     await repo.UpdateAsync(p);
//     return Results.NoContent();
// });
// app.MapDelete("/people", async (int id, IPersonRepository repo) =>
// {
//     await repo.DeleteAsync(id);
//     return Results.NoContent();
// });

app.MapControllers();

app.UseHttpsRedirection();

app.Run();
