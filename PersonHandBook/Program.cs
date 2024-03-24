using FluentValidation.AspNetCore;
using Microsoft.EntityFrameworkCore;
using NLog.Extensions.Logging;
using PersonHandBook;
using PersonHandBook.Middlewares;
using PersonHandBook.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddMvc(options =>
	{
		options.Filters.Add(new ModelStateFilter());
	})
	.AddFluentValidation(options =>
	{
		options.RegisterValidatorsFromAssembly(typeof(Program).Assembly);
	});

builder.Services.AddAutoMapper(typeof(PersonProfile));

ConfigurationManager configuration = builder.Configuration;
builder.Services.AddDbContext<PersonContext>(opt =>
		  opt.UseSqlServer(configuration.GetConnectionString("PersonHandBook"))
			 .EnableSensitiveDataLogging()
			 .UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking));
builder.Services.AddScoped<IPersonService, PersonService>();

builder.Services.AddLogging(logging =>
{
	logging.ClearProviders();
	logging.SetMinimumLevel(LogLevel.Trace);
});

// Add NLog as the logger provider
builder.Services.AddSingleton<ILoggerProvider, NLogLoggerProvider>();
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
	app.UseSwagger();
	app.UseSwaggerUI();
}

app.UseUnhandledExceptionLogger();
app.UseLocalizationMiddleware();
app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
