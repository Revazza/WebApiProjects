using BonusSystem.Db;
using BonusSystem.Services;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<BonusSystemDbContext>(options =>
{
    //for simplicity of the project
    options.UseSqlServer(@"Server=(localdb)\MSSQLLocalDB;Database=BonusSystem");
});

builder.Services.AddTransient<IEmployeeService,EmployeeService>();
builder.Services.AddSingleton<IHttpContextAccessor,HttpContextAccessor>();

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
