﻿namespace GeekMDSuite.Utilities.MeasurementUnits.Conversion
{
    public static class TemperatureConversion
    {
        public static double FarenheitToCelcius(double farenheit)
        {
            return (farenheit - 32.0) * 5.0 / 9.0;
        }

        public static double CelciusToFarenheight(double celcius)
        {
            return celcius * 9.0 / 5.0 + 32;
        }
    }
}