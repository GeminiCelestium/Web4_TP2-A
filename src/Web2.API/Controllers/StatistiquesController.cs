using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Net.Mime;
using Web2.API.BusinessLogic;
using Web2.API.Data;
using Web2.API.Data.Models;
using Web2.API.DTO;
using Web2.API.Services;

namespace Web2.API.Controllers
{
    [ApiController]
    [Route("api/statistiques")]
    public class StatistiquesController : ControllerBase
    {
        private readonly IStatistiquesService _statistiquesService;
        private readonly IMapper _mapper;

        public StatistiquesController(IStatistiquesService statistiquesService, IMapper mapper)
        {
            _statistiquesService = statistiquesService;
            _mapper = mapper;
        }

        [HttpGet("villes-populaires")]
        public IActionResult GetVillesPopulaires()
        {
            var villesPopulaires = _statistiquesService.GetVillesPopulaires();
            var villesPopulairesDTO = _mapper.Map<List<VillePopulaireDTO>>(villesPopulaires);
            return Ok(villesPopulairesDTO);
        }

        [HttpGet("events-rentables")]
        public IActionResult GetEventsRentables()
        {
            var eventsRentables = _statistiquesService.GetEvenementsRentables();
            var eventsRentablesDTO = _mapper.Map<List<EventRentableDTO>>(eventsRentables);
            return Ok(eventsRentablesDTO);
        }
    }
}
