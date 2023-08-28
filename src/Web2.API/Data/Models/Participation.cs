using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Web2.API.Data.Models
{
    public class Participation
    {      
        public int ID { get; set; }
        public string Email { get; set; }
        public string Nom { get; set; }
        public string Prenom { get; set; }
        public int NombrePlace { get; set; } = 1;
        public Evenement Evenement { get; set; }
        public bool IsValid { get; set; }
    }
}
