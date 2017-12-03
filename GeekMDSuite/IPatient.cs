﻿using System;

namespace GeekMDSuite
{
    public interface IPatient
    {
        DateTime DateOfBirth { get; }
        int Age { get; }
        Name Name { get; }
        string MedicalRecordNumber { get; }
        IGender Gender { get; }
        Race Race { get; }
    }
}