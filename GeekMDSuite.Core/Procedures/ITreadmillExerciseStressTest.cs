﻿using GeekMDSuite.Core.Tools.MeasurementUnits;

namespace GeekMDSuite.Core.Procedures
{
    public interface ITreadmillExerciseStressTest
    {
        TreadmillProtocol Protocol { get; set; }
        TimeDuration Time { get; set; }
        TmstResult Result { get; set; }
        BloodPressure SupineBloodPressure { get; set; }
        int SupineHeartRate { get; set; }
        BloodPressure MaximumBloodPressure { get; set; }
        int MaximumHeartRate { get; set; }
    }
}