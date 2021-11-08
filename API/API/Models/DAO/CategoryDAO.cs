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
            var CategoryList = (await db.Categories
                        .ToListAsync())
                        .Select(category => new CategoryDTO(category))
                        .ToList();
            CategoryList = CategoryList.FindAll(c => c.IsDeleted == false);
            return CategoryList;
        }

        public async Task<int> AddCategory(CategoryDTO categoryDTO)
        {
            var category = new Category()
            {
                DisplayName = categoryDTO.DisplayName,
                IsDeleted = false
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
                await db.SaveChangesAsync();
                return true;
            }
            else
            {
                return false;
            }
        }

        public async Task<bool> DeleteCategory(CategoryDTO category)
        {
            var result = db.Categories.SingleOrDefault(c => c.ID == category.ID);

            if (result != null)
            {
                result.IsDeleted = true;
                await db.SaveChangesAsync();
                return true;
            }
            else
            {
                return false;
            }
        }

        public async Task<bool> RestoreAllCategory()
        {
            var CategoryList = (await db.Categories
                        .ToListAsync())
                        .Select(category => new CategoryDTO(category))
                        .ToList();
            try
            {
                var DeletedList = db.Categories.Where(c => c.IsDeleted == true).ToList();
                DeletedList.ForEach(c => c.IsDeleted = false);
                db.SaveChanges();


                return true;
            }
            catch (Exception e)
            {
                return false;
            }
        }
    }
}