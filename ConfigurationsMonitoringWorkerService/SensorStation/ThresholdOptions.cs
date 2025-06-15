using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConfigurationsMonitoringWorkerService.SensorStation
{
    internal sealed partial record class ThresholdOptions : IValidateOptions<ThresholdOptions>
    {
        /// <summary>
        /// Gets or sets the low threshold.
        /// </summary>
        [Range(
            minimum: -0.001d,
            maximum: +1.000d)]
        [Required(ErrorMessage = """
        A low threshold value is required
        """)]
        public double Low { get; set; }

        /// <summary>
        /// Gets or sets the high threshold.
        /// </summary>
        [Range(
            minimum: +1d,
            maximum: +5d)]
        [Required(ErrorMessage = """
        A high threshold value is required
        """)]
        public double High { get; set; }

        public ValidateOptionsResult Validate(string? name, ThresholdOptions options)
        {
            throw new NotImplementedException();
        }
    }
}
