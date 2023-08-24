using Microsoft.AspNetCore.Http;
using System.Collections.Generic;
using System.Linq;
using Web2.API.Models;

namespace Web2.API.BusinessLogic
{
    public class VillesBL : IVilleBL
    {
        public Ville Add(Ville value)
        {

            if (value == null )
            {
                throw new HttpException 
                { 
                    Errors = new { Errors = "Parametres d'entrés non valides" },
                    StatusCode = StatusCodes.Status400BadRequest
                };
            }

            value.ID = Repository.IdSequenceVille++;
            Repository.Villes.Add(value);

            return value;
        }

        public IEnumerable<Ville> GetList()
        {
            return Repository.Villes;
        }

        public Ville Get(int id)
        {
            return Repository.Villes.FirstOrDefault(x => x.ID == id);
        }

        public Ville Updade(int id, Ville value)
        {
            if (value == null)
            {
                throw new HttpException
                {
                    Errors = new { Errors = "Parametres d'entrés non valides" },
                    StatusCode = StatusCodes.Status400BadRequest
                };
            }

            var ville = Repository.Villes.FirstOrDefault(x => x.ID == id);


            if (ville == null)
            {
                throw new HttpException
                {
                    Errors = new { Errors = $"Element introuvable (id = {id})" },
                    StatusCode = StatusCodes.Status400BadRequest
                };
            }

            ville.Name = value.Name;
            ville.Region = value.Region;

            return ville;
        }

        public void Delete(int id)
        {
            var ville = Repository.Villes.Where(x => x.ID == id).FirstOrDefault();
            if (ville != null)
            {
                if (Repository.Evenements.Any(x => x.VilleID == ville.ID))
                {
                    throw new HttpException
                    {
                        Errors = new { Error = "Il n'est pas possible de supprimer une ville lié a au moins un evenement" },
                        StatusCode = StatusCodes.Status409Conflict
                    };

                }
                Repository.Villes.Remove(ville);
            }
        }
    }
}
