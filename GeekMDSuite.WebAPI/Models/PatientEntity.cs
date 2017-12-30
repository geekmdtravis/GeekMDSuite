﻿namespace GeekMDSuite.WebAPI.Models
{
    public class PatientEntity : Patient, IEntity<PatientEntity>
    {
        public int Id { get; set; }

        public PatientEntity()
        {
            
        }

        public PatientEntity(Patient patient)
        {
            DateOfBirth = patient.DateOfBirth;
            Gender = patient.Gender;
            MedicalRecordNumber = patient.MedicalRecordNumber;
            Name = patient.Name;
            Race = patient.Race;
        }

        public void MapValues(PatientEntity subject)
        {
            DateOfBirth = subject.DateOfBirth;
            Gender.Category = subject.Gender.Category;
            MedicalRecordNumber = subject.MedicalRecordNumber;
            Name.First = subject.Name.First;
            Name.Middle = subject.Name.Middle;
            Name.Last = subject.Name.Last;
            Race = subject.Race;
        }
    }
}