using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConfigurationsMonitoringWorkerService.SensorStation
{
    internal class FakeSensorService : ISensorService
    {
        private double _currentTemperature;
        private readonly double _minChange;
        private readonly double _maxChange;

        public FakeSensorService(double initialTemperature = 65, double minChange = -0.05, double maxChange = 0.5)
        {
            _currentTemperature = initialTemperature;
            _minChange = minChange;
            _maxChange = maxChange;
        }

        public double ReadTemperature()
        {
            // Help to emulate a somewhat realistic/gradual change
            var change = Random.Shared.NextDouble() * (_maxChange - _minChange) + _minChange;
            _currentTemperature += change;
            return _currentTemperature;
        }
    }
}
