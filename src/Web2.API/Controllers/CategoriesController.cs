using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
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
    [Produces("application/json")]
    public class CategoriesController : ControllerBase
    {
        private readonly ICategoryBL _categoryBL;
        private readonly IMapper _mapper;

        private readonly TP2A_Context _context;

        public CategoriesController(ICategoryBL categoryBL, IMapper mapper, TP2A_Context context)
        {
            _categoryBL = categoryBL;
            _mapper = mapper;
            _context = context;
        }

        // GET: api/<CategoriesController>
        [HttpGet]
        [ProducesResponseType(typeof(List<CategorieDTO>), StatusCodes.Status200OK)]
        public async Task<IEnumerable<CategorieDTO>> Get()
        {
            var categories = await _context.Categories
                .Include(std => std.Evenements)
                .ToListAsync();

            var categoriesDTO = _mapper.Map<IEnumerable<CategorieDTO>>(categories);
            return categoriesDTO;
        }

        // GET api/<CategoriesController>/5
        [HttpGet("{id}")]
        [ProducesResponseType(typeof(CategorieDTO), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<CategorieDTO> Get(int id)
        {
            var categorie = await _context.Categories.Include(std => std.Evenements).FirstOrDefaultAsync(x => x.ID == id);

            var categorieDTO = _mapper.Map<CategorieDTO>(categorie);
            return categorieDTO;
        }

        // POST api/<CategoriesController>
        [HttpPost]
        [ProducesResponseType(typeof(CategorieDTO), StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [Consumes(MediaTypeNames.Application.Json)]
        public async Task<IActionResult> Post([FromBody] CategorieDTO categorieDTO)
        {
            var categorie = _mapper.Map<Categorie>(categorieDTO);
            _context.Categories.Add(categorie);
            await _context.SaveChangesAsync();

            var CategorieDTOCreee = _mapper.Map<CategorieDTO>(categorie);
            return CreatedAtAction(nameof(Get), new { id = CategorieDTOCreee.ID }, CategorieDTOCreee);
        }

        // PUT api/<CategoriesController>/5
        [HttpPut("{id}")]
        [ProducesResponseType(typeof(CategorieDTO), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [Consumes(MediaTypeNames.Application.Json)]
        public async Task<IActionResult> Put(int id, [FromBody] CategorieDTO categorieDTO)
        {
            var categorieAMAJ = await _context.Categories.Include(std => std.Evenements).FirstOrDefaultAsync(x => x.ID == id);

            if (categorieAMAJ != null)
            {
                _mapper.Map(categorieDTO, categorieAMAJ);
                await _context.SaveChangesAsync();
                return Ok(categorieDTO);
            }
            else
            {
                return NotFound();
            }
        }

        // PUT api/<CategoriesController>/5
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        public async Task<IActionResult> Delete(int id)
        {
            var categorie = await _context.Categories.FindAsync(id);

            if (categorie != null)
            {
                _context.Categories.Remove(categorie);
                await _context.SaveChangesAsync();
                return NoContent();
            }
            else
            {
                return Conflict();
            }
        }

    }
}
