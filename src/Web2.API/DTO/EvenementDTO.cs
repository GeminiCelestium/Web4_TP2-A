namespace Web2.API.DTO
{
    public class EvenementDTO
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
    }

    public class EvenementParticipationsDTO : EvenementDTO
    {
        public List<ParticipationDTO> Participations { get; set; }
    }
}
