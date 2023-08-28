using Web2.API.Data.Models;
using Web2.API.DTO;

namespace Web2.API.BusinessLogic
{
    public class VillesBL : IVilleBL
    {
        public VilleDTO Add(VilleDTO value)
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

            var villeNonDTO = ConversionVersVilleNonDTO(value); 
            Repository.Villes.Add(villeNonDTO);

            return value;
        }

        public IEnumerable<VilleDTO> GetList()
        {
            IEnumerable<Ville> listeVilles = Repository.Villes;
            List<VilleDTO> listeVillesDTO = new List<VilleDTO>();

            foreach (Ville ville in listeVilles)
            {
                VilleDTO villeDTO = new VilleDTO
                {
                    ID = ville.ID,
                    Name = ville.Name,
                    Region = (DTO.Region)ville.Region,
                };

                VilleEvenementsDTO listeEventsParVilleDTO = (VilleEvenementsDTO)ville.Evenements;

                listeVillesDTO.Add(villeDTO);
            }

            return listeVillesDTO;
        }

        public VilleDTO Get(int id)
        {
            var ville = Repository.Villes.FirstOrDefault(x => x.ID == id);

            if (ville != null)
            {
                VilleDTO villeDTO = new VilleDTO
                {
                    ID = ville.ID,
                    Name = ville.Name,
                    Region = (DTO.Region)ville.Region,
                };

                VilleEvenementsDTO listeEventsParVilleDTO = (VilleEvenementsDTO)ville.Evenements;

                return villeDTO;
            }

            return null;
        }

        public VilleDTO Update(int id, VilleDTO value)
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

            VilleDTO villeDTO = new VilleDTO
            {
                ID= ville.ID,
                Name = ville.Name,
                Region = (DTO.Region)ville.Region,
            };

            VilleEvenementsDTO listeVillesParEventDTO = (VilleEvenementsDTO)ville.Evenements;

            return villeDTO;
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

        public Ville ConversionVersVilleNonDTO(VilleDTO villeDTO)
        {
            VilleEvenementsDTO listeEvenementsParVilleDTO = new();

            return new Ville
            {
                ID = villeDTO.ID,
                Name = villeDTO.Name,
                Region = (Data.Models.Region)villeDTO.Region,
                Evenements = (ICollection<Evenement>)listeEvenementsParVilleDTO.Evenements,
            };
        }
    }
}
