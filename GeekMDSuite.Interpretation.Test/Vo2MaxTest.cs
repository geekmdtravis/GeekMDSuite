using System;
using GeekMDSuite.Calculations;
using GeekMDSuite.Common;
using Xunit;

namespace GeekMDSuite.Interpretation.Test
{
    public class Vo2MaxTest
    {
        private double vo2Max = 45;
        private Genders male = Genders.NonBinaryXy;
        private Genders female = Genders.Female;
        private double age = 52;

        [Fact]
        public void ReturnsCorrectInterpetationForMale()
        {
            var vo2MaxInterp = Vo2Max.Interpret(vo2Max, male, age);
            
            Assert.Equal(FitnessClassification.Good, vo2MaxInterp);
        }
        
        [Fact]
        public void ReturnsCorrectInterpetationForFemale()
        {
            var vo2MaxInterp = Vo2Max.Interpret(vo2Max, female, age);
            
            Assert.Equal(FitnessClassification.Excellent, vo2MaxInterp);
        }
    }
}