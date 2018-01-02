﻿using GeekMDSuite.Core;
using GeekMDSuite.WebAPI.Core.Models;
using GeekMDSuite.WebAPI.DataAccess.Services;
using GeekMDSuite.WebAPI.Presentation.EntityModels;

namespace GeekMDSuite.WebAPI.Core.DataAccess.Services
{
    public interface INewPatientService : INewKeyEntityService<PatientEntity, Patient>
    {
    }
}