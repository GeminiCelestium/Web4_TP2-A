using System.Text.RegularExpressions;
using Web2.API.Data.Models;
using Web2.API.DTO;

namespace Web2.API.BusinessLogic
{
    public class ParticipationBL : IParticipationBL
    {
        public ParticipationDTO Add(ParticipationDTO value)
        {
            ProcessParticipationValidation(value);

            value.ID = Repository.IdSequenceParticipation++;
            value.IsValid = false;
            value.Email = value.Email.Trim();
            value.IsValid = false;

            var participationNonDTO = ConversionVersParticipationNonDTO(value);
            Repository.Participations.Add(participationNonDTO);

            return value;
        }

        public void Delete(int id)
        {
            var participation = Repository.Participations.FirstOrDefault(x => x.ID == id);

            if (participation != null)
            {
                Repository.Participations.Remove(participation);
            }
        }

        public ParticipationDTO Get(int id)
        {
            var participation = Repository.Participations.FirstOrDefault(x => x.ID == id);

            if (participation != null)
            {
                ParticipationDTO participationDTO = new ParticipationDTO
                {
                    ID = participation.ID,
                    Email = participation.Email,
                    Nom = participation.Nom,
                    Prenom = participation.Prenom,
                    NombrePlace = participation.NombrePlace,
                    EvenementId = participation.EvenementId,
                    IsValid = participation.IsValid,
                };

                return participationDTO;
            }

            return null;
        }

        public IEnumerable<ParticipationDTO> GetList()
        {
            IEnumerable<Participation> listeParticipations = Repository.Participations;
            List<ParticipationDTO> listeParticipationsDTO = new List<ParticipationDTO>();

            foreach (Participation participation in listeParticipations)
            {
                ParticipationDTO participationDTO = new ParticipationDTO
                {
                    ID = participation.ID,
                    Email = participation.Email,
                    Nom = participation.Nom,
                    Prenom = participation.Prenom,
                    NombrePlace = participation.NombrePlace,
                    EvenementId = participation.EvenementId,
                    IsValid = participation.IsValid,
                };

                listeParticipationsDTO.Add(participationDTO);
            }

            return listeParticipationsDTO;
        }

        public EvenementParticipationsDTO GetByEvent(int eventId)
        {
            IEnumerable<Participation> listeParticipationsParEvent = Repository.Participations.Where(x => x.EvenementId == eventId);
            EvenementParticipationsDTO listeParticipationsParEventDTO = new EvenementParticipationsDTO();

            foreach (Participation participation in listeParticipationsParEvent)
            {
                ParticipationDTO participationDTO = new ParticipationDTO
                {
                    ID = participation.ID,
                    Email = participation.Email,
                    Nom = participation.Nom,
                    Prenom = participation.Prenom,
                    NombrePlace = participation.NombrePlace,
                    EvenementId = participation.EvenementId,
                    IsValid = participation.IsValid,
                };

                listeParticipationsParEventDTO.Participations.Add(participationDTO);
            }

            return listeParticipationsParEventDTO;
        }

        public bool GetStatus(int id)
        {
            var participation = Repository.Participations.FirstOrDefault(x => x.ID == id);
            
            if (participation == null)
            {
                throw new HttpException
                {
                    Errors = new { Errors = $"Element introuvable (id = {id})" },
                    StatusCode = StatusCodes.Status400BadRequest
                };
            }

            ParticipationDTO participationDTO = new ParticipationDTO
            {
                ID = participation.ID,
                Email = participation.Email,
                Nom = participation.Nom,
                Prenom = participation.Prenom,
                NombrePlace = participation.NombrePlace,
                EvenementId = participation.EvenementId,
                IsValid = participation.IsValid,
            };

            VerifyParticipation(participationDTO);

            return participation.IsValid;
        }

        private void VerifyParticipation(ParticipationDTO participationDTO)
        {
            if (!participationDTO.IsValid)
            {
                var isValid = new Random().Next(1, 10) > 5 ? true : false;//Simuler la validation externe;
                participationDTO.IsValid = isValid;
            }

        }

        private void ProcessParticipationValidation(ParticipationDTO value)
        {
            string errorMsg = null;

            if (String.IsNullOrEmpty(value?.Nom))
            {
                errorMsg = "Le nom du participant est requis";
            }
            else if (String.IsNullOrEmpty(value?.Prenom))
            {
                errorMsg = "Le prenom du participant  est requis";
            }
            else if (!(value?.NombrePlace >= 0))
            {
                errorMsg = "Le nombre de place d'une participation doit etre valide";
            }
            else if (!IsValidEmail(value?.Email))
            {
                errorMsg = "Une adresse email valide est requise pour une participation";
            }
            else if (value?.EvenementId is null)
            {
                errorMsg = "L'identifiant de l'événement d'une participantion est requis";
            }
            else
            {
                var evenement = Repository.Evenements.FirstOrDefault(x => x.ID == value.EvenementId);
                if (evenement is null)
                {
                    errorMsg = $"L'événement  (id = {value.EvenementId}) n'existe pas";
                }
                else if (evenement.DateFin.Value.CompareTo(DateTime.Now) < 0)
                {
                    errorMsg = "Il n'est pas possible de participer a un événement passé";
                }
                else if (Repository.Participations.Any(x => x.EvenementId == value.EvenementId && x.Email.Equals(value.Email.Trim(), StringComparison.OrdinalIgnoreCase)))
                {
                    errorMsg = $"Il existe déja une particpation enregistré avec cette adresse email  (email = {value.Email}) pour cette événement.";
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

        private bool IsValidEmail(string email)
        {
            if (String.IsNullOrEmpty(email))
            {
                return false;
            }
            try
            {
                return Regex.IsMatch(email,
                    @"^[^@\s]+@[^@\s]+\.[^@\s]+$",
                    RegexOptions.IgnoreCase, TimeSpan.FromMilliseconds(250));
            }
            catch (RegexMatchTimeoutException)
            {
                return false;
            }
        }

        public Participation ConversionVersParticipationNonDTO(ParticipationDTO participationDTO)
        {
            return new Participation
            {
                ID = participationDTO.ID,
                Email = participationDTO.Email,
                Nom = participationDTO.Nom,
                Prenom = participationDTO.Prenom,
                NombrePlace = participationDTO.NombrePlace,
                EvenementId = participationDTO.EvenementId,
                IsValid = participationDTO.IsValid,
            };
        }

    }
}
