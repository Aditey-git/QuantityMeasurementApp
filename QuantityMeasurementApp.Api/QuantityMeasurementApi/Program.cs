using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using QuantityAppService;
using QuantityAppRepository;
using Microsoft.EntityFrameworkCore;
using System.Reflection.Metadata;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();



builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddScoped<IQuantityMeasurementService, QuantityMeasurementServices>();

builder.Services.AddDbContext<QuantityMeasurementDbContext>(options => 
  options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddScoped<IQuantityMeasurementRepository, QuantityMeasurementDatabaseRepository>();

var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI();

app.MapControllers();

app.Run();