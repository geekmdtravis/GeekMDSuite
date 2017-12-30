﻿using GeekMDSuite.WebAPI.Core.DataAccess.Repositories;
using GeekMDSuite.WebAPI.DataAccess.Context;
using GeekMDSuite.WebAPI.Presentation.EntityModels;

namespace GeekMDSuite.WebAPI.DataAccess.Repositories
{
    public class BloodPressuresRepository : RepositoryAssociatedWithVisit<BloodPressureEntity>, IBloodPressuresRepository
    {
        public BloodPressuresRepository(GeekMdSuiteDbContext context) : base (context) {}
    }
}