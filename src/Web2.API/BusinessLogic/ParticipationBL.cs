using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Web2.API.Models;

namespace Web2.API.BusinessLogic
{
    public class ParticipationBL : IParticipationBL
    {
        public Participation Add(Participation value)
        {


            ProcessParticipationValidation(value);

            value.ID = Repository.IdSequenceParticipation++;
            value.IsValid = false;
            value.Email = value.Email.Trim();
            value.IsValid = false;
            Repository.Participations.Add(value);

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

        public Participation Get(int id)
        {
            return Repository.Participations.FirstOrDefault(x => x.ID == id);
        }

        public IEnumerable<Participation> GetList()
        {
            return Repository.Participations;
        }

        public IEnumerable<Participation> GetByEvent(int eventId)
        {
            return Repository.Participations.Where(x => x.EvenementId == eventId);
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

            VerifyParticipation(participation);

            return participation.IsValid;
        }

        private void VerifyParticipation(Participation participation)
        {
            if (!participation.IsValid)
            {
                var isValid = new Random().Next(1, 10) > 5 ? true : false;//Simuler la validation externe;
                participation.IsValid = isValid;
            }

        }

        private void ProcessParticipationValidation(Participation value)
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

    }
}
