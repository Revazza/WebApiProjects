using Microsoft.EntityFrameworkCore;
using WebApiProjects.Db;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

//for project simplicity I'll just hard-code value sql server
builder.Services.AddDbContext<MoviesDbContext>(options =>
{
    options.UseSqlServer(@"Server=(localdb)\MSSQLLocalDB;Database=MoviesDatabase");
});

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
