using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Web2.API.Data.Models;
using Web2.API.DTO;

namespace Web2.API.BusinessLogic
{
    public interface ICategoryBL
    {
        public IEnumerable<CategorieDTO> GetList();
        public CategorieDTO Get(int id);
        public CategorieDTO Add(CategorieDTO value);
        public CategorieDTO Updade(int id, CategorieDTO value);
        public void Delete(int id);
        public Categorie ConversionVersCategorieNonDTO(CategorieDTO categorieDTO);
    }
}
