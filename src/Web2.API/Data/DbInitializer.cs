using Web2.API.Data.Models;
using Web2.API.Models;

namespace Web2.API.Data
{
    public class DbInitializer
    {
        public static void Initialize(TP2A_Context context)
        {
            if (context.Categories.Any() || context.Evenements.Any() || context.Participations.Any() || context.Villes.Any())
            {
                return;
            }

            var categories = new Categorie[] {
                new Categorie{Name="Spectacle"},
                new Categorie{Name="Compétition"},
                new Categorie{Name="Sport"},
                new Categorie{Name="Concours"},
                new Categorie{Name="Bingo"},
                new Categorie{Name="LAN"},
            };

            context.Categories.AddRange(categories);

            var evenements = new Evenement[] {
                new Evenement{
                    Titre = "Le Grand Bingo",
                    Description = "Chaque année, le plus grand Bingo de la province de Québec !",
                    Organisateur = "Joseph Inc.",
                    DateDebut = DateTime.Parse("2023-07-17"),
                    DateFin = DateTime.Parse("2023-07-17"),
                    Adresse = "123 rue du Bingo",
                    Prix = 100,
                    VilleID = 3,
                    IdCategorie = { 5, },
                },

                new Evenement {
                    Titre = "Clash de l'été",
                    Description = "Chaque année, le plus grand Bingo de la province de Québec !",
                    Organisateur = "Gravel Inc.",
                    DateDebut = DateTime.Parse("2023-07-13"),
                    DateFin = DateTime.Parse("2023-07-21"),
                    Adresse = "456 rue de La Faille",
                    Prix = 1500,
                    VilleID = 2,
                    IdCategorie = { 2, 6, },
                },
            };

            context.Evenements.AddRange(evenements);

            var participations = new Participation[] {
                new Participation {
                    Email = "123456@gmail.com",
                    Nom = "Gravel",
                    Prenom = "Jason",
                    NombrePlace = 5,
                    EvenementId = 2,
                    IsValid = true,
                },

                new Participation
                {
                    Email = "13579@gmail.com",
                    Nom = "Tremblay",
                    Prenom = "Nicolas",
                    NombrePlace = 5,
                    EvenementId = 2,
                    IsValid = true,
                },

                new Participation
                {
                    Email = "02468@gmail.com",
                    Nom = "Dupont",
                    Prenom = "Marie-Pierre",
                    NombrePlace = 5,
                    EvenementId = 2,
                    IsValid = true,
                },

                new Participation
                {
                    Email = "987654@gmail.com",
                    Nom = "Joseph",
                    Prenom = "Nathan",
                    NombrePlace = 17,
                    EvenementId = 1,
                    IsValid = true,
                },

                new Participation
                {
                    Email = "97531@gmail.com",
                    Nom = "Gravel",
                    Prenom = "Jason",
                    NombrePlace = 21,
                    EvenementId = 1,
                    IsValid = true,
                },

                new Participation
                {
                    Email = "97531@gmail.com",
                    Nom = "Marcotte",
                    Prenom = "Vincent",
                    NombrePlace = 18,
                    EvenementId = 1,
                    IsValid = true,
                },
            };

            context.Participations.AddRange(participations);

            var villes = new Ville[] {
                new Ville{Name="Québec",Region=Region.CAPITALE_NATIONALE},
                new Ville{Name="Montréal",Region=Region.MONTREAL},
                new Ville{Name="Sept-Iles",Region=Region.COTE_NORD},
            };

            context.Villes.AddRange(villes);
        }
    }
}
