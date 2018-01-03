﻿using System;
using System.Collections.Generic;
using GeekMDSuite.WebAPI.Presentation.EntityModels;

namespace GeekMDSuite.WebAPI.Core.DataAccess.Repositories.EntityData
{
    public interface IPatientsRepository : IRepository<PatientEntity>
    {
        IEnumerable<PatientEntity> FindByName(string query);
        IEnumerable<PatientEntity> FindByMedicalRecordNumber(string query);
        IEnumerable<PatientEntity> FindByDateOfBirth(DateTime dateOfBirth);
        PatientEntity FindByGuid(Guid guid);
    }
}