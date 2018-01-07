﻿namespace GeekMDSuite.Core.Models.Procedures
{
    public class OcularPressure
    {
        protected OcularPressure()
        {
        }

        private OcularPressure(int leftMmHg, int rightMmHg) : this()
        {
            Left = leftMmHg;
            Right = rightMmHg;
        }

        public int Left { get; set; }
        public int Right { get; set; }

        public static OcularPressure Build(int leftMmHg, int rightMmHg)
        {
            return new OcularPressure(leftMmHg, rightMmHg);
        }

        public override string ToString()
        {
            return $"Left: {Left} mmHg, Right: {Right} mmHg";
        }
    }
}