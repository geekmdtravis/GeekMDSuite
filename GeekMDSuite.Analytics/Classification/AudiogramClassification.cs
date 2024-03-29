﻿using System;
using GeekMDSuite.Core.Models.Procedures;

namespace GeekMDSuite.Analytics.Classification
{
    public class AudiogramClassification : IClassifiable<AudiogramClassificationResult>
    {
        private readonly AudiogramDataset _left;
        private readonly AudiogramDataset _right;

        public AudiogramClassification(Audiogram audiogram)
        {
            _left = audiogram.Left;
            _right = audiogram.Right;
        }

        public AudiogramClassification()
        {
        }

        public AudiogramClassificationResult Classification => new AudiogramClassificationResult(
            GetClassification(),
            GetLaterality(),
            WorseSide());

        public override string ToString()
        {
            return Classification.ToString();
        }

        private Laterality GetLaterality()
        {
            return DifferenceLessThan10dB() ? Laterality.Bilateral : WorseSide();
        }

        private Laterality WorseSide()
        {
            if (LeftAndRightSideAreEqual()) return Laterality.Bilateral;
            return LeftIsWorseThanRight() ? Laterality.Left : Laterality.Right;
        }

        private bool LeftIsWorseThanRight()
        {
            return AudiogramDatasetClassification.HighestDatapoint(_left) >
                   AudiogramDatasetClassification.HighestDatapoint(_right);
        }

        private bool LeftAndRightSideAreEqual()
        {
            return AudiogramDatasetClassification.HighestDatapoint(_left) ==
                   AudiogramDatasetClassification.HighestDatapoint(_right);
        }

        private HearingLoss GetClassification()
        {
            return WorseSide() == Laterality.Left || WorseSide() == Laterality.Bilateral
                ? new AudiogramDatasetClassification(_left).Classification
                : new AudiogramDatasetClassification(_right).Classification;
        }

        private bool DifferenceLessThan10dB()
        {
            return Math.Abs(AudiogramDatasetClassification.HighestDatapoint(_left) -
                            AudiogramDatasetClassification.HighestDatapoint(_right)) < 10.0f;
        }
    }
}