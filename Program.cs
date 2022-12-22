using Data4SalesChallenge;
using Data4SalesChallenge.Context;
using Data4SalesChallenge.Repository;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddSingleton<DBContext>();
builder.Services.AddScoped<IStartupRepository, StartupRepository>();
builder.Services.AddScoped<IFilmsRepository, FilmsRepository>();
builder.Services.AddScoped<IPeopleRepository, PeopleRepository>();
builder.Services.AddScoped<IPlanetsRepository, PlanetsRepository>();
builder.Services.AddScoped<ISpeciesRepository, SpeciesRepository>();
builder.Services.AddScoped<IStarshipsRepository, StarshipsRepository>();
builder.Services.AddScoped<IVehiclesRepository, VehiclesRepository>();

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var root = Directory.GetCurrentDirectory();
var dotenv = Path.Combine(root, ".env");
DotEnv.Load(dotenv);

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
