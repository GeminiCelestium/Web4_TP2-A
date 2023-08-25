namespace Web2.API.DTO
{
    public class CategorieDTO
    {
        public int ID { get; set; }
        public string Name { get; set; }
    }

    public class CategorieEvenementsDTO : CategorieDTO
    {
        public List<EvenementDTO> Evenements { get; set; }
    }
}
