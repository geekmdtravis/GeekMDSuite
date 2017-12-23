﻿using GeekMDSuite.PatientActivities;
using GeekMDSuite.Services.Interpretation.PatientActivities;
using Xunit;

namespace GeekMDSuite.Test
{
    public class StretchingRegimenInterpretationTests
    {
        [Theory]
        [InlineData(3,10,ExerciseIntensity.Low,ExerciseRegimenClassification.Insufficient)]
        [InlineData(3,10,ExerciseIntensity.Moderate,ExerciseRegimenClassification.Adequate)]
        [InlineData(3,20,ExerciseIntensity.High,ExerciseRegimenClassification.Aspirational)]
        [InlineData(3,50,ExerciseIntensity.Low,ExerciseRegimenClassification.Insufficient)]

        public void Classification_GivenValues_ReturnsCorrectClassification(double sessionsPerWeek, double minutesPerSession,
            ExerciseIntensity intensity, ExerciseRegimenClassification expectedExerciseRegimenClassification)
        {
            var classification = new StretchingRegimenInterpretation(
                ExerciseRegimenParameters.Build(sessionsPerWeek, minutesPerSession, intensity)).Classification;
            
            Assert.Equal(expectedExerciseRegimenClassification, classification);
        }
        
        [Fact]
        public void RegimenPercentOfGoalAchieved_GivenHighIntensityStretchingAnd40Minutes_Returns200()
        {
            var result = new StretchingRegimenInterpretation(ExerciseRegimenParameters.Build(4, 10, ExerciseIntensity.High)).DurationPercentOfGoalAchieved;
            
            Assert.Equal(200, result);
        }
        
        [Fact]
        public void IntensityIsAdequate_GivenModerateIntensityStretchingAnd30Minutes_ReturnsTrue()
        {
            var result = new StretchingRegimenInterpretation(ExerciseRegimenParameters.Build(3, 10, ExerciseIntensity.Moderate)).IntensityIsAdequate;
            
            Assert.True(result);
        }

        [Fact]
        public void DurationIsAdequate_GivenModerateIntensityStretchingAnd30Minutes_ReturnsTrue()
        {
            var result = new StretchingRegimenInterpretation(ExerciseRegimenParameters.Build(3, 10, ExerciseIntensity.Moderate)).IntensityIsAdequate;
            
            Assert.True(result);
        }
        
        [Fact]
        public void RegimenIsAdequate_GivenModerateIntensityStretchingAnd30Minutes_ReturnsTrue()
        {
            var result = new StretchingRegimenInterpretation(ExerciseRegimenParameters.Build(3, 30, ExerciseIntensity.Moderate)).RegimenIsAdequate;
            
            Assert.True(result);
        }
    }
}