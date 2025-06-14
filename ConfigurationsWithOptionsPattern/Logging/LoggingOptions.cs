namespace ConfigurationsWithOptionsPattern.Logging
{
    public class LoggingOptions
    {
        public const string LoggingConfigurationSectionName = "Logging";

        public LogLevelOptions? LogLevel { get; set; }
    }

    public sealed class LogLevelOptions
    : Dictionary<string, LogLevel>
    {
        /// <inheritdoc cref="Dictionary{TKey, TValue}" />
        public LogLevelOptions()
            : base(StringComparer.OrdinalIgnoreCase)
        {
        }
    }
}
