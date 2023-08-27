using Web2.API.Data.Models;
using Web2.API.DTO;

namespace Web2.API.BusinessLogic
{
    public interface IEvenementBL
    {
        public IEnumerable<EvenementDTO> GetList();
        public EvenementDTO Get(int id);
        public EvenementDTO Add(EvenementDTO value);
        public EvenementDTO Update(int id, EvenementDTO value);
        public void Delete(int id);

        public VilleEvenementsDTO GetByVille(int villeId);

        public Evenement ConversionVersEvenementNonDTO(EvenementDTO evenementDTO);
    }
}
