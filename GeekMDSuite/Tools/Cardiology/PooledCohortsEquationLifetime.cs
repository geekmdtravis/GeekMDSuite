﻿using static GeekMDSuite.Tools.Cardiology.PooledCohortsEquation.AscvdLifetimeRiskPercentages;

namespace GeekMDSuite.Tools.Cardiology
{
    public partial class PooledCohortsEquation
    {
        public double AscvdLifetimeRisk()
        {
            if (MajorPoints >= 2) return Gender.IsGenotypeXy(_gender) ? Male.TwoMajor : Female.TwoMajor;
            if (MajorPoints == 1) return Gender.IsGenotypeXy(_gender) ? Male.OneMajor : Female.OneMajor;
            if (Elevated) return Gender.IsGenotypeXy(_gender) ? Male.Elevated : Female.Elevated;
            if (NotOptimal) return Gender.IsGenotypeXy(_gender) ? Male.NotOptimal : Female.NotOptimal;
            
            return Gender.IsGenotypeXy(_gender) ? Male.Optimal : Female.Optimal;
        }

        private bool NotOptimal => (_totalCholesterol >= 180 || _systolicBloodPressure >= 120) && !_hypertensionTreatment;

        private bool Elevated => (_totalCholesterol >= 200 || _systolicBloodPressure >= 140) && !_hypertensionTreatment;

        private int MajorPoints => (_totalCholesterol >= 240 ? 1 : 0) +
                                   (_systolicBloodPressure >= 160 ? 1 : 0) +
                                   (_hypertensionTreatment ? 1 : 0) +
                                   (_diabetic ? 1 : 0) +
                                   (_smoker ? 1 : 0);

        internal static class AscvdLifetimeRiskPercentages
        {
            public static class Male
            {
                public const double TwoMajor = 69;
                public const double OneMajor = 50;
                public const double Elevated = 46;
                public const double NotOptimal = 36;
                public const double Optimal = 5;
            }
            public static class Female
            {
                public const double TwoMajor = 50;
                public const double OneMajor = 39;
                public const double Elevated = 39;
                public const double NotOptimal = 27;
                public const double Optimal = 8;
            }
        }
    }
}