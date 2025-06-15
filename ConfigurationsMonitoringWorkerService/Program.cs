using ConfigurationsMonitoringWorkerService;
using ConfigurationsMonitoringWorkerService.SensorStation;

var builder = Host.CreateApplicationBuilder(args);

builder.Services.AddSingleton<SensorFactory>();
builder.Services.AddHostedService<SensorMonitorService>();

builder.Services.AddOptionsWithValidateOnStart<SensorStationOptions>()
    .BindConfiguration(configSectionPath: SensorStationOptions.SensorStationOptionsSectionName)
    .ValidateDataAnnotations();

builder.Services.AddHostedService<Worker>();

var host = builder.Build();
host.Run();
