using MagicVilla_VillaAPI.Custom_Logger;
using Serilog;

var builder = WebApplication.CreateBuilder(args);

////Optimizing the projec to use the sirilog and log the data into a file
//Log.Logger = new LoggerConfiguration().MinimumLevel.Debug().WriteTo.File("log/villaLogs.txt",rollingInterval:RollingInterval.Day).CreateLogger();

//builder.Host.UseSerilog();



// Add services to the container.

//setting the api to not accept types than the JSON and allowing xml
//builder.Services.AddControllers(option => option.ReturnHttpNotAcceptable=true).AddNewtonsoftJson().AddXmlDataContractSerializerFormatters();


builder.Services.AddControllers().AddNewtonsoftJson();







// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

//Registering the custom logger
//   //Using Logging class
//builder.Services.AddSingleton<ILogging, Logging>();
    //Using LoggingV2 class
builder.Services.AddSingleton<ILogging, LoggingV2>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
