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
        private readonly TP2A_Context _context;
        private readonly IMapper _mapper;

        public CategoriesController(ICategoryBL categoryBL, TP2A_Context context, IMapper mapper)
        {
            _categoryBL = categoryBL;
            _context = context;
            _mapper = mapper;
        }

        // GET: api/<CategoriesController>
        [HttpGet]
        [ProducesResponseType(typeof(List<CategorieDTO>), StatusCodes.Status200OK)]
        public async Task<IEnumerable<CategorieDTO>> Get(int pageIndex = 1, int pageCount = 5)
        {
            var categories = await _context.Categories.Include(std => std.Evenements)
                .Skip((pageIndex - 1) * pageCount)
                .Take(pageCount)
                .AsNoTracking()
                .ToListAsync();

            return _mapper.Map<IList<Categorie>, IEnumerable<CategorieDTO>>(categories);
        }

        // GET api/<CategoriesController>/5
        [HttpGet("{id}")]
        [ProducesResponseType(typeof(CategorieDTO), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<CategorieDTO> Get(int id)
        {
            var categorie = await _context.Categories.Include(std => std.Evenements).FirstOrDefaultAsync(x => x.ID == id);

            return _mapper.Map<CategorieDTO>(categorie);
        }

        // POST api/<CategoriesController>
        [HttpPost]
        [ProducesResponseType(typeof(CategorieDTO), StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [Consumes(MediaTypeNames.Application.Json)]
        public async Task Post([FromBody] CategorieDTO categorie)
        {
            _context.Categories.Add(new Categorie { ID = categorie.ID, Name = categorie.Name, });
            await _context.SaveChangesAsync();
        }

        // PUT api/<CategoriesController>/5
        [HttpPut("{id}")]
        [ProducesResponseType(typeof(CategorieDTO), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [Consumes(MediaTypeNames.Application.Json)]
        public async Task Put(int id, [FromBody] CategorieDTO categorie)
        {
            var categorieAMAJ = _context.Categories.Include(std => std.Evenements).FirstOrDefault(x => x.ID == id);

            categorieAMAJ.ID = categorie.ID;
            categorieAMAJ.Name = categorie.Name;

            await _context.SaveChangesAsync();
        }

        // PUT api/<CategoriesController>/5
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        public async Task Delete(int id)
        {
            var categorie = _context.Categories.Find(id);
            _context.Categories.Remove(categorie);
            await _context.SaveChangesAsync();
        }

    }
}
