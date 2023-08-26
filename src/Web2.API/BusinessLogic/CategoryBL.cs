using Web2.API.Data.Models;
using Web2.API.DTO;

namespace Web2.API.BusinessLogic
{
    public class CategoryBL : ICategoryBL
    {
        public CategorieDTO Add(CategorieDTO value)
        {
            if (value is null || String.IsNullOrEmpty(value.Name?.Trim()))
            {
                throw new HttpException
                {
                    Errors = new { Error = "Une valeur pour le champs 'Nom' est requis" },
                    StatusCode = StatusCodes.Status400BadRequest
                };
            }

            var categorie = new CategorieDTO { Name = value.Name.Trim().ToUpper() };
            categorie.ID = Repository.IdSequenceCategory++;

            var categorieNonDTO = ConversionVersCategorieNonDTO(categorie);
            Repository.Categories.Add(categorieNonDTO);

            return categorie;
        }

        public void Delete(int id)
        {
            var categorie = Repository.Categories.FirstOrDefault(x => x.ID == id);

            if (categorie != null)
            {
                if (Repository.Evenements.Any(x => x.IdCategorie.Contains(categorie.ID)))
                {
                    throw new HttpException
                    {
                        Errors = new { Error = "Il n'est pas possible de supprimer une categorie lié a au moins un evenement" },
                        StatusCode = StatusCodes.Status409Conflict
                    };

                }
                Repository.Categories.Remove(categorie);
            }
        }

        public CategorieDTO Get(int id)
        {
            var categorie = Repository.Categories.FirstOrDefault(x => x.ID == id);

            if (categorie != null)
            {
                CategorieDTO categorieDTO = new CategorieDTO
                {
                    ID = categorie.ID,
                    Name = categorie.Name,
                };

                CategorieEvenementsDTO listeEventsParCategorieDTO = (CategorieEvenementsDTO)categorie.Evenements;

                return categorieDTO;
            }

            return null;
        }

        public IEnumerable<CategorieDTO> GetList()
        {
            IEnumerable<Categorie> listeCategories = Repository.Categories;
            List<CategorieDTO> listeCategoriesDTO = new List<CategorieDTO>();

            foreach (Categorie categorie in listeCategories)
            {
                CategorieDTO categorieDTO = new CategorieDTO
                {
                    ID = categorie.ID,
                    Name = categorie.Name,
                };

                CategorieEvenementsDTO listeEventsParCategorieDTO = (CategorieEvenementsDTO)categorie.Evenements;

                listeCategoriesDTO.Add(categorieDTO);
            }

            return listeCategoriesDTO;
        }

        public CategorieDTO Updade(int id, CategorieDTO value)
        {
            if (value == null || String.IsNullOrEmpty(value.Name?.Trim()))
            {
                throw new HttpException
                {
                    Errors = new { Error = "Une valeur pour le champs 'Nom' est requis" },
                    StatusCode = StatusCodes.Status400BadRequest
                };
            }

            var categorie = Repository.Categories.FirstOrDefault(x => x.ID == id);


            if (categorie == null)
            {
                throw new HttpException
                {
                    Errors = new { Errors = $"Element introuvable (id = {id})" },
                    StatusCode = StatusCodes.Status400BadRequest
                };
            }

            categorie.Name = value.Name.Trim().ToUpper();

            CategorieDTO categorieDTO = new CategorieDTO
            {
                ID = categorie.ID,
                Name = categorie.Name,
            };

            return categorieDTO;
        }

        public Categorie ConversionVersCategorieNonDTO(CategorieDTO categorieDTO)
        {
            CategorieEvenementsDTO listeEventsParCategorieDTO = new();

            return new Categorie
            {
                ID = categorieDTO.ID,
                Name = categorieDTO.Name,
                Evenements = (ICollection<Evenement>)listeEventsParCategorieDTO.Evenements,
            };
        }
    }
}
