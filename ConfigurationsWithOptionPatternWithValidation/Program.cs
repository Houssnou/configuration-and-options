using ConfigurationsWithOptionPatternWithValidation;
using ConfigurationsWithOptionPatternWithValidation.Features;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Options;
using static ConfigurationsWithOptionPatternWithValidation.Features.FeatureOptionsValidators;

var builder = Host.CreateApplicationBuilder(args);

builder.Services.AddOptions<FeatureOptions>(name: "TodoApi")
    .BindConfiguration(configSectionPath: "Features:TodoApi")
    //.ValidateOnStart()  -- UNCOMMENT THIS FOR MANUAL VALIDATION
    .ValidateDataAnnotations();

//builder.Services.AddOptionsWithValidateOnStart<FeatureOptions>(name: "WeatherStation")  -- UNCOMMENT THIS FOR MANUAL VALIDATION
// moving to fluent api validation
//builder.Services.AddOptions<FeatureOptions>(name: "WeatherStation")
//    .BindConfiguration(configSectionPath: "Features:WeatherStation")
//    .ValidateDataAnnotations();

// fluent api validation   -- inline validation
//builder.Services.AddOptions<FeatureOptions>(name: "WeatherStation")
//    .BindConfiguration(configSectionPath: "Features:WeatherStation")
//    .Validate(opt =>
//    {
//        if (opt is { Enabled: true, Endpoint: null })
//        {
//            return false;
//        }

//        return true;
//    }, "The weather station cannot be enabled without..... bla bla");

// fluent api validation   -- validation using a class wich allows to reuse the validation logic and unit tests
builder.Services.AddOptions<FeatureOptions>(name: "WeatherStation")
    .BindConfiguration(configSectionPath: "Features:WeatherStation")
    .Validate(
    validation: EnabledWithMissingEndpoint.Validation,
    failureMessage: EnabledWithMissingEndpoint.FailureMessage);

//builder.Services.TryAddEnumerable(descriptor: ServiceDescriptor.Singleton<IValidateOptions<FeatureOptions>, FeatureOptions>());

builder.Services.AddHostedService<Worker>();

var host = builder.Build();
host.Run();
