﻿using System;
using GeekMDSuite.Procedures;

namespace GeekMDSuite.Services.Interpretation
{
    public class OcularPressureInterpretation : IInterpretable<OcularPressureClassification>
    {
        public OcularPressureInterpretation(IOcularPressure pressure)
        {
            _ocularPressure = pressure ?? throw new ArgumentNullException(nameof(pressure));
        }
        public InterpretationText Interpretation => throw new NotImplementedException();
        public OcularPressureClassification Classification => Classify();
        public OcularPressureClassification Left => ClassifyLeft();
        public OcularPressureClassification Right => ClassifyRight();
        
        public static readonly int UpperLimitOfNormal = 21;

        public override string ToString() => Classification.ToString();

        private OcularPressureClassification Classify() => _ocularPressure.Left > _ocularPressure.Right 
            ? ClassifyLeft() : ClassifyRight();

        private OcularPressureClassification ClassifyLeft() => ClassifySide(_ocularPressure.Left);

        private OcularPressureClassification ClassifyRight() => ClassifySide(_ocularPressure.Right);
        
        private static OcularPressureClassification ClassifySide(int side) => side <= UpperLimitOfNormal
            ? OcularPressureClassification.Normal
            : OcularPressureClassification.OcularHypertension;

        private readonly IOcularPressure _ocularPressure;
    }
}