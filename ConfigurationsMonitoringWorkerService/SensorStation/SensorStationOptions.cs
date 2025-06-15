using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConfigurationsMonitoringWorkerService.SensorStation
{
    internal partial class SensorStationOptions : IValidateOptions<SensorStationOptions>
    {
        /// <summary>
        /// Gets the name of the configuration section that
        /// corresponds to the <see cref="SensorStationOptions"/>.
        /// </summary>
        public const string SensorStationOptionsSectionName = "SensorOptions";

        /// <summary>
        /// Gets or sets the polling interval between sensor temperature readings.
        /// </summary>
        [Range(
            type: typeof(TimeSpan),
            minimum: "00:00:00",
            maximum: "23:59:59",
            ErrorMessage = """
            Time must be between 00:00:00 and 23:59:59
            """)]
        [RegularExpression(
            pattern: @"^([0-1]?[0-9]|2[0-3]):[0-5][0-9]:[0-5][0-9]$",
            ErrorMessage = """
            Time must be in the format HH:mm:ss and between 00:00:00 and 23:59:59
            """)]
        public TimeSpan PollingInterval { get; set; }

        /// <summary>
        /// Gets or sets the map of named thresholds settings.
        /// </summary>
        [Required(ErrorMessage = """
        A mapping of station names to thresholds is required
        """)]
        public Dictionary<string, ThresholdOptions>? Sensors { get; set; } = default!;

        public override string ToString()
        {
            var builder = new StringBuilder();

            builder.AppendFormat(
                "Polling interval: {0}", PollingInterval);

            builder.AppendLine();

            if (Sensors is { Count: > 0 })
            {
                builder.AppendLine("Sensors:");
            }

            foreach (var (name, thresholds) in Sensors ?? [])
            {
                builder.AppendFormat("""
                  "{0}" — Threshold range: ({1:F2}°F - {2:F2}°F)
                """,
                    name, thresholds.Low, thresholds.High);

                builder.AppendLine();
            }

            return builder.ToString();
        }

        public ValidateOptionsResult Validate(string? name, SensorStationOptions options)
        {
            throw new NotImplementedException();
        }
    }
}
