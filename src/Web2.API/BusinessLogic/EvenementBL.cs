using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Web2.API.Models;

namespace Web2.API.BusinessLogic
{
    public class EvenementBL : IEvenementBL
    {
        public Evenement Add(Evenement value)
        {

            ProcessEventValidation(value);

            value.ID = Repository.IdSequenceEvenement++;
            Repository.Evenements.Add(value);

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

        public Evenement Get(int id)
        {
            return Repository.Evenements.FirstOrDefault(x => x.ID == id);
        }

        public IEnumerable<Evenement> GetList()
        {
            return Repository.Evenements;
        }

        public Evenement Updade(int id, Evenement value)
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

            evenement.Titre = value.Titre;
            evenement.Description = value.Description;
            evenement.Organisateur = value.Organisateur;
            evenement.DateDebut = value.DateDebut;
            evenement.DateFin = value.DateFin;
            evenement.VilleID = value.VilleID;
            evenement.CategoryIDs = value.CategoryIDs;
            evenement.Prix = value.Prix;

            return evenement;
        }

        public IEnumerable<Evenement> GetByVille(int villeId)
        {
            return Repository.Evenements.Where(x => x.VilleID == villeId);
        }

        private void ProcessEventValidation(Evenement value)
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
            else if (value?.CategoryIDs?.Any() != true)
            {
                errorMsg = "Une Categorie au moins est requis pour un evenement";
            }
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
                    foreach (var categoryId in value.CategoryIDs)
                    {
                        if (!Repository.Villes.Any(x => x.ID == categoryId))
                        {
                            errorMsg = $"La  category (id = {categoryId}) n'existe pas";
                            break;
                        }
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
    }
}