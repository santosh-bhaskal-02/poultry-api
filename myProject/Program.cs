using Microsoft.EntityFrameworkCore;
using myProject.dbContextNameSpace;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddCors(options =>
{
    options.AddPolicy("myCors",
        policy =>
        {
            policy.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod();
        });
});


builder.Services.AddDbContext<dbContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("Default")));
var app = builder.Build();

// Configure the HTTP request pipeline.

app.UseHttpsRedirection();

app.UseAuthorization();

app.UseCors("myCors");


app.MapControllers();

app.Run();
