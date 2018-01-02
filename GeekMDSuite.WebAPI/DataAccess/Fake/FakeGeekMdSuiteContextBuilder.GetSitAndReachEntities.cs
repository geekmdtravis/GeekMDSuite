using System.Collections.Generic;
using GeekMDSuite.Core.Procedures;
using GeekMDSuite.WebAPI.Presentation.EntityModels;

namespace GeekMDSuite.WebAPI.DataAccess.Fake
{
    public static partial class FakeGeekMdSuiteContextBuilder
    {
        public static List<SitAndReachEntity> GetSitAndReachEntities()
        {
            return new List<SitAndReachEntity>()
            {
                new SitAndReachEntity(SitAndReach.Build(12)) {VisitId = BruceWaynesVisitGuid},
                new SitAndReachEntity(SitAndReach.Build(25)) {VisitId = XerMajestiesVisitGuid}
            };
        }
    }
}