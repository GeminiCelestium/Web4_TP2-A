using AutoMapper;
using Web2.API.Data.Models;
using Web2.API.DTO;

namespace Web2.API
{
    public class Web2APIMappingProfile : Profile
    {
        public Web2APIMappingProfile()
        {
            CreateMap<Categorie, CategorieDTO>();
            CreateMap<Categorie, CategorieEvenementsDTO>();
            CreateMap<Ville, VilleDTO>();
            CreateMap<Ville, VilleEvenementsDTO>();
            CreateMap<Evenement, EvenementDTO>();
            CreateMap<Evenement, EvenementParticipationsDTO>();
            CreateMap<Participation, ParticipationDTO>();

            
        }
    }
}