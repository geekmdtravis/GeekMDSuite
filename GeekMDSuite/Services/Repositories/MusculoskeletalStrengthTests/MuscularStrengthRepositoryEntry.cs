﻿using GeekMDSuite.Services.Interpretation;
using GeekMDSuite.Tools.Generic;

namespace GeekMDSuite.Services.Repositories.MusculoskeletalStrengthTests
{
    internal class MuscularStrengthRepositoryEntry : IStrengthTestRanges
    {
        public MuscularStrengthRepositoryEntry(
            Interval<int> ageRange, 
            double lowerLimitOfPoor, 
            double lowerLimitOfBelowAverage, 
            double lowerLimitOfAverage, 
            double lowerLimitOfAboveAverage, 
            double lowerLimitOfGood, 
            double lowerLimitOfExcellent)
        {
            AgeRange = ageRange;
            LowerLimitOfPoor = lowerLimitOfPoor;
            LowerLimitOfBelowAverage = lowerLimitOfBelowAverage;
            LowerLimitOfAverage = lowerLimitOfAverage;
            LowerLimitOfAboveAverage = lowerLimitOfAboveAverage;
            LowerLimitOfGood = lowerLimitOfGood;
            LowerLimitOfExcellent = lowerLimitOfExcellent;
        }

        public Interval<int> AgeRange { get; }
        public double LowerLimitOfPoor { get; }
        public double LowerLimitOfBelowAverage { get; }
        public double LowerLimitOfAverage { get; }
        public double LowerLimitOfAboveAverage { get; }
        public double LowerLimitOfGood { get; }
        public double LowerLimitOfExcellent { get; }
    }
}