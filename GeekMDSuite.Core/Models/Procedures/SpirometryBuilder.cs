﻿using System;
using GeekMDSuite.Core.Builders;

namespace GeekMDSuite.Core.Models.Procedures
{
    public class SpirometryBuilder : Builder<SpirometryBuilder, Spirometry>
    {
        private double _forcedExpiratoryFlow25To75;
        private double _forcedExpiratoryTime;

        private double _forcedExpiratoryVolume1Second;
        private double _forcedVitalCapacity;
        private double _peakExpiratoryFlow;

        public override Spirometry Build()
        {
            ValidatePreBuildState();
            return BuildWithoutModelValidation();
        }

        public override Spirometry BuildWithoutModelValidation()
        {
            return new Spirometry
            {
                ForcedExpiratoryFlow25To75 = _forcedExpiratoryFlow25To75,
                ForcedExpiratoryTime = _forcedExpiratoryTime,
                ForcedExpiratoryVolume1Second = _forcedExpiratoryVolume1Second,
                ForcedVitalCapacity = _forcedVitalCapacity,
                PeakExpiratoryFlow = _peakExpiratoryFlow
            };
        }

        public SpirometryBuilder SetForcedExpiratoryVolume1Second(double liters)
        {
            _forcedExpiratoryVolume1Second = liters;
            return this;
        }

        public SpirometryBuilder SetForcedVitalCapacity(double liters)
        {
            _forcedVitalCapacity = liters;
            return this;
        }

        public SpirometryBuilder SetPeakExpiratoryFlow(double litersPerSecond)
        {
            _peakExpiratoryFlow = litersPerSecond;
            return this;
        }

        public SpirometryBuilder SetForcedExpiratoryFlow25To75(double litersPerSecond)
        {
            _forcedExpiratoryFlow25To75 = litersPerSecond;
            return this;
        }

        public SpirometryBuilder SetForcedExpiratoryTime(double seconds)
        {
            _forcedExpiratoryTime = seconds;
            return this;
        }

        private void ValidatePreBuildState()

        {
            var message = string.Empty;
            if (IsEffectivelyZero(_forcedExpiratoryVolume1Second))
                message += $"{nameof(SetForcedExpiratoryVolume1Second)} ";
            if (IsEffectivelyZero(_forcedVitalCapacity)) message += $"{nameof(SetForcedVitalCapacity)} ";
            if (IsEffectivelyZero(_peakExpiratoryFlow)) message += $"{nameof(SetPeakExpiratoryFlow)} ";
            if (IsEffectivelyZero(_forcedExpiratoryFlow25To75)) message += $"{nameof(SetForcedExpiratoryFlow25To75)} ";
            if (IsEffectivelyZero(_forcedExpiratoryTime)) message += $"{nameof(_forcedExpiratoryTime)} ";

            if (!string.IsNullOrEmpty(message)) throw new MissingMethodException($"{message} should be set");
        }

        private static bool IsEffectivelyZero(double value)
        {
            return Math.Abs(value - default(double)) < 0.001;
        }
    }
}