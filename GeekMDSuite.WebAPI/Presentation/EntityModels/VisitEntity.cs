﻿using System;
using GeekMDSuite.WebAPI.Core.Models;

namespace GeekMDSuite.WebAPI.Presentation.EntityModels
{
    public class VisitEntity : IVisitData<VisitEntity>
    {
        public int Id { get; set; }
        public Guid Visit { get; set; }
        public Guid Patient { get; set; }
        public DateTime Date { get; set; }
        
        public void MapValues(VisitEntity subject)
        {
            Patient = subject.Patient;
            Date = subject.Date;
        }
    }
}