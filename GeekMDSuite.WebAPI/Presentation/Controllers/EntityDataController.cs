﻿using System;
using GeekMDSuite.WebAPI.Core.DataAccess;
using GeekMDSuite.WebAPI.Core.DataAccess.Repositories.EntityData;
using GeekMDSuite.WebAPI.Core.Exceptions;
using GeekMDSuite.WebAPI.Core.Models;
using GeekMDSuite.WebAPI.Presentation.StatusCodeResults;
using Microsoft.AspNetCore.Mvc;

namespace GeekMDSuite.WebAPI.Presentation.Controllers
{
    [Produces("application/json", "application/xml")]
    public abstract class EntityDataController<T> : Controller where T : class, IEntity<T>
    {
        private readonly IRepository<T> _repo;

        protected readonly IUnitOfWork UnitOfWork;

        protected EntityDataController(IUnitOfWork unitOfWork)
        {
            UnitOfWork = unitOfWork;
            _repo = UnitOfWork.EntityData<T>();
        }

        [HttpGet]
        [Route("")]
        public IActionResult Get()
        {
            return NotFound("Not an API endpoint. Please see the documentation.");
        }
        
        [HttpGet]
        [Route("all/")]
        public IActionResult GetAll()
        {
            try
            {
                return Ok(_repo.All());
            }
            catch (RepositoryElementNotFoundException)
            {
                return NotFound("Empty repository.");
            }
        }

        [HttpGet("{id}")]
        public IActionResult GetByEntityId(int id)
        {
            try
            {
                return Ok(_repo.FindById(id));
            }
            catch (RepositoryElementNotFoundException)
            {
                return NotFound($"Cannot locate element with id {id}.");
            }
        }

        [HttpPost]
        public virtual IActionResult Post([FromBody] T entity)
        {
            try
            {
                _repo.Add(entity);
                UnitOfWork.Complete();
                return Ok();
            }
            catch (EntityNotUniqeException e)
            {
                return Conflict(e.Message);
            }
            catch (ArgumentNullException)
            {
                return BadRequest("A null entity was provided.");
            }
        }

        [HttpPut]
        [Route("update/")]
        public IActionResult Put([FromBody] T entity)
        {
            try
            {
                _repo.Update(entity);
                UnitOfWork.Complete();
                return Ok();
            }
            catch (RepositoryElementNotFoundException e )
            {
                return NotFound(e.Message);
            }
            catch (ArgumentNullException)
            {
                return BadRequest("A null entity was provided.");
            }
        }

        [HttpDelete]
        [Route("delete/{id}")]
        public IActionResult Delete(int id)
        {
            try
            {
                _repo.Delete(id);
                UnitOfWork.Complete();
                return Ok();
            }
            catch (RepositoryElementNotFoundException e)
            {
                return NotFound(e.Message);
            }
        }

        [HttpDelete]
        //TODO: Remove this method
        public IActionResult Delete([FromBody] int[] ids)
        {
            if (ids == null || ids.Length <= 0)
                return new BadRequestResult();
            
            try
            {
                foreach (var id in ids) _repo.Delete(id);
                UnitOfWork.Complete();
                return Ok();
            }
            catch (RepositoryElementNotFoundException e)
            {
                return NotFound(e.Message);
            }
        }
        
        public ConflictResult Conflict(string message)
        {
            return new ConflictResult(message);
        }
    }
}