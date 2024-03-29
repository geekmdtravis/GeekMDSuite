﻿using System;

namespace GeekMDSuite.Core.Models.Procedures
{
    public class Audiogram
    {
        private Audiogram(AudiogramDataset left, AudiogramDataset right)
        {
            Left = left ?? throw new ArgumentNullException(nameof(left));
            Right = right ?? throw new ArgumentNullException(nameof(right));
        }

        protected Audiogram()
        {
            Right = new AudiogramDataset();
            Left = new AudiogramDataset();
        }

        public AudiogramDataset Right { get; set; }
        public AudiogramDataset Left { get; set; }

        public static Audiogram Build(AudiogramDataset left, AudiogramDataset right)
        {
            return new Audiogram(left, right);
        }

        public override string ToString()
        {
            return $"Left: {Left}\nRight:{Right}";
        }
    }
}