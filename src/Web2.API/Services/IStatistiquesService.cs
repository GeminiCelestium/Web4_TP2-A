using Web2.API.DTO;

namespace Web2.API.Services
{
    public interface IStatistiquesService
    {
        List<VilleDTO> GetVillesPopulaires();
        List<EvenementDTO> GetEvenementsRentables();
    }
}
