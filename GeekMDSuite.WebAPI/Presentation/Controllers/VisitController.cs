﻿using System;
using System.IO;
using System.Threading.Tasks;
using GeekMDSuite.WebAPI.Core.DataAccess;
using GeekMDSuite.WebAPI.Core.DataAccess.Services;
using GeekMDSuite.WebAPI.Core.Exceptions;
using GeekMDSuite.WebAPI.Presentation.EntityModels;
using Microsoft.AspNetCore.Mvc;

namespace GeekMDSuite.WebAPI.Presentation.Controllers
{
    [Produces("application/json", "application/xml")]
    public class VisitController : VisitDataController<VisitEntity>
    {
        private readonly INewVisitService _newVisitService;

        public VisitController(IUnitOfWork unitOfWork, INewVisitService newVisitService) : base(unitOfWork)
        {
            _newVisitService = newVisitService;
        }

        public override async Task<IActionResult> Post(VisitEntity visitEntity)
        {
            try
            {
                var newVisit = await _newVisitService
                    .WithUnitOfWork(UnitOfWork)
                    .GenerateUsing(visitEntity);

                await UnitOfWork.Visits.Add(newVisit);
                await UnitOfWork.Complete();
                return Ok();
            }
            catch (InvalidDataException)
            {
                return BadRequest("PatientGuid is empty.");
            }
            catch (ArgumentNullException)
            {
                return BadRequest($"{nameof(VisitEntity)} is malformed.");
            }
        }

        // GET api/visits/bymrn/"guid"
        [HttpGet("bymrn/{mrn}")]
        public async Task<IActionResult> GetByMedicalRecordNumber(string mrn)
        {
            try
            {
                return Ok(await UnitOfWork.Visits.FindByMedicalRecordNumber(mrn));
            }
            catch (ArgumentNullException)
            {
                return BadRequest("An empty medical record number was provided.");
            }
            catch (RepositoryElementNotFoundException e)
            {
                return NotFound(e.Message);
            }
        }

        // GET api/visits/byname
        [HttpGet("byname/{name}")]
        public async Task<IActionResult> GetByName(string name)
        {
            try
            {
                return Ok(await UnitOfWork.Visits.FindByName(name));
            }
            catch (RepositoryElementNotFoundException e)
            {
                return NotFound(e.Message);
            }
            catch (ArgumentNullException)
            {
                return BadRequest("An empty name was provided.");
            }
        }

        // GET api/visits/bydob/"dateOfBirth"
        [HttpGet("bydob/{dateOfBirth}")]
        public async Task<IActionResult> GetByDateOfBirth(string dateOfBirth)
        {
            try
            {
                var parsedDob = DateTime.Parse(dateOfBirth);
                return Ok(await UnitOfWork.Visits.FindByDateOfBirth(parsedDob));
            }
            catch (FormatException e)
            {
                return BadRequest(e.Message);
            }
            catch (ArgumentOutOfRangeException e)
            {
                return BadRequest(e.Message);
            }
            catch (RepositoryElementNotFoundException e)
            {
                return NotFound(e.Message);
            }
        }
    }
}