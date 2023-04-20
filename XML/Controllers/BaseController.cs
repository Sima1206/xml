﻿using Microsoft.AspNetCore.Mvc;
using XML.Configuration;
using XML.Services;

namespace XML.Controllers
{
    public class BaseController<TEntity> : ControllerBase where TEntity : class
    {
        protected ProjectConfiguration _configuration;
        protected BaseService<TEntity> _baseService;

        public BaseController(ProjectConfiguration configuration)
        {
            _configuration = configuration;
            _baseService = new BaseService<TEntity>(configuration);
        }

        [HttpGet("{id}")]

        public virtual IActionResult Get(int id)
        {
            TEntity entity = _baseService.Get(id);

            if (entity == null)
            {
                return BadRequest();
            }

            return Ok(entity);
        }

        [HttpPost]
        public virtual IActionResult Add(TEntity entity)
        {
            if (entity == null)
            {
                return BadRequest();
            }

            TEntity response = _baseService.Add(entity);

            return Ok(response);
        }

        [HttpPut("{id}")]
        public IActionResult Update(int id, TEntity entity)
        {
            if (entity == null)
            {
                return BadRequest();
            }

            bool response = _baseService.Update(id, entity);

            return Ok(response);
        }
        [HttpDelete("{id}")]
        public virtual IActionResult Delete(int id)
        {
            bool response = _baseService.Delete(id);

            return Ok(response);
        }
    }
}

