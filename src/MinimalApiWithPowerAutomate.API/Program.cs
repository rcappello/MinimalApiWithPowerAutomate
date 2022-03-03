using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Identity.Web;
using Microsoft.Identity.Web.Resource;
using Microsoft.IdentityModel.Tokens;
using MinimalApiWithPowerAutomate.API.BusinessLayer.Mappers;
using MinimalApiWithPowerAutomate.API.BusinessLayer.Services;
using MinimalApiWithPowerAutomate.API.DataAccessLayer;
using MinimalApiWithPowerAutomate.API.Registration;
using Serilog;
using System.Reflection;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Application Insights Telemetry
builder.Services.AddApplicationInsightsTelemetry();

// Add Automapper
builder.Services.AddAutoMapper(typeof(ECommerceMapperProfile).Assembly);

// Add services to the container.
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddMicrosoftIdentityWebApi(builder.Configuration.GetSection("AzureAd"));
builder.Services.AddAuthorization();

// Add CORS
builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(builder =>
    {
        builder.AllowAnyOrigin();
        builder.AllowAnyHeader();
        builder.AllowAnyMethod();
    });
});

// Add Serilog
builder.Host.UseSerilog((hostingContext, loggerConfiguration) =>
{
    loggerConfiguration.ReadFrom.Configuration(hostingContext.Configuration);
});
// Add Console
builder.Logging.AddConsole();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Add DBContext
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<IApplicationDbContext, ApplicationDbContext>(options =>
{
    options.UseSqlServer(connectionString, providerOptions =>
    {
        providerOptions.EnableRetryOnFailure(3, TimeSpan.FromSeconds(1), null);
        providerOptions.UseQuerySplittingBehavior(QuerySplittingBehavior.SplitQuery);
    });
});

// Add Services
builder.Services.AddScoped<WeatherService>();
builder.Services.AddScoped<ECommerceService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
//if (app.Environment.IsDevelopment()) //Always enabled for the Demo
{
    app.UseSwagger(options =>
    {
        options.SerializeAsV2 = true; //Required for PowerAutomate
    });
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

var scopeRequiredByApi = app.Configuration["AzureAd:Scopes"];
app.RegisterEndpoints(Assembly.GetExecutingAssembly(), scopeRequiredByApi);

app.UseHttpsRedirection();
app.UseCors();

app.Logger.LogInformation("Minimal API started with this Azure TenantId : {TenantId}", builder.Configuration.GetSection("AzureAd").GetValue<string>("TenantId"));

app.Run();
