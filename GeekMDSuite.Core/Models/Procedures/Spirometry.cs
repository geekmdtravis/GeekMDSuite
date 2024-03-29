﻿namespace GeekMDSuite.Core.Models.Procedures
{
    public class Spirometry
    {
        protected internal Spirometry()
        {
        }

        private Spirometry(double forcedExpiratoryVolume1Second,
            double forcedVitalCapacity,
            double peakExpiratoryFlow,
            double forcedExpiratoryFlow25To75,
            double forcedExpiratoryTime)
        {
            ForcedExpiratoryVolume1Second = forcedExpiratoryVolume1Second;
            ForcedVitalCapacity = forcedVitalCapacity;
            PeakExpiratoryFlow = peakExpiratoryFlow;
            ForcedExpiratoryFlow25To75 = forcedExpiratoryFlow25To75;
            ForcedExpiratoryTime = forcedExpiratoryTime;
        }

        public double ForcedExpiratoryVolume1Second { get; set; }
        public double ForcedVitalCapacity { get; set; }
        public double PeakExpiratoryFlow { get; set; }
        public double ForcedExpiratoryFlow25To75 { get; set; }
        public double ForcedExpiratoryTime { get; set; }

        public double ForcedExpiratoryVolume1SecondToForcedVitalCapacityRatio =>
            ForcedExpiratoryVolume1Second / ForcedVitalCapacity;

        internal static Spirometry Build(double forcedExpiratoryVolume1Second,
            double forcedVitalCapacity,
            double peakExpiratoryFlow,
            double forcedExpiratoryFlow25To75,
            double forcedExpiratoryTime)
        {
            return new Spirometry(forcedExpiratoryVolume1Second, forcedVitalCapacity,
                peakExpiratoryFlow, forcedExpiratoryFlow25To75, forcedExpiratoryTime);
        }

        public override string ToString()
        {
            return
                $"FEV1 {ForcedExpiratoryVolume1Second} L, FVC {ForcedVitalCapacity} L, FEF25-75 {ForcedExpiratoryFlow25To75} L/s " +
                $"PET {PeakExpiratoryFlow} L/s, FET {ForcedExpiratoryTime} sec, FEV1:FVC {ForcedExpiratoryVolume1SecondToForcedVitalCapacityRatio}";
        }
    }
}