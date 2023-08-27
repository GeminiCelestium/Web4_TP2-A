using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Net;
using System.Net.Mime;
using Web2.API.BusinessLogic;
using Web2.API.Data;
using Web2.API.Data.Models;
using Web2.API.DTO;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Web2.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VillesController : ControllerBase
    {
        private readonly IVilleBL _villeBL;
        private readonly IMapper _mapper;

        private readonly TP2A_Context _context;

        public VillesController(IVilleBL villeBL, IMapper mapper, TP2A_Context context)
        {
            _villeBL = villeBL;
            _mapper = mapper;
            _context = context;
        }


        // GET: api/<VillesController>
        [HttpGet]
        public ActionResult<IEnumerable<VilleDTO>> Get()
        {
            var villes = _villeBL.GetList();

            var villesDTO = _mapper.Map<IEnumerable<VilleDTO>>(villes);
            return Ok(villesDTO);
        }

        // GET api/<VillesController>/5
        [HttpGet("{id}")]
        public ActionResult<VilleDTO> Get(int id)
        {
            var ville = _villeBL.Get(id);

            if (ville is null)
            {
                return NotFound(new { Errors = $"Element introuvable (id = {id})" });
            }

            var villeDTO = _mapper.Map<VilleDTO>(ville);
            return Ok(villeDTO);
        }

        // POST api/<VillesController>
        [HttpPost]
        public ActionResult Post([FromBody] VilleDTO value)
        {
            var VilleAAjouter = _mapper.Map<Ville>(value);

            _context.Villes.Add(VilleAAjouter);
            _context.SaveChanges();

            return CreatedAtAction(nameof(Get), new { id = VilleAAjouter.ID }, null);

            //var ville = _mapper.Map<Ville>(villeDTO);
            //ville = _villeBL.Add(ville);
            //var createdVilleDTO = _mapper.Map<VilleDTO>(ville);
            //return CreatedAtAction(nameof(Get), new { id = createdVilleDTO.ID }, createdVilleDTO);
        }

        // PUT api/<VillesController>/5
        [HttpPut("{id}")]
        public ActionResult Put(int id, [FromBody] VilleDTO value)
        {
            var villeAMAJ = _villeBL.Get(id);

            if (villeAMAJ == null)
            {
                return NotFound();
            }

            _mapper.Map(value, villeAMAJ);

            _villeBL.Update(id, villeAMAJ);

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
