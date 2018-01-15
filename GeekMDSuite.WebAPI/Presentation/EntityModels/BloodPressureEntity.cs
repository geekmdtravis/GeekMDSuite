﻿using System;
using GeekMDSuite.Core.Models;
using GeekMDSuite.WebAPI.Core.Models;

namespace GeekMDSuite.WebAPI.Presentation.EntityModels
{
    public class BloodPressureEntity : BloodPressure, IVisitData<BloodPressure>
    {
        public int Id { get; set; }
        public Guid Guid { get; set; }
        
        public BloodPressureEntity()
        {
            Guid = Guid.Empty;
        }

        public BloodPressureEntity(BloodPressure bloodPressure) : this()
        {
            MapValues(bloodPressure);
        }

        public void MapValues(BloodPressure subject)
        {
            Diastolic = subject.Diastolic;
            Systolic = subject.Systolic;
            OrganDamage = subject.OrganDamage;
        }
    }
}