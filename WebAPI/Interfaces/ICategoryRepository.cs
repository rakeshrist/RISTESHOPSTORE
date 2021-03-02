namespace WebAPI.Interfaces
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using WebAPI.Models;

    public interface ICategoryRepository
    {
        Task<IEnumerable<tbl_category>> GetCategoriesAsync();
        void AddCategory(tbl_category category);
        void DeleteCategory(Guid categoryId);
        Task<tbl_category> FindCategoryAsync(Guid id);
    }
}