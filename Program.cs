using ApiCatalogo.Context;
using Microsoft.EntityFrameworkCore;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers()
    .AddJsonOptions(options => 
        options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles
    );
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


string mySqlConnection = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseMySql(mySqlConnection,
    ServerVersion.AutoDetect(mySqlConnection))
);



var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
} else
{
    app.UseExceptionHandler("/Error");
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();
app.Run();
