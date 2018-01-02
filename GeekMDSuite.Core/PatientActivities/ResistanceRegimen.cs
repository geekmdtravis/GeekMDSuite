﻿using System;
using System.Collections.Generic;
using System.Linq;

namespace GeekMDSuite.Core.PatientActivities
{
    public class ResistanceRegimen : ExerciseRegimen, IResistanceRegimen
    {
        internal ResistanceRegimen(
            IExerciseRegimenParameters regimen, 
            int secondsRestDurationPerSet, 
            List<ResistenceRegimenFeatures> features) 
            : base(regimen.SessionsPerWeek, 
                regimen.AverageSessionDuration, 
                regimen.Intensity)
        {
            if (regimen == null) throw new ArgumentNullException(nameof(regimen));
            SecondsRestDurationPerSet = secondsRestDurationPerSet;
            Features = features ?? throw new ArgumentNullException(nameof(features));
        }

        protected internal ResistanceRegimen()
        {
            
        }
        public int SecondsRestDurationPerSet { get; set; }
        public List<ResistenceRegimenFeatures> Features { get; set; }
        
        public override string ToString()
        {
            return base.ToString() + " " + ListFeatures();
        }

        private string ListFeatures() => 
            Features.Aggregate(string.Empty, (current, feature) => current + $"{feature} ");
    }
}