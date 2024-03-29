﻿using GeekMDSuite.Core.Models.Procedures;

namespace GeekMDSuite.Analytics.Classification
{
    public class AudiogramClassificationResult
    {
        public AudiogramClassificationResult(HearingLoss classification, Laterality laterality, Laterality worseSide)
        {
            Classification = classification;
            Laterality = laterality;
            WorseSide = worseSide;
        }

        public HearingLoss Classification { get; }
        public Laterality Laterality { get; }
        public Laterality WorseSide { get; }

        public override string ToString()
        {
            return $"Classification: {Classification} " +
                   (Laterality == Laterality.Bilateral ? string.Empty : $"Laterality: {Laterality} ") +
                   (WorseSide == Laterality.Bilateral ? string.Empty : $"Worse Side: {WorseSide}");
        }
    }
}