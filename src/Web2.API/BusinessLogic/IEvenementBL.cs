using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Web2.API.Models;

namespace Web2.API.BusinessLogic
{
    public interface IEvenementBL
    {
        public IEnumerable<Evenement> GetList();
        public Evenement Get(int id);

        public Evenement Add(Evenement value);
        public Evenement Updade(int id, Evenement value);
        public void Delete(int id);

        public IEnumerable<Evenement> GetByVille(int villeId);
    }
}
