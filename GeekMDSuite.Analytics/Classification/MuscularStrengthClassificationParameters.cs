﻿using GeekMDSuite.Core.Models;
using GeekMDSuite.Core.Models.Procedures;

namespace GeekMDSuite.Analytics.Classification
{
    public class MuscularStrengthClassificationParameters
    {
        public MuscularStrengthClassificationParameters(MuscularStrengthTest test, Patient patient)
        {
            Test = test;
            Patient = patient;
        }

        public MuscularStrengthTest Test { get; private set; }
        public Patient Patient { get; private set; }
    }
}