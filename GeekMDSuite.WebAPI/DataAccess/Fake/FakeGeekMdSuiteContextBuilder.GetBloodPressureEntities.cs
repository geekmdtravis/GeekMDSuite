using System;
using System.Collections.Generic;
using GeekMDSuite.Core;
using GeekMDSuite.WebAPI.Presentation.EntityModels;

namespace GeekMDSuite.WebAPI.DataAccess.Fake
{
    public static partial class FakeGeekMdSuiteContextBuilder
    {
        private static List<BloodPressureEntity> GetBloodPressureEntities()
        {
            return new List<BloodPressureEntity>()
            {
                new BloodPressureEntity(BloodPressure.Build(115, 75)) { VisitId = XerMajestiesVisitGuid },
                new BloodPressureEntity(BloodPressure.Build(190, 110, true)) { VisitId = BruceWaynesVisitGuid },
            };
        }
    }
}