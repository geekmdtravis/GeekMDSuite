﻿namespace GeekMDSuite.Core.Tools.MeasurementUnits.Conversion
{
    public static class LengthConversion
    {
        public static double InchesToCentimeters(double inches) => inches * 2.54;
        public static double CentimetersToInches(double centimeters) => centimeters / 2.54;
        public static double CentimetersToMeters(double centimeters) => centimeters / 100;
    }
}