using Web2.API.Data.Models;
using Web2.API.DTO;

namespace Web2.API.BusinessLogic
{
    public class EvenementBL : IEvenementBL
    {
        public EvenementDTO Add(EvenementDTO value)
        {
            ProcessEventValidation(value);

            value.ID = Repository.IdSequenceEvenement++;

            var eventNonDTO = ConversionVersEvenementNonDTO(value);
            Repository.Evenements.Add(eventNonDTO);

            return value;
        }

        public void Delete(int id)
        {
            var evenement = Repository.Evenements.FirstOrDefault(x => x.ID == id);
            if (evenement is not null)
            {
                Repository.Evenements.Remove(evenement);
            }
        }

        public EvenementDTO Get(int id)
        {
            var evenement = Repository.Evenements.FirstOrDefault(x => x.ID == id);

            if (evenement != null)
            {
                EvenementDTO evenementDTO = new EvenementDTO
                {
                    ID = evenement.ID,
                    Titre = evenement.Titre,
                    Description = evenement.Description,
                    Organisateur = evenement.Organisateur,
                    DateDebut = evenement.DateDebut,
                    DateFin = evenement.DateFin,
                    Adresse = evenement.Adresse,
                    Prix = evenement.Prix,
                    VilleID = evenement.VilleID,
                    IdCategorie = evenement.IdCategorie,
                };

                EvenementParticipationsDTO listeParticipationsParEventDTO = (EvenementParticipationsDTO)evenement.Participations;

                return evenementDTO;
            }

            return null;
        }

        public IEnumerable<EvenementDTO> GetList()
        {
            IEnumerable<Evenement> listeEvenements = Repository.Evenements;
            List<EvenementDTO> listeEvenementsDTO = new List<EvenementDTO>();

            foreach (Evenement evenement in listeEvenements)
            {
                EvenementDTO evenementDTO = new EvenementDTO
                {
                    ID = evenement.ID,
                    Titre = evenement.Titre,
                    Description = evenement.Description,
                    Organisateur = evenement.Organisateur,
                    DateDebut = evenement.DateDebut,
                    DateFin = evenement.DateFin,
                    Adresse = evenement.Adresse,
                    Prix = evenement.Prix,
                    VilleID = evenement.VilleID,
                    IdCategorie = evenement.IdCategorie,
                };

                EvenementParticipationsDTO listeParticipationsParEventDTO = (EvenementParticipationsDTO)evenement.Participations;

                listeEvenementsDTO.Add(evenementDTO);
            }

            return listeEvenementsDTO;
        }


        public EvenementDTO Update(int id, EvenementDTO value)
        {
            var evenement = Repository.Evenements.FirstOrDefault(x => x.ID == id);


            if (evenement == null)
            {
                throw new HttpException
                {
                    Errors = new { Errors = $"Element introuvable (id = {id})" },
                    StatusCode = StatusCodes.Status404NotFound
                };
            }

            ProcessEventValidation(value);

            EvenementDTO evenementDTO = new EvenementDTO
            {
                ID = evenement.ID,
                Titre = evenement.Titre,
                Description = evenement.Description,
                Organisateur = evenement.Organisateur,
                DateDebut = evenement.DateDebut,
                DateFin = evenement.DateFin,
                Adresse = evenement.Adresse,
                Prix = evenement.Prix,
                VilleID = evenement.VilleID,
                IdCategorie = evenement.IdCategorie,
            };

            EvenementParticipationsDTO listeParticipationsParEventDTO = (EvenementParticipationsDTO)evenement.Participations;

            return evenementDTO;
        }

        public VilleEvenementsDTO GetByVille(int villeId)
        {
            IEnumerable<Evenement> listeEvenementsParVille = Repository.Evenements.Where(x => x.VilleID == villeId);
            VilleEvenementsDTO listeEvenementsParVilleDTO = new VilleEvenementsDTO();

            foreach (Evenement evenement in listeEvenementsParVille)
            {
                EvenementDTO evenementDTO = new EvenementDTO
                {
                    ID = evenement.ID,
                    Titre = evenement.Titre,
                    Description = evenement.Description,
                    Organisateur = evenement.Organisateur,
                    DateDebut = evenement.DateDebut,
                    DateFin = evenement.DateFin,
                    Adresse = evenement.Adresse,
                    Prix = evenement.Prix,
                    VilleID = evenement.VilleID,
                    IdCategorie = evenement.IdCategorie,
                };

                EvenementParticipationsDTO listeParticipationsParEventDTO = (EvenementParticipationsDTO)evenement.Participations;

                listeEvenementsParVilleDTO.Evenements.Add(evenementDTO);
            }

            return listeEvenementsParVilleDTO;
        }

        private void ProcessEventValidation(EvenementDTO value)
        {
            string errorMsg = null;

            if (String.IsNullOrEmpty(value?.Titre))
            {
                errorMsg = "Le Titre d'un evenement est requis";
            }
            else if (String.IsNullOrEmpty(value?.Description))
            {
                errorMsg = "La description d'un evenement est requis";
            }
            else if (String.IsNullOrEmpty(value?.Organisateur))
            {
                errorMsg = "L'organisateur d'un evenement est requis";
            }
            else if (String.IsNullOrEmpty(value?.Adresse))
            {
                errorMsg = "L'adresse de rue d'un evenement est requis";
            }
            else if (!value.DateDebut.HasValue)
            {
                errorMsg = "La date de debut d'un evenement est requis";
            }
            else if (!value.DateFin.HasValue)
            {
                errorMsg = "La date de fin d'un evenement est requis";
            }
            else if (value.DateFin.Value.CompareTo(value.DateDebut.Value) <= 0)
            {
                errorMsg = "La date de fin d'un evenement ne peut pas etre avant la date de debut";
            }
            else if (value.DateDebut.Value.CompareTo(DateTime.Now) <= 0)
            {
                errorMsg = "La date de debut d'un evenement ne peut pas dans le passé";
            }
            /*else if (value?.IdCategorie?.Any() != true)
            {
                errorMsg = "Une Categorie au moins est requis pour un evenement";
            }*/
            else if (value?.VilleID is null)
            {
                errorMsg = "La  Ville  d'un evenement est requis";
            }
            else
            {
                if (!Repository.Villes.Any(x => x.ID == value.VilleID))
                {
                    errorMsg = $"La  Ville  (id = {value.VilleID}) n'existe pas";
                }
                else
                {
                    if (!Repository.Villes.Any(x => x.ID == value.IdCategorie))
                    {
                        errorMsg = $"La  category (id = {value.IdCategorie}) n'existe pas";
                        return;
                    }
                }
            }

            if (errorMsg is not null)
            {
                throw new HttpException
                {
                    Errors = new { Errors = errorMsg },
                    StatusCode = StatusCodes.Status400BadRequest
                };
            }
        }

        public Evenement ConversionVersEvenementNonDTO(EvenementDTO evenementDTO)
        {
            EvenementParticipationsDTO listeParticipationsParEventDTO = new();

            return new Evenement
            {
                ID = evenementDTO.ID,
                Titre = evenementDTO.Titre,
                Description = evenementDTO.Description,
                Organisateur = evenementDTO.Organisateur,
                DateDebut = evenementDTO.DateDebut,
                DateFin = evenementDTO.DateFin,
                Adresse = evenementDTO.Adresse,
                Prix = evenementDTO.Prix,
                VilleID = evenementDTO.VilleID,
                IdCategorie = evenementDTO.IdCategorie,
                Participations = (ICollection<Participation>)listeParticipationsParEventDTO.Participations,
            };
        }
    }
}