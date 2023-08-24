using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mime;
using System.Threading.Tasks;
using Web2.API.BusinessLogic;
using Web2.API.Models;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Web2.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Produces("application/json")]
    public class ParticipationsController : ControllerBase
    {
        private readonly IParticipationBL _participationBL;

        public ParticipationsController(IParticipationBL participationBL)
        {
            _participationBL = participationBL;
        }



        // GET: api/<ParticipationsController>
        /// <summary>
        /// Lister toutes les participations
        /// </summary>
        /// <returns></returns>
        /// <response code="200">Retourne une liste de participation</response>
        [HttpGet]
        [ProducesResponseType(typeof(List<Participation>), StatusCodes.Status200OK)]
        public ActionResult<IEnumerable<Participation>> Get()
        {
            return Ok(_participationBL.GetList());
        }

        // GET api/<ParticipationsController>/5
        /// <summary>
        /// Obtenir les détails d'une participation a partir de son id
        /// </summary>
        /// <param name="id">Identifiant de la participation</param>
        /// <returns><see cref="Participation"/></returns>
        /// <response code="200">Retourne une participation</response>
        /// <response code="404">Retourne une erreure si la participation est introuvable</response>
        [HttpGet("{id}")]
        [ProducesResponseType(typeof(Participation), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<Participation> Get(int id)
        {
            var participation = _participationBL.Get(id);

            return participation != null ? Ok(participation) : NotFound(new { Errors = $"Element introuvable (id = {id})" });
        }

        // POST api/<ParticipationsController>
        /// <summary>
        /// Permet d'ajouter un participation
        /// </summary>
        /// <param name="value"><see cref="Participation"/> Nouvelle participation</param>
        /// <returns></returns>
        /// <response code="202">Retourne une reponse avec une entete Location contenant l'uri de verification du status de la resource</response>
        /// <response code="400">Retourne une erreure de validation</response>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status202Accepted)]
        [Consumes(MediaTypeNames.Application.Json)]
        public ActionResult Post([FromBody] Participation value)
        {
            value = _participationBL.Add(value);

            return new AcceptedResult { Location = Url.Action(nameof(Status), new { id = value.ID }) };
        }

        // GET api/<ParticipationsController>/5/status
        /// <summary>
        /// Verifier le status de traitement de la creation/ajout d'une participation
        /// </summary>
        /// <param name="id">Identifiant de la participation</param>
        /// <returns>retourne le status en tant que string</returns>
        /// <response code="200">Retourne le status d'une participation qui est en attente de validation</response>
        /// <response code="303">Redirige vers GET d'une participation deja validé</response>
        [HttpGet("{id}/status")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status303SeeOther)]
        public ActionResult Status(int id)
        {
            if (_participationBL.GetStatus(id)) {
                Response.Headers.Add("Location", Url.Action(nameof(Get), new { id = id }));
                return new StatusCodeResult(StatusCodes.Status303SeeOther);
            }

            return Ok(new { status = "Validation en attente" });
        }

        // DELETE api/<ParticipationsController>/5
        /// <summary>
        /// Supprimer une participation
        /// </summary>
        /// <param name="id">Identifiant de la participation</param>
        /// <returns></returns>
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public ActionResult Delete(int id)
        {
            _participationBL.Delete(id);
            return NoContent();
        }

        // GET api/<AutomobilesController>/5
        /// <summary>
        /// Liste des Participations d'un événement
        /// </summary>
        /// <param name="eventId">Identifiant de l'evenement</param>
        /// <returns></returns>
        /// <response code="200">Retourne la liste des partcipations</response>
        [HttpGet("/api/evenements/{eventId}/[controller]")]
        [ProducesResponseType(typeof(List<Participation>), StatusCodes.Status200OK)]
        public ActionResult<IEnumerable<Participation>> GetByEvent(int eventId)
        {
            return Ok(_participationBL.GetByEvent(eventId));
        }
    }
}
