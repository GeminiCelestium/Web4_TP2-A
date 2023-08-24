using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Web2.API.Models;

namespace Web2.API.BusinessLogic
{
    public interface IVilleBL
    {
        public IEnumerable<Ville> GetList();
        public Ville Get(int id);

        public Ville Add(Ville value);
        public Ville Updade(int id, Ville value);
        public void Delete(int id);
    }
}
