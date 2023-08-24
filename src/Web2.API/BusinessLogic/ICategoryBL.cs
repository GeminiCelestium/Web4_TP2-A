using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Web2.API.Models;

namespace Web2.API.BusinessLogic
{
    public interface ICategoryBL
    {
        public IEnumerable<Category> GetList();
        public Category Get(int id);
        public Category Add(Category value);
        public Category Updade(int id, Category value);
        public void Delete(int id);
    }
}
