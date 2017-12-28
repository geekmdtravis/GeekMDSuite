﻿using GeekMDSuite.LaboratoryData.Builder;
using GeekMDSuite.Tools.Cardiology;
using Moq;
using Xunit;

namespace GeekMDSuite.Test
{
    public partial class PooledCohortsEquationTests
    {
        [Theory]
        [InlineData(Race.White, GenderIdentity.Female, false, false, false, 2.1)]
        [InlineData(Race.White, GenderIdentity.Female, true, false, false, 2.8)]
        [InlineData(Race.White, GenderIdentity.Female, false, true, false, 3.9)]
        [InlineData(Race.White, GenderIdentity.Female, false, false, true, 5.0)]
        [InlineData(Race.White, GenderIdentity.Female, true, true, false, 5.3)]
        [InlineData(Race.White, GenderIdentity.Female, true, false, true, 6.6)]
        [InlineData(Race.White, GenderIdentity.Female, false, true, true, 9.4)]
        [InlineData(Race.White, GenderIdentity.Female, true, true, true, 12.5)]
        [InlineData(Race.White, GenderIdentity.Male, false, false, false, 5.3)]
        [InlineData(Race.White, GenderIdentity.Male, true, false, false, 6.3)]
        [InlineData(Race.White, GenderIdentity.Male, false, true, false, 10.1)]
        [InlineData(Race.White, GenderIdentity.Male, false, false, true, 10.0)]
        [InlineData(Race.White, GenderIdentity.Male, true, true, false, 11.8)]
        [InlineData(Race.White, GenderIdentity.Male, true, false, true, 11.6)]
        [InlineData(Race.White, GenderIdentity.Male, false, true, true, 18.4)]
        [InlineData(Race.White, GenderIdentity.Male, true, true, true, 21.2)]
        [InlineData(Race.BlackOrAfricanAmerican, GenderIdentity.Female, false, false, false, 3.0)]
        [InlineData(Race.BlackOrAfricanAmerican, GenderIdentity.Female, true, false, false, 4.6)]
        [InlineData(Race.BlackOrAfricanAmerican, GenderIdentity.Female, false, true, false, 7.1)]
        [InlineData(Race.BlackOrAfricanAmerican, GenderIdentity.Female, false, false, true, 5.9)]
        [InlineData(Race.BlackOrAfricanAmerican, GenderIdentity.Female, true, true, false, 10.6)]
        [InlineData(Race.BlackOrAfricanAmerican, GenderIdentity.Female, true, false, true, 8.9)]
        [InlineData(Race.BlackOrAfricanAmerican, GenderIdentity.Female, false, true, true, 13.7)]
        [InlineData(Race.BlackOrAfricanAmerican, GenderIdentity.Female, true, true, true, 20.1)]
        [InlineData(Race.BlackOrAfricanAmerican, GenderIdentity.Male, false, false, false, 6.1)]
        [InlineData(Race.BlackOrAfricanAmerican, GenderIdentity.Male, true, false, false, 9.9)]
        [InlineData(Race.BlackOrAfricanAmerican, GenderIdentity.Male, false, true, false, 11.3)]
        [InlineData(Race.BlackOrAfricanAmerican, GenderIdentity.Male, false, false, true, 10.3)]
        [InlineData(Race.BlackOrAfricanAmerican, GenderIdentity.Male, true, true, false, 18.1)]
        [InlineData(Race.BlackOrAfricanAmerican, GenderIdentity.Male, true, false, true, 16.6)]
        [InlineData(Race.BlackOrAfricanAmerican, GenderIdentity.Male, false, true, true, 18.7)]
        [InlineData(Race.BlackOrAfricanAmerican, GenderIdentity.Male, true, true, true, 29.2)]
        public void PercentAscvdRisk10Year_Given2013AccAhaSampleParams_ReturnsCorrectRiskPercentage(Race race,
            GenderIdentity genderIdentity, bool hypertensionTreatment, bool diabetes,
            bool smoker, double expected)
        {
            var mockPatient = new Mock<IPatient>();
            mockPatient.Setup(p => p.Age).Returns(55);
            mockPatient.Setup(p => p.Gender.Category).Returns(genderIdentity);
            mockPatient.Setup(p => p.Race).Returns(race);

            var ascvd = new PooledCohortsEquation(
                    mockPatient.Object,
                    BloodPressure.Build(120, 75),
                    Quantitative.Serum.CholesterolTotal(213),
                    Quantitative.Serum.HighDensityLipoprotein(50),
                    hypertensionTreatment,
                    smoker,
                    diabetes)
                .AscvdRiskPercentOver10Years();

            const double tolerance = 0.1;
            Assert.InRange(ascvd, expected - tolerance, expected + tolerance);
        }
        
        [Theory]
        [InlineData(Race.White, GenderIdentity.Female, 1.1)]
        [InlineData(Race.White, GenderIdentity.Male, 3.0)]
        [InlineData(Race.BlackOrAfricanAmerican, GenderIdentity.Female, 1.5)]
        [InlineData(Race.BlackOrAfricanAmerican, GenderIdentity.Male, 4.2)]
        public void IdealPercentAscvdRisk10Year_Given2013AccAhaSampleParams_ReturnsCorrectRiskPercentage(Race race,
            GenderIdentity genderIdentity, double expected)
        {
            var mockPatient = new Mock<IPatient>();
            mockPatient.Setup(p => p.Age).Returns(55);
            mockPatient.Setup(p => p.Gender.Category).Returns(genderIdentity);
            mockPatient.Setup(p => p.Race).Returns(race);

            var idealAscvd = new PooledCohortsEquation(
                    mockPatient.Object,
                    BloodPressure.Build(default(int), default(int)),
                    Quantitative.Serum.CholesterolTotal(default(int)),
                    Quantitative.Serum.HighDensityLipoprotein(default(int)))
                .IdealAscvdRiskPercentOver10Years();

            const double tolerance = 0.31; // Tolerance necessary because expected values are estimates.
            Assert.InRange(idealAscvd, expected - tolerance, expected + tolerance);
        }
    }
}