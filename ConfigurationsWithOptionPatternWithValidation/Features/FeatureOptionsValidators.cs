using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConfigurationsWithOptionPatternWithValidation.Features
{
    internal class FeatureOptionsValidators
    {
        internal static (Func<FeatureOptions, bool> Validation, string FailureMessage) EnabledWithMissingEndpoint => (
               Validation: static options =>
               {
                   if (options is { Enabled: true, Endpoint: null })
                   {
                       return false;
                   }

                   return true;
               },
               FailureMessage: "The weather station cannot be enabled without a valid URI."
           );
    }
}
