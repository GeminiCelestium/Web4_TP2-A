using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mime;
using System.Threading.Tasks;
using Web2.API.BusinessLogic;
using Web2.API.DTO;
using Web2.API.Models;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Web2.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Produces("application/json")]
    public class CategoriesController : ControllerBase
    {
        private readonly ICategoryBL _categoryBL;

        public CategoriesController(ICategoryBL categoryBL)
        {
            _categoryBL = categoryBL;
        }

        // GET: api/<CategoriesController>
        [HttpGet]
        [ProducesResponseType(typeof(List<CategorieDTO>), StatusCodes.Status200OK)]
        public IEnumerable<CategorieDTO> Get()
        {
            return _categoryBL.GetList();
        }

        // GET api/<CategoriesController>/5
        [HttpGet("{id}")]
        [ProducesResponseType(typeof(CategorieDTO), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult Get(int id)
        {
            var category = _categoryBL.Get(id);
            return category is null ? NotFound(new { Errors = $"Element introuvable (id = {id})" }) : Ok(category);
        }

        // POST api/<CategoriesController>
        [HttpPost]
        [ProducesResponseType(typeof(CategorieDTO), StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [Consumes(MediaTypeNames.Application.Json)]
        public ActionResult Post([FromBody] CategorieDTO value)
        {
            value = _categoryBL.Add(value);
            return CreatedAtAction(nameof(Get), new { id = value.ID }, value);
        }

        // PUT api/<CategoriesController>/5
        [HttpPut("{id}")]
        [ProducesResponseType(typeof(CategorieDTO), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [Consumes(MediaTypeNames.Application.Json)]
        public ActionResult Put(int id, [FromBody] CategorieDTO value)
        {
            value = _categoryBL.Updade(id, value);
            return Ok(value);
        }

        // PUT api/<CategoriesController>/5
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        public ActionResult Delete(int id)
        {
            _categoryBL.Delete(id);
            return NoContent();
        }

    }
}
