using Web2.API.Data.Models;

namespace Web2.API.DTO
{
    public class VilleDTO
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public Region Region { get; set; }
    }

    public enum Region
    {
        ESTRIE,
        MAURICIE,
        OUTAOUAI,
        ABITIBI,
        COTE_NORD
    }

    public class VilleEvenementsDTO : VilleDTO
    {
        public List<EvenementDTO> Evenements { get; set;}
    }
}
