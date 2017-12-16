﻿using System;
using GeekMDSuite.Extensions;
using Newtonsoft.Json.Linq;

namespace GeekMDSuite
{
    public class Patient : IPatient
    {
        public Patient(Name name, DateTime dateOfBirth, IGender gender, Race race, string medicalRecordNumber)
        {
            DateOfBirth = dateOfBirth;
            Name = name;
            MedicalRecordNumber = medicalRecordNumber;
            Gender = gender;
            Race = race;
        }

        public DateTime DateOfBirth { get; }
        public int Age => DateOfBirth.ElapsedYears();
        public Name Name { get; }
        public string MedicalRecordNumber { get; }
        public IGender Gender { get; }
        public Race Race { get; }

        public static Patient Create(Name name, DateTime dateOfBirth, IGender gender, Race race, string medicalRecordNumber) 
            => new Patient(name, dateOfBirth, gender, race, medicalRecordNumber);
    }
}