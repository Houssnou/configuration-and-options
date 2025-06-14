using ConfigurationsWithOptionsPattern.Feature;
using ConfigurationsWithOptionsPattern.Logging;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddOptions<LoggingOptions>()
    .Bind(config: builder.Configuration.GetSection(key: LoggingOptions.LoggingConfigurationSectionName));

builder.Services.AddOptions<FeatureOptions>(name: "TodoApi").Bind(config: builder.Configuration.GetSection(key: "Features:TodoApi"));

//builder.Services.AddOptions<FeatureOptions>(name: "WeatherStation").Bind(config: builder.Configuration.GetSection(key: "Features:WeatherStation"));

// overrides and/or merges with existing configured bindings
builder.Services.PostConfigure<FeatureOptions>(
    name: "WeatherStation",
    configureOptions: static (FeatureOptions options) =>
    {
        options.Version = new(1, 0);
        options.Endpoint = new(uriString: "https://freetestapi.com/api/v1/weathers");
        options.Tags = [
            "fake-weather",
            "test-api"
            ];
    });

// override all config bound instances of FeatureOptions
builder.Services.PostConfigureAll<FeatureOptions>(
    configureOptions: static (FeatureOptions options) => options.Tags ??= []
    );

builder.Services.Configure<FeatureOptions>(
    name: "WeatherStation",
    config: builder.Configuration.GetSection(key: "Features:WeatherStation"),
    configureBinder: static (BinderOptions o) =>
    {
        o.BindNonPublicProperties = true;
        o.ErrorOnUnknownConfiguration = false;
    });

// Add services to the container.
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
    app.MapOpenApi();
}

app.UseHttpsRedirection();

app.MapGet(pattern: "/logging/options", handler: static (IOptions<LoggingOptions> options) =>
{
    var loggingOptions = options.Value;
    return Results.Json(statusCode: StatusCodes.Status200OK, data: loggingOptions);
})
.WithName("GetLoggingOptions")
.WithOpenApi();



app.MapGet(pattern: "/features", handler: static (IOptionsSnapshot<FeatureOptions> options) =>
{
    var todoOptions = options.Get(name: "TodoApi");
    var weatherOptions = options.Get(name: "WeatherStation");
    return Results.Json(statusCode: StatusCodes.Status200OK, data: new
    {
        TodoApi = todoOptions,
        WeatherStation = weatherOptions
    });
})
.WithName("GetFeaturesOptions")
.WithOpenApi();

app.Run();
