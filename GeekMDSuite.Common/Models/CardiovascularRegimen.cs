﻿using GeekMDSuite.Common.Services;

namespace GeekMDSuite.Common.Models
{
    public class CardiovascularRegimen : ExerciseRegimenBase
    {
        public CardiovascularRegimen(ExerciseRegimenBase baseRegimen) 
            : base(baseRegimen.SessionsPerWeek, baseRegimen.AverageSessionDuration, baseRegimen.Intensity)
        {
            Goals = GetExerciseGoalValues.TotalWeeklyDuration(ExerciseClassifications.Cardiovascular);
        }

        public sealed override ExerciseDurationGoals Goals { get; set; }
    }
}