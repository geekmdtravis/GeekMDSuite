﻿using GeekMDSuite.Analytics.Classification.PatientActivities;
using GeekMDSuite.Core.Models.PatientActivities;
using Xunit;

namespace GeekMDSuite.Analytics.UnitTests.Classification.PatientActivities
{
    public class StretchingRegimenInterpretationTests
    {
        [Theory]
        [InlineData(3, 10, ExerciseIntensity.Low, ExericiseRegimen.Insufficient)]
        [InlineData(3, 10, ExerciseIntensity.Moderate, ExericiseRegimen.Adequate)]
        [InlineData(3, 20, ExerciseIntensity.High, ExericiseRegimen.Aspirational)]
        [InlineData(3, 50, ExerciseIntensity.Low, ExericiseRegimen.Insufficient)]
        public void Classification_GivenValues_ReturnsCorrectClassification(double sessionsPerWeek,
            double minutesPerSession,
            ExerciseIntensity intensity, ExericiseRegimen expectedExerciseRegimenClassification)
        {
            var classification = new StretchingRegimenClassification(
                StretchingRegimen.Build(sessionsPerWeek, minutesPerSession, intensity)).Classification;

            Assert.Equal(expectedExerciseRegimenClassification, classification);
        }

        [Fact]
        public void DurationIsAdequate_GivenModerateIntensityStretchingAnd30Minutes_ReturnsTrue()
        {
            var result = new StretchingRegimenClassification(StretchingRegimen.Build(3, 10, ExerciseIntensity.Moderate))
                .IntensityIsAdequate;

            Assert.True(result);
        }

        [Fact]
        public void IntensityIsAdequate_GivenModerateIntensityStretchingAnd30Minutes_ReturnsTrue()
        {
            var result = new StretchingRegimenClassification(StretchingRegimen.Build(3, 10, ExerciseIntensity.Moderate))
                .IntensityIsAdequate;

            Assert.True(result);
        }

        [Fact]
        public void RegimenIsAdequate_GivenModerateIntensityStretchingAnd30Minutes_ReturnsTrue()
        {
            var result = new StretchingRegimenClassification(StretchingRegimen.Build(3, 30, ExerciseIntensity.Moderate))
                .RegimenIsAdequate;

            Assert.True(result);
        }

        [Fact]
        public void RegimenPercentOfGoalAchieved_GivenHighIntensityStretchingAnd40Minutes_Returns200()
        {
            var result = new StretchingRegimenClassification(StretchingRegimen.Build(4, 10, ExerciseIntensity.High))
                .DurationPercentOfGoalAchieved;

            Assert.Equal(200, result);
        }
    }
}