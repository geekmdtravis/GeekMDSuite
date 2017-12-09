﻿namespace GeekMDSuite.Services.Interpretation
{
    public class CentralBloodPressureInterpretationResult
    {
        public CentralBloodPressureInterpretationResult(CentralBloodPressureCategory category, CentralBloodPressureReferenceAge referenceAge)
        {
            Category = category;
            ReferenceAge = referenceAge;
        }

        public CentralBloodPressureCategory Category { get; }
        public CentralBloodPressureReferenceAge ReferenceAge { get; }
    }
}