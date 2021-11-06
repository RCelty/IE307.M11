using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using API.Models.DTO;
using API.Models.EF;

namespace API.Models.DAO
{
    public class CategoryDAO
    {
        private static CategoryDAO instance;

        public static CategoryDAO Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new CategoryDAO();
                }
                return instance;
            }
            private set => instance = value;
        }

        PhoneStoreEntities1 db = new PhoneStoreEntities1();

        public async Task<List<CategoryDTO>> GetAllCategory()
        {
            return (await db.Categories
                        .ToListAsync())
                        .Select(category => new CategoryDTO(category))
                        .ToList();
        }

        public async Task<int> AddCategory(CategoryDTO categoryDTO)
        {
            var category = new Category()
            {
                DisplayName = categoryDTO.DisplayName
            };

            db.Categories.Add(category);
            await db.SaveChangesAsync();

            return category.ID;
        } 

        public async Task<bool> UpdateCategory(CategoryDTO category)
        {
            var result = db.Categories.SingleOrDefault(c => c.ID == category.ID);

            if (result != null)
            {
                result.DisplayName = category.DisplayName;
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}