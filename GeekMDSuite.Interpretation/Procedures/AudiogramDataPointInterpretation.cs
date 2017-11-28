﻿using System;
using GeekMDSuite.Models;
using GeekMDSuite.Services;
using GeekMDSuite.Tools;

namespace GeekMDSuite.Interpretation.Procedures
{
    public class AudiogramDataPointInterpretation : IInterpretable
    {
        public Interpretation Interpretation => throw new NotImplementedException();
        
        private Interpretation _interpretation;

        public static AudiogramDatapoint Interpret(int value)
        {
            if (NormalRange.ContainsClosed(value)) 
                return new AudiogramDatapoint { Value = value, Flag = HearingLossClassification.None };
            if (MildRange.ContainsLeftOpen(value)) 
                return new AudiogramDatapoint { Value = value, Flag = HearingLossClassification.Mild };
            if (ModerateRange.ContainsLeftOpen(value)) 
                return new AudiogramDatapoint { Value = value, Flag = HearingLossClassification.Moderate };
            return SevereRange.ContainsLeftOpen(value) 
                ? new AudiogramDatapoint { Value = value, Flag = HearingLossClassification.Severe } 
                : new AudiogramDatapoint { Value = value, Flag = HearingLossClassification.Profound };
        }
        public static Interval<int> NormalRange => GetHearingLossRangeClassifications.GetRange(HearingLossClassification.None);
        public static Interval<int> MildRange => GetHearingLossRangeClassifications.GetRange(HearingLossClassification.Mild);
        public static Interval<int> ModerateRange => GetHearingLossRangeClassifications.GetRange(HearingLossClassification.Moderate);
        public static Interval<int> SevereRange => GetHearingLossRangeClassifications.GetRange(HearingLossClassification.Severe);
        public static Interval<int> ProfounRange => GetHearingLossRangeClassifications.GetRange(HearingLossClassification.Profound);

    }
}