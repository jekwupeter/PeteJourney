using FluentValidation.AspNetCore;
using Microsoft.EntityFrameworkCore;
using PeteJourney.API.Data;
using PeteJourney.API.Repositories;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<PeteJourneyDbContext>(options =>
    {
        options.UseSqlServer(builder.Configuration.GetConnectionString("PeteJourney"));
    });

builder.Services.AddScoped<IRegionRepository, RegionRepository>();
builder.Services.AddScoped<IRunRepository, RunRepository>();
builder.Services.AddScoped<IRunDifficultyRepository, RunDifficultyRepository>();

builder.Services.AddFluentValidation(opt => opt.RegisterValidatorsFromAssemblyContaining<Program>());
//builder.Services.AddFluentValidationAutoValidation().AddFluentValidationClientsideAdapters();
builder.Services.AddAutoMapper(typeof(Program).Assembly);

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
