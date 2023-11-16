using Hangfire;
using Hangfire.SqlServer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using WebApplication1.DBModels;
using WebApplication1.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();

builder.Services.AddTransient<IEmailHelper,EmailHelper>();

//builder.Services.AddHangfire(configuration => configuration
//       .SetDataCompatibilityLevel(CompatibilityLevel.Version_170)
//       .UseSimpleAssemblyNameTypeSerializer()
//       .UseRecommendedSerializerSettings()
//       .UseSqlServerStorage(builder.Configuration.GetConnectionString("localDB"), new SqlServerStorageOptions
//       {
//           CommandBatchMaxTimeout = TimeSpan.FromMinutes(5),
//           SlidingInvisibilityTimeout = TimeSpan.FromMinutes(5),
//           QueuePollInterval = TimeSpan.Zero,
//           UseRecommendedIsolationLevel = true,
//           DisableGlobalLocks = true
//       }));


builder.Services.AddHangfire(configuration => configuration
       .SetDataCompatibilityLevel(CompatibilityLevel.Version_180)
       .UseSimpleAssemblyNameTypeSerializer()
.UseRecommendedSerializerSettings()
       .UseSqlServerStorage(builder.Configuration.GetConnectionString("localDB")));

builder.Services.AddHangfireServer();

builder.Services.AddDbContext<ADATDBContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("localDB")));
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

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
