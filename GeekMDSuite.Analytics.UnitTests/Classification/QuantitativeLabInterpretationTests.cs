﻿using GeekMDSuite.Analytics.Classification;
using GeekMDSuite.Core.Builders;
using GeekMDSuite.Core.Builders.LaboratoryData;
using GeekMDSuite.Core.Models;
using Xunit;

namespace GeekMDSuite.Analytics.UnitTests.Classification
{
    public class QuantitativeLabInterpretationTests
    {
        private readonly Patient _patient;

        public QuantitativeLabInterpretationTests()
        {
            _patient = PatientBuilder.Initialize().BuildWithoutModelValidation();
        }

        [Theory]
        [InlineData(239, GenderIdentity.Male, QuantitativeLabResult.VeryLow)]
        [InlineData(255, GenderIdentity.Male, QuantitativeLabResult.Low)]
        [InlineData(301, GenderIdentity.Male, QuantitativeLabResult.Normal)]
        [InlineData(970, GenderIdentity.Male, QuantitativeLabResult.High)]
        [InlineData(0, GenderIdentity.Female, QuantitativeLabResult.VeryLow)]
        [InlineData(7, GenderIdentity.Female, QuantitativeLabResult.Low)]
        [InlineData(59, GenderIdentity.Female, QuantitativeLabResult.Normal)]
        [InlineData(61, GenderIdentity.Female, QuantitativeLabResult.High)]
        public void QuantitativeLabTestosterone_GivenValues_ReturnsCorrectClassification(int result,
            GenderIdentity genderIdentity, QuantitativeLabResult expectedResult)
        {
            _patient.Gender = Gender.Build(genderIdentity);
            var test = Quantitative.Serum.TestosteroneTotal(result);

            var classification =
                new QuantitativeLabClassification(new QuantitativeLabClassificationParameters(test, _patient))
                    .Classification;

            Assert.Equal(expectedResult, classification);
        }
    }
}