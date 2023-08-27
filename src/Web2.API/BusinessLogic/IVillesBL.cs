using Web2.API.Data.Models;
using Web2.API.DTO;

namespace Web2.API.BusinessLogic
{
    public interface IVilleBL
    {
        public IEnumerable<VilleDTO> GetList();
        public VilleDTO Get(int id);

        public VilleDTO Add(VilleDTO value);
        public VilleDTO Update(int id, VilleDTO value);
        public void Delete(int id);

        public Ville ConversionVersVilleNonDTO(VilleDTO villeDTO);
    }
}
