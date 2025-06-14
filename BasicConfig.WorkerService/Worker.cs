namespace BasicConfig.WorkerService
{
    public class Worker : BackgroundService
    {
        private readonly ILogger<Worker> _logger;
        private readonly IConfiguration _config;

        public Worker(ILogger<Worker> logger, IConfiguration config)
        {
            _logger = logger;
            _config = config;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                if (_logger.IsEnabled(LogLevel.Information))
                {
                    _logger.LogInformation("Worker running at: {time}", DateTimeOffset.Now);
                }

                // originally: await Task.Delay(1_000, stoppingToken);

                // manual parse of delay from appsettings.json
                // var delay = TimeSpan.Parse(_config["Delay"] ?? "0:0:05");

                // binding to a configuration section
                var delay = _config.GetValue<TimeSpan>("Delay", TimeSpan.FromSeconds(5));


                await Task.Delay(delay, stoppingToken);
            }
        }
    }
}
