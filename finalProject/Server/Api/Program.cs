using Bll;
using Bll.Functions;
using Bll.Interfaces;
using Dal.Entities;
using Dal.Functions;
using Dal.Interfaces;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
//builder.Services.AddControllers()
//           .AddJsonOptions(options =>
//           {
//               options.JsonSerializerOptions.Converters.Add( );
//           });
//Extension method for dependency injection
ExtensionMethod.InitDI( builder.Services, builder.Configuration.GetConnectionString("myContextCon"));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseCors(options =>
{
    options.AllowAnyOrigin()
        .AllowAnyHeader()
        .AllowAnyMethod();
});



app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
