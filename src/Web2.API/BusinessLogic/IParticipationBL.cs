using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Web2.API.Models;

namespace Web2.API.BusinessLogic
{
    public interface IParticipationBL
    {
        public IEnumerable<Participation> GetList();
        public Participation Get(int id);

        public Participation Add(Participation value);
        public void Delete(int id);

        public bool GetStatus(int id);

        public IEnumerable<Participation> GetByEvent(int eventId);
    }
}
