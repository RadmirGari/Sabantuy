using Data.DBContext;
using Microsoft.EntityFrameworkCore;
using DotNetEnv;

Env.Load();
var builder = WebApplication.CreateBuilder(args);
builder.Configuration.AddEnvironmentVariables();

var reactUrl = builder.Configuration["URL"];

if(reactUrl == null){
    Console.WriteLine("Couldnt find a url for the frontend in env. It returned as null.");
    return;
}

builder.Services.AddCors(o => o.AddPolicy("AllowReact",
    p => p.WithOrigins(reactUrl)
          .AllowAnyMethod()
          .AllowAnyHeader()
));


// Register the DbContext with SQLite
builder.Services.AddDbContext<MyDbContext>(options =>
    options.UseSqlite(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddScoped<IImageService, ImageService>();


builder.Services.AddControllers();
builder.Services.AddOpenApi();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();             
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "My API V1");
        c.RoutePrefix = string.Empty; 
    });
}

app.UseCors("AllowReact");

app.UseHttpsRedirection();

app.MapControllers();

app.Run();
