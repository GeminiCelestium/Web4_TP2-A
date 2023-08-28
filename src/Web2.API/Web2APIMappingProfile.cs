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
            CreateMap<Ville, VilleDTO>();
            CreateMap<Evenement, EvenementDTO>();
            CreateMap<Participation, ParticipationDTO>();           
        }
    }
}