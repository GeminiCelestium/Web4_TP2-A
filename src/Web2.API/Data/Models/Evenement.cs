using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Web2.API.Data.Models
{
    public class Evenement
    {        
        public int ID { get; set; }
        public string Titre { get; set; }
        public string Description { get; set; }
        public DateTime? DateDebut { get; set; }
        public string Organisateur { get; set; }
        public DateTime? DateFin { get; set; }
        public string Adresse { get; set; }
        public double? Prix { get; set; }

        public int? VilleID { get; set; }
        public List<int> IdCategorie { get; set; }

        public ICollection<Participation> Participations { get; set; }
    }
}
