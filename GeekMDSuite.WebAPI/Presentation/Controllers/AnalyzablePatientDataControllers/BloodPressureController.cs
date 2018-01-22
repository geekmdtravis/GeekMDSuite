﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using GeekMDSuite.Utilities.Extensions;
using GeekMDSuite.WebAPI.Core.DataAccess;
using GeekMDSuite.WebAPI.Core.Exceptions;
using GeekMDSuite.WebAPI.Core.Models;
using GeekMDSuite.WebAPI.Core.Presentation;
using GeekMDSuite.WebAPI.Core.Presentation.ResourceModels;
using GeekMDSuite.WebAPI.Presentation.EntityModels;
using GeekMDSuite.WebAPI.Presentation.ResourceModels;
using GeekMDSuite.WebAPI.Presentation.StubFromUserModels;
using GeekMDSuite.WebAPI.Presentation.StubModels;
using Microsoft.AspNetCore.Mvc;

namespace GeekMDSuite.WebAPI.Presentation.Controllers.AnalyzablePatientDataControllers
{
    [Produces("application/json", "application/xml")]
    public class BloodPressureController : VisitDataController
    {
        
        public BloodPressureController(IUnitOfWork unitOfWork, IMapper mapper, IErrorService errorService, IUrlHelper urlHelper) : base(unitOfWork, mapper, errorService, urlHelper)
        {
        }

        public async Task<IActionResult> GetBySearch(EntityDataFindFilter filter)
        {
            var entities = await GetFilteredBloodPressureEntities(filter);
            
            var resources = GenerateBloodPressureResources(entities);

            return Ok(resources);
        }

        public async Task<IActionResult> GetById(int id)
        {
            try
            {
                var entity = await UnitOfWork.BloodPressures.FindById(id);
                var stub = Mapper.Map<BloodPressureEntity, BloodPressureStub>(entity);
                var resource = new BloodPressureResource
                {
                    Links = GenerateGetByIdLinks(id),
                    Properties = stub
                };

                return Ok(resource);
            }
            catch (RepositoryEntityNotFoundException)
            {
                var error = ErrorService.PayloadBuilder
                    .HasErrorCode(ErrorPayloadErrorCode.RepositoryEntityNotFound)
                    .HasInternalMessage($"An blood pressure entity with the id {id} could not be located in the repository.")
                    .TellsUser("The requested blood pressure entry could not be found")
                    .Build();
                return BadRequest(error);
            }
        }

        public async Task<IActionResult> Post([FromBody] BloodPressureStubFromUser stub)
        {
            try
            {
                var entity = Mapper.Map<BloodPressureStubFromUser, BloodPressureEntity>(stub);
                await UnitOfWork.BloodPressures.Add(entity);
                await UnitOfWork.Complete();
                var url = UrlHelper.Action<BloodPressureController>(a => a.Post(stub));
                return Created(url, entity);
            }
            catch (ArgumentNullException)
            {
                var error = ErrorService.PayloadBuilder
                    .HasErrorCode(ErrorPayloadErrorCode.DataModelFromUserIsInvalid)
                    .HasInternalMessage("A null object was included in the request body and cannot be processed as a blood pressure entity") 
                    .TellsUser("The request to create a new blood pressure entry was malformed and likely empty. Please retry.")
                    .Build();
                return BadRequest(error);
            }
            catch (EntityNotUniqeException)
            {
                var error = ErrorService.PayloadBuilder
                    .HasErrorCode(ErrorPayloadErrorCode.EntityIdIsNotUniqe)
                    .HasInternalMessage($"The object provided in the request body has id {stub.Id} and already exists in the repository. Either this is not a new object, or the new object was incorrectly formatted. In order for the object to be created correctly it should be 0") 
                    .TellsUser("The request to create a new blood pressure entry was imporoperly formatted ")
                    .Build();
                return Conflict(error);
            }
        }

        public async Task<IActionResult> Put(int id, [FromBody] BloodPressureStubFromUser stub)
        {
            try
            {
                var entity = Mapper.Map<BloodPressureStubFromUser, BloodPressureEntity>(stub);
                await UnitOfWork.BloodPressures.Update(entity);
                await UnitOfWork.Complete();
                return Ok(stub);
            }
            catch (RepositoryEntityNotFoundException)
            {
                var error = ErrorService.PayloadBuilder
                    .HasErrorCode(ErrorPayloadErrorCode.RepositoryEntityNotFound)
                    .HasInternalMessage($"A blood pressure entity with Id {id} could not be located in the repository. No changes were made.")
                    .TellsUser("The request could not be processed because the blood pressure entry identified for update couldn't not be found")
                    .Build();
                return BadRequest(error);
            }
            catch (ArgumentNullException)
            {
                var error = ErrorService.PayloadBuilder
                    .HasErrorCode(ErrorPayloadErrorCode.DataModelFromUserIsInvalid)
                    .HasInternalMessage("The blood pressure data model received from client was null and could not be processed.")
                    .TellsUser("The request to createa a new blood pressure was improperly formatted")
                    .Build();
                return BadRequest(error);
            }
        }

        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                await UnitOfWork.BloodPressures.Delete(id);
                await UnitOfWork.Complete();
                return NoContent();
            }
            catch (RepositoryEntityNotFoundException)
            {
                var error = ErrorService.PayloadBuilder
                    .HasErrorCode(ErrorPayloadErrorCode.RepositoryEntityNotFound)
                    .HasInternalMessage($"A blood pressure element with Id {id} could not be located in the repository. No changes were made.")
                    .TellsUser("The requested blood pressure resource could not be found")
                    .Build();
                return BadRequest(error);
            }
        }
        
        private List<ResourceLink> GenerateGetByIdLinks(int id)
        {
            return new List<ResourceLink>
            {
                new ResourceLink
                {
                    Description = "Get blood pressure resource by it's unique identifier.",
                    Href = UrlHelper.Action<BloodPressureController>(a => a.GetById(id)),
                    HtmlMethod = HtmlMethod.Get,
                    Relationship = UrlRelationship.Self
                },
                new ResourceLink
                {
                    Description = "Delete blood pressure resource by it's unique identifier.",
                    Href = UrlHelper.Action<BloodPressureController>(a => a.Delete(id)),
                    HtmlMethod = HtmlMethod.Delete,
                    Relationship = UrlRelationship.Next
                },
                new ResourceLink
                {
                    Description = "Update the values for this existing blood pressure resource",
                    Href = UrlHelper.Action<BloodPressureController>(a => a.Put(id, null)),
                    HtmlMethod = HtmlMethod.Put,
                    Relationship = UrlRelationship.Search
                },
                new ResourceLink
                {
                    Description = "Search for blood pressures resources with filters and pagination",
                    Href = UrlHelper.Action<BloodPressureController>(a => a.GetBySearch(null)),
                    HtmlMethod = HtmlMethod.Get,
                    Relationship = UrlRelationship.Search
                },
                new ResourceLink
                {
                    Description = "Add a new blood pressure resource",
                    Href = UrlHelper.Action<BloodPressureController>(a => a.Post(null)),
                    HtmlMethod = HtmlMethod.Post,
                    Relationship = UrlRelationship.Search
                },
                new ResourceLink
                {
                    Description = "Go to the application data root",
                    Href = UrlHelper.Action<DataController>(a => a.Get()),
                    HtmlMethod = HtmlMethod.Get,
                    Relationship = UrlRelationship.Up
                }
            };
        }

        private List<ResourceLink> GenerateLinksForGetBySearch(IEntity stub)
        {
            return new List<ResourceLink>
            {
                new ResourceLink
                {
                    Description = "Get this blood pressure resource",
                    Href = UrlHelper.Action<BloodPressureController>(a => a.GetById(stub.Id)),
                    HtmlMethod = HtmlMethod.Get,
                    Relationship = UrlRelationship.Next
                },
                new ResourceLink
                {
                    Description = "Go to the application data root",
                    Href = UrlHelper.Action<DataController>(a => a.Get()),
                    HtmlMethod = HtmlMethod.Get,
                    Relationship = UrlRelationship.Up
                }
            };
        }
                
        private async Task<IEnumerable<BloodPressureEntity>> GetFilteredBloodPressureEntities(EntityDataFindFilter filter)
        {
            var entities = await UnitOfWork.BloodPressures.All();
            if (filter == null) return entities;
            if (filter.VisitGuid != null)
                entities = entities.Where(e => e.Guid == filter.VisitGuid);
            if (filter.Offset != null)
                entities = entities.Skip((int) filter.Offset);
            if (filter.Take != null)
                entities = entities.Take((int) filter.Take);

            return entities;
        }
        
        private IEnumerable<BloodPressureResource> GenerateBloodPressureResources(IEnumerable<BloodPressureEntity> entities)
        {
            var stubs = entities.Select(Mapper.Map<BloodPressureEntity, BloodPressureStub>);
            var resources = stubs.Select(stub => new BloodPressureResource
            {
                Links = GenerateLinksForGetBySearch(stub),
                Properties = stub
            });
            return resources;
        }
    }

    public class EntityDataFindFilter
    {
        public Guid? VisitGuid { get; set; }
        public int? Offset { get; set; }
        public int? Take { get; set; }

        public override string ToString()
        {
            var builder = new StringBuilder();
            if (VisitGuid.IsNotNullOrEmtpy()) builder.Append($"{nameof(VisitGuid)}={VisitGuid}");
            if (Offset != null) builder.Append($"{nameof(Offset)}={Offset}");
            if (Take != null) builder.Append($"{nameof(Take)}={Take}");

            return string.Join("&", builder);
        }
    }
}