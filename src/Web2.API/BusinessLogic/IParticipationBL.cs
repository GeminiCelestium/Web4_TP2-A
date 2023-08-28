using Web2.API.Data.Models;
using Web2.API.DTO;

namespace Web2.API.BusinessLogic
{
    public interface IParticipationBL
    {
        public IEnumerable<ParticipationDTO> GetList();
        public ParticipationDTO Get(int id);

        public ParticipationDTO Add(ParticipationDTO value);
        public void Delete(int id);

        public bool GetStatus(int id);

        public EvenementParticipationsDTO GetByEvent(int eventId);

        public Participation ConversionVersParticipationNonDTO(ParticipationDTO participationDTO);
    }
}
