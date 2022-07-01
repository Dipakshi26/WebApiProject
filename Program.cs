using Microsoft.EntityFrameworkCore;
using WebClassData;


var builder = WebApplication.CreateBuilder(args);

// Configure the HTTP request pipeline.

builder.Services.AddDbContext<DemoDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("CrudWebApiDbConnection") ??
    throw new InvalidOperationException("Connection string 'CrudWebApiDbConnection' not found.")));

// Add services to the container.

builder.Services.AddAutoMapper(typeof(Program));
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();
// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

// Configure the HTTP request pipeline.

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
