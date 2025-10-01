using AspNetCoreRateLimit;
using Microsoft.AspNetCore.Mvc;
using Services.Contracts;
using WebApi.Extensions;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddControllers(
    config =>
    { 
        config.RespectBrowserAcceptHeader = true;
        config.ReturnHttpNotAcceptable = true;
       // config.CacheProfiles.Add("5mins", new CacheProfile() { Duration=300 });
    }
    ).AddXmlDataContractSerializerFormatters().AddNewtonsoftJson().AddApplicationPart(typeof(Presentation.AssemblyReference).Assembly);
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.ConfigureSqlContext(builder.Configuration);
builder.Services.ConfigureRepositoryManager();
builder.Services.ConfigureServiceManager();
builder.Services.ConfigureLoggerServiceManager();
builder.Services.AddAutoMapper(typeof(Program));
builder.Services.ConfigureActionFilters();
builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(policy =>
        policy.AllowAnyOrigin()
              .AllowAnyMethod()
              .AllowAnyHeader());
});
builder.Services.ConfigureVersioning();
builder.Services.AddMemoryCache();
builder.Services.ConfigureRateLimitingOptions();
builder.Services.AddHttpContextAccessor();
//builder.Services.ConfigureResponseCaching();
builder.Services.ConfigureHttpCacheHeaders();
var app = builder.Build();
var logger=app.Services.GetRequiredService<ILoggerService>();
app.ConfigureGlobalExceptionMiddleware();
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
if (app.Environment.IsProduction())
{
    app.UseHsts();
}

app.UseIpRateLimiting();
app.UseCors();
app.UseHttpsRedirection();
//app.UseResponseCaching();
app.UseHttpCacheHeaders();
app.UseAuthorization();

app.MapControllers();

app.Run();
