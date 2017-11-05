﻿using GeekMDSuite.Common;

namespace GeekMDSuite.Interpretation
{
    public static class BodyMassIndex
    {
        public static BodyMassIndexCategories Interpret (Races race, double result) => ClassifyBodyMassIndex (race, result);

        private static BodyMassIndexCategories ClassifyBodyMassIndex(Races race, double result)
        {
            var overWeightLowerLimit = race == Races.Asian ? OverWeightLowerLimitAsian : OverWeightLowerLimitNonAsian;
            var obeseClass1LowerLimit = race == Races.Asian ? ObeseClass1LowerLimitAsian : ObeseClass1LowerLimitNonAsian;
            
            if (result < UnderWeightLowerLimit)
                return BodyMassIndexCategories.SeverelyUnderweight;
            if (result < NormalWeightLowerLimit)
                return BodyMassIndexCategories.Underweight;
            if (result < overWeightLowerLimit)
                return BodyMassIndexCategories.NormalWeight;
            if (result < obeseClass1LowerLimit)
                return BodyMassIndexCategories.OverWeight;
            if (result < ObeseClass2LowerLimit)
                return BodyMassIndexCategories.ObesityClassI;
            return result < ObeseClass3LowerLimit ? BodyMassIndexCategories.ObesityClassII : BodyMassIndexCategories.ObesityClassIII;
        }
        
        public static double UnderWeightLowerLimit = 17.5;
        public static double NormalWeightLowerLimit = 18.5;
        public static double OverWeightLowerLimitAsian = 23;
        public static double OverWeightLowerLimitNonAsian = 25;
        public static double ObeseClass1LowerLimitAsian = 27;
        public static double ObeseClass1LowerLimitNonAsian = 30;
        public static double ObeseClass2LowerLimit = 35;
        public static double ObeseClass3LowerLimit = 40;
    }
}