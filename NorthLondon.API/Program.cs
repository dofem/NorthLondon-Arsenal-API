using Microsoft.Extensions.Logging.Configuration;
using Microsoft.OpenApi.Models;
using NorthLondon.Application;
using NorthLondon.Infastructure;
using NorthLondon.Infastructure.Utility;
using Serilog;
using Serilog.Events;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
Log.Logger = new LoggerConfiguration()
    .MinimumLevel.Override("Microsoft",LogEventLevel.Information)
    .MinimumLevel.Information()
    .WriteTo.Console()
    .WriteTo.File($"Logs/{DateTime.Now.ToShortDateString()}.txt", rollingInterval : RollingInterval.Day)
    .CreateLogger();

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1", new OpenApiInfo
    {
        Version = "1.0",
        Title = "Arsenal Player API",
        Description = "An API that helps to add,delete,update and get Arsenal Players records",
        Contact = new OpenApiContact
        {
            Name = "Dally Oluwafemi O.",
            Email = "DallyOluwafemi@gmail.com",
        }
    });
});

builder.Services.Configure<AppSettings>(options => builder.Configuration.GetConnectionString("DefaultConnection"));

builder.Services.AddScoped<IDataAccess, DataAccess>();
builder.Services.AddScoped<IPlayerService,PlayerService>();


var app = builder.Build();

// Configure the HTTP request pipeline.

app.UseHsts();

app.UseCors(x => x
.AllowAnyOrigin()
.AllowAnyMethod()
.AllowAnyHeader());

app.UseSwagger();
app.UseSwaggerUI();


app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
