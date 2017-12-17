﻿using System;

namespace GeekMDSuite
{
    public class BodyCompositionBuilder
    {
        private double _heightInches;
        private double _waistInches;
        private double _hipsInches;
        private double _weightPounds;

        public BodyCompositionBuilder SetHeight(double inches)
        {
            _heightInches = inches;
            return this;
        }

        public BodyCompositionBuilder SetWaist(double inches)
        {
            _waistInches = inches;
            return this;
        }
        
        public BodyCompositionBuilder SetHips(double inches)
        {
            _hipsInches = inches;
            return this;
        }
        
        public BodyCompositionBuilder SetWeight(double pounds)
        {
            _weightPounds = pounds;
            return this;
        }

        public BodyComposition Build()
        {
            ValidatePreBuildState();
            return BodyComposition.Build(_heightInches, _waistInches, _hipsInches, _weightPounds);
        }

        private void ValidatePreBuildState()
        {
            var message = string.Empty;
            if (IsEffectivelyZero(_heightInches)) message += $"{nameof(SetHeight)} ";
            if (IsEffectivelyZero(_waistInches)) message += $"{nameof(SetWaist)} ";
            if (IsEffectivelyZero(_hipsInches)) message += $"{nameof(SetHips)} ";
            if (IsEffectivelyZero(_weightPounds)) message += $"{nameof(SetWeight)} ";

            if (!string.IsNullOrEmpty(message)) throw new MissingMethodException(message + " needs to be set.");
        }

        private static bool IsEffectivelyZero(double value) => Math.Abs(value - default(double)) < 0.001;
    }
}