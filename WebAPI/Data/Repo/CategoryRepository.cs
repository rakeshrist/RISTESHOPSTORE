using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using WebAPI.Interfaces;
using WebAPI.Models;

namespace WebAPI.Data.Repo
{
    public class CategoryRepository : ICategoryRepository
    {
        private readonly StoreDataContext dc;
        public CategoryRepository(StoreDataContext dc)
        {
            this.dc = dc;

        }
        public void AddCategory(tbl_category category)
        {
            dc.tbl_categories.AddAsync(category);
        }

        public void DeleteCategory(Guid categoryId)
        {
            var category = dc.tbl_categories.Find(categoryId);
            dc.tbl_categories.Remove(category);
        }

        public async Task<tbl_category> FindCategoryAsync(Guid id)
        {
            return await dc.tbl_categories.FindAsync(id);
        }

        public async Task<IEnumerable<tbl_category>> GetCategoriesAsync()
        {
            return await dc.tbl_categories.ToListAsync();
        }


    }
}