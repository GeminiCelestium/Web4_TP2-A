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
    [Produces("application/json")]
    public class EvenementsController : ControllerBase
    {
        private readonly IEvenementBL _evenementBL;
        private readonly IMapper _mapper;

        private readonly TP2A_Context _context;

        public EvenementsController(IEvenementBL evenementBL, IMapper mapper, TP2A_Context context)
        {
            _evenementBL = evenementBL;
            _mapper = mapper;
            _context = context;
        }


        // GET: api/<EvenementsController>
        /// <summary>
        /// Lister tous les evenements de la plateforme
        /// </summary>
        /// <returns></returns>
        /// <response code="200">Retourne un evenement</response>
        [HttpGet]
        [ProducesResponseType(typeof(List<EvenementDTO>), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<IEnumerable<EvenementDTO>>> Get(string recherche, int pageIndex = 1, int pageCount = 5)
        {
            IQueryable<Evenement> query = _context.Evenements.OrderBy(e => e.DateDebut);

            if (!string.IsNullOrEmpty(recherche))
            {
                query = query.Where(e =>
                    e.Titre.Contains(recherche) ||
                    e.Description.Contains(recherche));
            }

            var evenements = await query
                .Skip((pageIndex - 1) * pageCount)
                .Take(pageCount)
                .AsNoTracking()
                .ToListAsync();

            var evenementsDTO = _mapper.Map<List<EvenementDTO>>(evenements);

            return Ok(evenementsDTO);
        }

        // GET api/evenements/5
        /// <summary>
        /// Obtenir un evenement par son ID
        /// </summary>
        /// <remarks> Sample of request:
        /// 
        ///    GET api/evenements/5
        ///     
        /// </remarks>
        /// <param name="id"> ID de l'evenement</param>
        /// <returns></returns>
        /// <response code="200">Retourne un evenement</response>
        /// <response code="404">Retourne une erreur si l'evenement est introuvable</response>
        [HttpGet("{id}")]
        [ProducesResponseType(typeof(EvenementDTO), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        public async Task<ActionResult<EvenementDTO>> Get(int id)
        {
            var evenement = await _context.Evenements.FirstOrDefaultAsync(x => x.ID == id);

            if (evenement == null)
            {
                return NotFound(new { Errors = $"Element introuvable (id = {id})" });
            }

            var evenementDTO = _mapper.Map<EvenementDTO>(evenement);
            return Ok(evenementDTO);
        }

        // POST api/evenements
        /// <summary>
        /// Creer un evenement
        /// </summary>
        /// <param name="value">Événement a créer</param>
        /// <returns></returns>
        /// <response code="201">Retourne une reponse avec un header Loaction contenant l'url vers le GET de l'evenement créé</response>
        /// <response code="400">Retourne une erreur de validation de l'evenement</response>
        [HttpPost]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [ProducesResponseType((int)HttpStatusCode.Created)]
        [Consumes(MediaTypeNames.Application.Json)]
        public ActionResult Post([FromBody] EvenementDTO value)
        {
            var evenementACreer = _mapper.Map<Evenement>(value);

            _context.Evenements.Add(evenementACreer);
            _context.SaveChanges();

            return CreatedAtAction(nameof(Get), new { id = evenementACreer.ID }, null);

        }

        // PUT api/evenements/5
        /// <summary>
        /// Modification d'un événement
        /// </summary>
        /// <param name="id">Identifiant de l'événement a modifier</param>
        /// <param name="value">Les nouvelles valeurs de l'événement</param>
        /// <returns></returns>
        /// <response code="204">Operation reussi. Reponse vide</response>
        /// <response code="400">Retourne une erreur de validation de l'evenement</response>
        /// <response code="404">Retourne une erreur si l'evenement est introuvable</response>
        [HttpPut("{id}")]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType((int)HttpStatusCode.NoContent)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [Consumes(MediaTypeNames.Application.Json)]
        public ActionResult Put(int id, [FromBody] EvenementDTO value)
        {
            var evenementAMAJ = _evenementBL.Get(id);

            if (evenementAMAJ == null)
            {
                return NotFound();
            }

            _mapper.Map(value, evenementAMAJ);

            _evenementBL.Update(id, evenementAMAJ);

            return NoContent();
        }

        // DELETE api/evenements/5
        /// <summary>
        /// Supprimer un événement
        /// </summary>
        /// <param name="id">Identifiant de l'événement a supprimer</param>
        /// <returns></returns>
        /// <response code="204">Operation reussi. Reponse vide</response>
        [HttpDelete("{id}")]
        [ProducesResponseType((int)HttpStatusCode.NoContent)]
        public ActionResult Delete(int id)
        {
            _evenementBL.Delete(id);
            return NoContent();
        }

        // DELETE api/villes/2/evenements/
        /// <summary>
        /// Liste des événements d'une ville donnée
        /// </summary>
        /// <param name="villeId">Identifiant de la ville</param>
        /// <returns>Liste de participation <see cref="Participation"/></returns>
        /// <response code="200">Lister des evenements de la ville</response>
        [HttpGet("/api/villes/{villeId}/[controller]")]
        [ProducesResponseType(typeof(List<EvenementDTO>), (int)HttpStatusCode.OK)]
        public ActionResult<VilleEvenementsDTO> GetListByVille(int villeId)
        {
            var evenements = _evenementBL.GetByVille(villeId);
            var evenementsDTO = _mapper.Map<List<EvenementDTO>>(evenements);

            return Ok(evenementsDTO);
        }
    }
}
