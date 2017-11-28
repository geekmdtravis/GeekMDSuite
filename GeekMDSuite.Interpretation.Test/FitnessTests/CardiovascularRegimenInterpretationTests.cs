﻿using GeekMDSuite.Models;
using Xunit;
using GeekMDSuite.Models;

namespace GeekMDSuite.Interpretation.Test.FitnessTests
{
    public class CardiovascularRegimenInterpretationTests
    {
        
        [Fact]
        public void Calculate_GivenLowIntensityAnd150Minutes_ReturnsInsufficient()
        {
            var regimen = new CardiovascularRegimen(new ExerciseRegimenBase(3,50,ExerciseIntensity.Low));
            var result = GeekMDSuite.CardiovascularRegimenInterpretation.Interpret(regimen);
            
            Assert.Equal(ExerciseRegimenClassification.Insufficient, result);
        }
        
        [Fact]
        public void Calculate_GivenModerateIntensityAnd150Minutes_ReturnsAdequate()
        {
            var result = GeekMDSuite.CardiovascularRegimenInterpretation.Interpret(
                new CardiovascularRegimen(new ExerciseRegimenBase(3, 50, ExerciseIntensity.Moderate)));
            
            Assert.Equal(ExerciseRegimenClassification.Adequate, result);
        }
        
        [Fact]
        public void Calculate_GivenHighIntensityAnd150Minutes_ReturnsAspirational()
        {
            var result = GeekMDSuite.CardiovascularRegimenInterpretation.Interpret(
                new CardiovascularRegimen(new ExerciseRegimenBase(3, 50, ExerciseIntensity.High)));
            
            Assert.Equal(ExerciseRegimenClassification.Aspirational, result);
        }
        
        [Fact]
        public void RegimenPercentOfGoalAchieved_GivenHighIntensityCardiovascularAnd150Minutes_Returns200()
        {
            var result =
                GeekMDSuite.CardiovascularRegimenInterpretation.DurationPercentOfGoalAchieved(
                    new CardiovascularRegimen(new ExerciseRegimenBase(3, 50, ExerciseIntensity.High)));
            
            Assert.Equal(200, result);
        }

        [Fact]
        public void IntensityIsAdequate_GivenModerateIntensityCardiovascularAnd150Minutes_ReturnsTrue()
        {
            var result = GeekMDSuite.CardiovascularRegimenInterpretation.IntensityIsAdequate(
                new CardiovascularRegimen(new ExerciseRegimenBase(3, 50, ExerciseIntensity.Moderate)));
            
            Assert.True(result);
        }

        [Fact]
        public void DurationIsAdequate_GivenModerateIntensityCardiovascularAnd150Minutes_ReturnsTrue()
        {
            var result = GeekMDSuite.CardiovascularRegimenInterpretation.IntensityIsAdequate(
                new CardiovascularRegimen(new ExerciseRegimenBase(3, 50, ExerciseIntensity.Moderate)));
            
            Assert.True(result);
        }
        
        [Fact]
        public void RegimenIsAdequate_GivenModerateIntensityCardiovascularAnd150Minutes_ReturnsTrue()
        {
            var result = GeekMDSuite.CardiovascularRegimenInterpretation.RegimenIsAdequate(
                new CardiovascularRegimen(new ExerciseRegimenBase(3, 50, ExerciseIntensity.Moderate)));
            
            Assert.True(result);
        }
    }
}