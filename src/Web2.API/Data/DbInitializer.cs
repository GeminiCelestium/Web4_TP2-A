using Web2.API.Data.Models;

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
                new Categorie{ID = 1, Name="Spectacle"},
                new Categorie{ID = 2, Name="Compétition"},
                new Categorie{ID = 3, Name="Sport"},
                new Categorie{ID = 4, Name="Concours"},
                new Categorie{ID = 5, Name="Bingo"},
                new Categorie{ID = 6, Name="LAN"},
            };

            context.Categories.AddRange(categories);

            var villes = new Ville[] {
                new Ville{ID = 1, Name = "Québec", Region =Region.CAPITALE_NATIONALE},
                new Ville{ID = 2, Name = "Montréal", Region =Region.MONTREAL},
                new Ville{ID = 3, Name = "Sept-Iles", Region =Region.COTE_NORD},
            };

            context.Villes.AddRange(villes);

            var evenements = new Evenement[] {
                new Evenement{
                    ID = 1,
                    Titre = "Le Grand Bingo",
                    Description = "Chaque année, le plus grand Bingo de la province de Québec !",
                    Organisateur = "Joseph Inc.",
                    DateDebut = DateTime.Parse("2023-07-17").ToUniversalTime(),
                    DateFin = DateTime.Parse("2023-07-17").ToUniversalTime(),
                    Adresse = "123 rue du Bingo",
                    Prix = 100,
                    VilleID = 3,
                    IdCategorie = 5,
                },

                new Evenement {
                    ID = 2,
                    Titre = "Clash de l'été",
                    Description = "Retrouvez Les Conquérents au meilleur de leur forme pour cet évènement tant attendu !",
                    Organisateur = "Gravel Inc.",
                    DateDebut = DateTime.Parse("2023-07-13").ToUniversalTime(),
                    DateFin = DateTime.Parse("2023-07-21").ToUniversalTime(),
                    Adresse = "456 rue de La Faille",
                    Prix = 1500,
                    VilleID = 1,
                    IdCategorie = 6,
                },
            };

            context.Evenements.AddRange(evenements);

            var participations = new Participation[] {
                new Participation {
                    ID = 1,
                    Email = "123456@gmail.com",
                    Nom = "Gravel",
                    Prenom = "Jason",
                    NombrePlace = 5,
                    Evenement = evenements[2],
                    IsValid = true,
                },

                new Participation
                {
                    ID = 2,
                    Email = "13579@gmail.com",
                    Nom = "Tremblay",
                    Prenom = "Nicolas",
                    NombrePlace = 5,
                    Evenement = evenements[2],
                    IsValid = true,
                },

                new Participation
                {
                    ID = 3,
                    Email = "02468@gmail.com",
                    Nom = "Dupont",
                    Prenom = "Marie-Pierre",
                    NombrePlace = 5,
                    Evenement = evenements[2],
                    IsValid = true,
                },

                new Participation
                {
                    ID = 4,
                    Email = "987654@gmail.com",
                    Nom = "Joseph",
                    Prenom = "Nathan",
                    NombrePlace = 17,
                    Evenement = evenements[1],
                    IsValid = true,
                },

                new Participation
                {
                    ID = 5,
                    Email = "97531@gmail.com",
                    Nom = "Gravel",
                    Prenom = "Jason",
                    NombrePlace = 21,
                    Evenement = evenements[1],
                    IsValid = true,
                },

                new Participation
                {
                    ID = 6,
                    Email = "97531@gmail.com",
                    Nom = "Marcotte",
                    Prenom = "Vincent",
                    NombrePlace = 18,
                    Evenement = evenements[1],
                    IsValid = true,
                },
            };

            context.Participations.AddRange(participations);

            context.SaveChanges();
        }
    }
}
