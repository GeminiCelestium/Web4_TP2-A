using System.ComponentModel.DataAnnotations;

namespace Web2.API.Data.Models
{
    public class Categorie
    {        
        public int ID { get; set; }
        public string Name { get; set; }

        public ICollection<Evenement> Evenements { get; set;}
    }
}
