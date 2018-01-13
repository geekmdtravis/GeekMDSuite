﻿using System;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using GeekMDSuite.WebAPI.DataAccess.Fake;
using GeekMDSuite.WebAPI.DataAccess.Services;
using GeekMDSuite.WebAPI.Mapping;
using GeekMDSuite.WebAPI.Presentation.Controllers;
using GeekMDSuite.WebAPI.Presentation.EntityModels;
using Microsoft.AspNetCore.Mvc;
using Xunit;

namespace GeekMDSuite.WebAPI.UnitTests.Controllers
{
    public class VisitsControllerTests
    {
        public VisitsControllerTests()
        {
            Mapper.Initialize(v => v.AddProfile(new MappingProfile()));
            _controller = new VisitController(new FakeUnitOfWorkSeeded(), new NewVisitService(), Mapper.Instance);
        }

        private readonly VisitController _controller;

        [Fact]
        public async Task Add_GivenNewVisit_CreatesAVisitWithVisitGuid()
        {
            var visitDate = DateTime.Now;
            var unitOfWork = new FakeUnitOfWorkEmpty();
            var controller = new VisitController(unitOfWork, new NewVisitService(), Mapper.Instance);

            await controller.Post(new VisitEntity
            {
                Date = visitDate,
                PatientGuid = Guid.NewGuid()
            });

            var addedVisits = (await unitOfWork.Visits.All()).FirstOrDefault();

            Assert.True(addedVisits != null && addedVisits.VisitId != Guid.Empty);
            Mapper.Reset();
        }

        [Fact]
        public async Task Post_GivenNullPatient_ReturnsBadRequestObjectResult()
        {
            var result = await _controller.Post(null);

            Assert.Equal(typeof(BadRequestObjectResult), result.GetType());
            Mapper.Reset();
        }

        [Fact]
        public async Task Post_GivenPatientWithEmptyGuid_ReturnsBadRequestObjectResul()
        {
            var result = await _controller.Post(new VisitEntity
            {
                VisitId = Guid.Empty
            });

            Assert.Equal(typeof(BadRequestObjectResult), result.GetType());
            Mapper.Reset();
        }
    }
}