using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Web2.API.BusinessLogic;
using Web2.API.Models;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Web2.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VillesController : ControllerBase
    {
        private readonly IVilleBL _villeBL;

        public VillesController(IVilleBL villeBL)
        {
            _villeBL = villeBL;
        }



        // GET: api/<VillesController>
        [HttpGet]
        public IEnumerable<Ville> Get()
        {
            return _villeBL.GetList();
        }

        // GET api/<VillesController>/5
        [HttpGet("{id}")]
        public ActionResult Get(int id)
        {
            var ville = _villeBL.Get(id);
            return ville is null ? NotFound(new { Errors = $"Element introuvable (id = {id})" }) : Ok(ville);
        }

        // POST api/<VillesController>
        [HttpPost]
        public ActionResult Post([FromBody] Ville value)
        {
            value = _villeBL.Add(value);
            return CreatedAtAction(nameof(Get), new { id = value.ID }, null);
        }

        // PUT api/<VillesController>/5
        [HttpPut("{id}")]
        public ActionResult Put(int id, [FromBody] Ville value)
        {
            _villeBL.Updade(id, value);
            return NoContent();
        }

        // DELETE api/<VillesController>/5
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        public ActionResult Delete(int id)
        {
            _villeBL.Delete(id);
            return NoContent();
        }

    }
}
