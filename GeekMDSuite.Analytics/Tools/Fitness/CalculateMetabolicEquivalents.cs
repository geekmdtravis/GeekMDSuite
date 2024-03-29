﻿using GeekMDSuite.Core.Models;
using GeekMDSuite.Core.Models.Procedures;

namespace GeekMDSuite.Analytics.Tools.Fitness
{
    public static class CalculateMetabolicEquivalents
    {
        public static double FromVo2Max(double vo2Max)
        {
            return vo2Max / 3.5;
        }

        public static double FromTreadmillStressTest(TreadmillExerciseStressTest stressTest, Patient patient)
        {
            return FromVo2Max(CalculateVo2Max.FromTreadmillStressTest(stressTest, patient));
        }
    }
}