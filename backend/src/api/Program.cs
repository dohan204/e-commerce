using System.Net;
using System.Threading.RateLimiting;
using api.Exceptions;
using api.Helpers.Dtos.config;
using application;
using infrastructure.dependency;
using Microsoft.AspNetCore.RateLimiting;
using Serilog;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
Log.Logger = new LoggerConfiguration()
    .MinimumLevel.Debug()
    .WriteTo.Console()
    .WriteTo.File($"Logs/log-.txt", rollingInterval: RollingInterval.Hour)
    .CreateLogger();
builder.Services.AddControllers();
// using dj
builder.Services.AddApplication(builder.Configuration);
builder.Services.AddInfrastructure(builder.Configuration);
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();
builder.Services.AddAuthentication();
builder.Services.AddAuthorization();
builder.Services.AddExceptionHandler<GlobalExceptionHandler>();
builder.Services.AddProblemDetails(); // hiển thị chi tiết khi api trả về response


var optionsRate = new RateLimitOptions();
builder.Configuration.GetSection(RateLimitOptions.RateLimit).Bind(optionsRate);
builder.Services.AddMediatR(cfg =>
{
    cfg.RegisterServicesFromAssembly(typeof(ApplicationMarker).Assembly);

    cfg.RegisterServicesFromAssembly(typeof(Program).Assembly);
});
builder.Services.AddRateLimiter(options =>
{
    options.GlobalLimiter = PartitionedRateLimiter.Create<HttpContext, string>(httpContext =>
    RateLimitPartition.GetSlidingWindowLimiter(
        partitionKey: httpContext.Connection.RemoteIpAddress?.ToString() ?? "anonymous",
        factory: _ => new SlidingWindowRateLimiterOptions
        {
            PermitLimit = optionsRate.PermitLimit,
            Window = TimeSpan.FromSeconds(optionsRate.Window),
            SegmentsPerWindow = optionsRate.SegmentsPerWindow,
            QueueProcessingOrder = QueueProcessingOrder.OldestFirst,
            QueueLimit = optionsRate.QueueLimit
        }
    ));

    options.OnRejected = async (context, cancellationtoken) =>
    {
        context.HttpContext.Response.StatusCode = StatusCodes.Status429TooManyRequests;
        context.HttpContext.Response.ContentType = "application/json";

        var response = new
        {
            Status = 429,
            Message = "Thao tác quá nhanh, vui lòng chậm lại",
            RetryAfSeconds = context.Lease.TryGetMetadata(MetadataName.RetryAfter, out var retryAfter)
                      ? retryAfter.TotalSeconds : 0
        };
        await context.HttpContext.Response.WriteAsJsonAsync(response, cancellationtoken);

        
    };
}
);
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}
app.Use((context, next) =>
{
    Console.WriteLine(context.Request.Path);
    return next();
});
app.UseRateLimiter();
app.UseExceptionHandler();
// app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
public partial class Program { }