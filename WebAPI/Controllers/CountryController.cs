using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using WebAPI.Dtos;
using WebAPI.Interfaces;
using WebAPI.Models;
namespace WebAPI.Controllers
{
    [Authorize]
    public class CountryController : BaseController
    {
        private readonly IUnitOfWork uow;
        private readonly IMapper mapper;
        public CountryController(IUnitOfWork uow, IMapper mapper)
        {
            this.uow = uow;
            this.mapper = mapper;
        }

        [HttpGet("categories")]
        [AllowAnonymous]
        public async Task<IActionResult> GetCategories()
        {
            //throw new UnauthorizedAccessException();
            var categories = await uow.categoryRepository.GetCategoriesAsync();
            var categoriesDto = mapper.Map<IEnumerable<CategoryDto>>(categories);
            return Ok(categoriesDto);
        }



        // Post api/category/post --Post the data in JSON format
        [HttpPost("post")]
        [AllowAnonymous]
        public async Task<IActionResult> AddCategory(CategoryDto categoryDto)
        {
            var category = mapper.Map<tbl_category>(categoryDto);
            category.updated_on = DateTime.UtcNow;
            category.updated_by = 2;
            // var category = new tbl_category
            // {
            //     category_name = categoryDto.category_name,
            //     updated_on = DateTime.UtcNow
            // };

            uow.categoryRepository.AddCategory(category);
            await uow.SaveAsync();
            return StatusCode(201);
        }

        [HttpPut("update/{id}")]
        public async Task<IActionResult> UpdateCategory(Guid id, CategoryDto categoryDto)
        {

            //if (id != categoryDto.category_id)
            //   return BadRequest("Update not allowed");

            var categoryFromDb = await uow.categoryRepository.FindCategoryAsync(id);

            if (categoryFromDb == null)
                return BadRequest("Update not allowed");

            categoryFromDb.updated_by = 2;
            categoryFromDb.updated_on = DateTime.UtcNow;
            mapper.Map(categoryDto, categoryFromDb);

            //throw new Exception("Some error occured");

            await uow.SaveAsync();
            return Ok(categoryDto);


        }

        [HttpPut("updateCategoryName/{id}")]
        public async Task<IActionResult> UpdateCategory(Guid id, CategoryUpdateDto categoryDto)
        {
            var categoryFromDb = await uow.categoryRepository.FindCategoryAsync(id);
            categoryFromDb.updated_by = 2;
            categoryFromDb.updated_on = DateTime.UtcNow;
            mapper.Map(categoryDto, categoryFromDb);
            await uow.SaveAsync();
            return Ok(categoryDto);
        }

        [HttpPatch("update/{id}")]
        public async Task<IActionResult> UpdateCategoryPatch(Guid id, JsonPatchDocument<tbl_category> categoryToPatch)
        {
            var categoryFromDb = await uow.categoryRepository.FindCategoryAsync(id);
            categoryFromDb.updated_by = 2;
            categoryFromDb.updated_on = DateTime.UtcNow;
            categoryToPatch.ApplyTo(categoryFromDb, ModelState);
            await uow.SaveAsync();
            return StatusCode(200);
        }

        [HttpDelete("delete/{id}")]
        public async Task<IActionResult> DeleteCategory(Guid id)
        {
            uow.categoryRepository.DeleteCategory(id);
            await uow.SaveAsync();
            return Ok(id);
        }

        // Post api/category/add?category=test
        // Post api/category/add/test
        // [HttpPost("add")]
        // [HttpPost("add/{category}")]
        // public async Task<IActionResult> AddCategory(string category)
        // {

        //     tbl_category newcaterory = new tbl_category();
        //     newcaterory.category_name = category;
        //     await dc.tbl_categories.AddAsync(newcaterory);
        //     await dc.SaveChangesAsync();
        //     return Ok(newcaterory);
        // }
    }
}