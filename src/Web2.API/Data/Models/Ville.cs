using System.ComponentModel.DataAnnotations;

namespace Web2.API.Data.Models
{
    public class Ville
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public Region Region { get; set; }

        public ICollection<Evenement> Evenements { get; set; }
    }

    public enum Region
    {
        ESTRIE,
        MAURICIE,
        OUTAOUAI,
        ABITIBI,
        COTE_NORD
    }
}
