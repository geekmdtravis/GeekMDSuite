﻿namespace GeekMDSuite.Core
{
    public interface IBloodPressure
    {
        int Systolic { get; set; }
        int Diastolic { get; set; }
        bool OrganDamage { get; set; }
    }
}