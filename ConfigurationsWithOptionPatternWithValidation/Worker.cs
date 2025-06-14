using ConfigurationsWithOptionPatternWithValidation.Features;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace ConfigurationsWithOptionPatternWithValidation
{
    public class Worker : BackgroundService
    {
        private readonly ILogger<Worker> _logger;
        private readonly IOptionsMonitor<FeatureOptions> _options;

        public Worker(ILogger<Worker> logger, IOptionsMonitor<FeatureOptions> options)
        {
            _logger = logger;
            _options = options;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                if (_logger.IsEnabled(LogLevel.Information))
                {
                    _logger.LogInformation(
                   "TODO API feature options: {Options}",
                   _options.Get("TodoApi"));

                    _logger.LogInformation(
                        "Weather Station feature options: {Options}",
                        _options.Get("WeatherStation"));
                }

                await Task.Delay(
                    10_000, stoppingToken);
            }
        }
    }
}
