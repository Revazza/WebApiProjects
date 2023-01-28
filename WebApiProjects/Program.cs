using Microsoft.EntityFrameworkCore;
using MoviesDatabase.Api.Services;
using System.Text.Json.Serialization;
using WebApiProjects.Db;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddControllers().AddJsonOptions(x =>
                x.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles);


//for project simplicity I'll just hard-code sql server value
builder.Services.AddDbContext<MoviesDbContext>(options =>
{
    options.UseSqlServer(@"Server=(localdb)\MSSQLLocalDB;Database=MoviesDatabase");
});

builder.Services.AddTransient<IMovieRepository, MovieRepository>();
builder.Services.AddTransient<IDirectorRepository, DirectorRepository>();

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
