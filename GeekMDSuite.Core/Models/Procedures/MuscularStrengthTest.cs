﻿namespace GeekMDSuite.Core.Models.Procedures
{
    public abstract class MuscularStrengthTest
    {
        public double Value { get; set; }
        public StrengthTest Type { get; set; }

        public override string ToString()
        {
            return $"{Value} {Type.ToString().ToLower()}";
        }
    }
}