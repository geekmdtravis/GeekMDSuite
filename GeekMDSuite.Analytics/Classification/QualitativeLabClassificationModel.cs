﻿namespace GeekMDSuite.Analytics.Classification
{
    public class QualitativeLabClassificationModel
    {
        public string LabName { get; set; }
        public string LabNameShort { get; set; }
        public string LabNamefull { get; set; }
        public string Specimen { get; set; }
        public string Interpretation { get; set; }
        public string Comments { get; set; }

        public override string ToString()
        {
            return $"{LabNameShort}({Specimen})";
        }
    }
}