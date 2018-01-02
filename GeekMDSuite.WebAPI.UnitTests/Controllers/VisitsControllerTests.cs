﻿using System;
using GeekMDSuite.WebAPI.DataAccess.Fake;
using GeekMDSuite.WebAPI.Presentation.Controllers;
using Microsoft.AspNetCore.Mvc;
using Xunit;

namespace GeekMDSuite.WebAPI.UnitTests.Controllers
{
    public class VisitsControllerTests
    {
        [Fact]
        public void GetByMedicalRecordNumber_GivenEmptyString_ReturnsBadRequestObjectResult()
        {
            var result = _controller.GetByMedicalRecordNumber(string.Empty);
            Assert.Equal(typeof(BadRequestResult), result.GetType());
        }
        
        [Fact]
        public void GetByMedicalRecordNumber_GivenRandomString_ReturnsNotFoundResult()
        {
            var result = _controller.GetByMedicalRecordNumber(Guid.NewGuid().ToString());
            Assert.Equal(typeof(NotFoundResult), result.GetType());
        }
        
        [Fact]
        public void GetByMedicalRecordNumber_GivenMedicalNumberThatExistsInContext_ReturnsOkObjectResult()
        {
            var result = _controller.GetByMedicalRecordNumber(FakeGeekMdSuiteContextBuilder.BruceWaynesMedicalRecordNumber);
            Assert.Equal(typeof(OkObjectResult), result.GetType());
        }
        
        [Fact]
        public void GetByName_GivenEmptyString_ReturnsBadRequestObjectResult()
        {
            var result = _controller.GetByName(string.Empty);
            Assert.Equal(typeof(BadRequestResult), result.GetType());
        }
        
        [Fact]
        public void GetByName_GivenRandomString_ReturnsNotFoundResult()
        {
            var result = _controller.GetByName(Guid.NewGuid().ToString());
            Assert.Equal(typeof(NotFoundResult), result.GetType());
        }
        
        [Fact]
        public void GetByName_GivenMedicalNumberThatExistsInContext_ReturnsOkObjectResult()
        {
            var result = _controller.GetByName("Bruce Wayne");
            Assert.Equal(typeof(OkObjectResult), result.GetType());
        }
        
        [Fact]
        public void GetByDateOfBirth_GivenAgeGreaterThan150Years_ReturnsBadRequestObjectResult()
        {
            var result = _controller.GetByDateOfBirth(DateTime.Now.AddYears(-151).ToShortDateString());
            Assert.Equal(typeof(BadRequestResult), result.GetType());
        }
        
        [Fact]
        public void GetByDateOfBirth_GivenAgeOfZeroOrNegative_ReturnsBadRequestObjectResult()
        {
            var result = _controller.GetByDateOfBirth(DateTime.Now.AddYears(1).ToShortDateString());
            Assert.Equal(typeof(BadRequestResult), result.GetType());
        }
        
        [Fact]
        public void GetByDateOfBirth_GivenDateOfBirthNotInRepository_ReturnsNotFoundResult()
        {
            var result = _controller.GetByDateOfBirth(new DateTime(2000, 1, 1).ToShortDateString());
            Assert.Equal(typeof(NotFoundResult), result.GetType());
        }
        
        [Fact]
        public void GetByDateOfBirth_GivenDateOfBirthThatExistsInContext_ReturnsOkObjectResult()
        {
            var result = _controller.GetByDateOfBirth(new DateTime(1900, 1, 1).ToShortDateString());
            Assert.Equal(typeof(OkObjectResult), result.GetType());
        }
        
        private readonly VisitsController _controller = new VisitsController(new FakeUnitOfWorkSeeded());
    }
}