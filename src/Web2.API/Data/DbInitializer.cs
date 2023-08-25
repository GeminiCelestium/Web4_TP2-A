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

            var categories = new Categorie[]
                {

                };

            var evenements = new Evenement[]
                {

                };

            var participations = new Participation[]
                {

                };

            var villes = new Ville[]
                {

                };
        }
    }
}
