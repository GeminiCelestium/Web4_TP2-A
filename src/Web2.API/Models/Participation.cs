using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Web2.API.Models
{
    public class Participation
    {
        [Key]
        public int ID { get; set; }
        public string Email { get; set; }
        public string Nom { get; set; }
        public string Prenom { get; set; }
        public int NombrePlace { get; set; } = 1;
        public int? EvenementId { get; set; }
        public bool IsValid { get; set; }
    }
}
