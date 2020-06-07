using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NextJsWebAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks;

namespace NextJsWebAPI.Controllers
{
    [ApiController]
    public class CategoryController : ControllerBase
    {
        private DataContext _dataContext;

        public CategoryController(DataContext context)
        {
            _dataContext = context;
        }

        [Route("api/category")]
        [Authorize]
        [HttpPost]
        public async Task<IActionResult> CreateCategory([FromBody] Category model)
        {
            try
            {
                var category = new Category();
                category.Id = model.Id;
                category.Name = model.Name;
                _dataContext.Add(category);
                _dataContext.SaveChanges();
                return await Task.FromResult(Ok(category));
            }
            catch (Exception ex)
            {
                var msg = ex.Message;
                return BadRequest(msg);
            }
        }

        [Route("api/categories")]
        [HttpGet]
        public async Task<IActionResult> GetCategories()
        {
            try
            {
                List<Category> categories = new List<Category>();

                categories = _dataContext.Categories.ToList();

                return await Task.FromResult(Ok(categories));
            }
            catch (Exception ex)
            {
                var msg = ex.Message;
                return BadRequest(msg);
            }
        }

        [Route("api/category/{id}")]
        [HttpGet]
        public async Task<IActionResult> GetCategory(int id)
        {
            try
            {
                Category cat = new Category();

                cat = _dataContext.Categories.Where(x => x.Id == id).FirstOrDefault();

                return await Task.FromResult(Ok(cat));
            }
            catch (Exception ex)
            {
                var msg = ex.Message;
                return BadRequest(msg);
            }
        }

        [Route("api/category/{id}")]
        [HttpDelete]
        public async Task<IActionResult> DeleteCategory(int id)
        {
            string message = "success";

            try
            {
                Category cat = new Category();
                var catid = _dataContext.Categories.Where(x => x.Id == id).FirstOrDefault();
                _dataContext.Categories.Remove(catid);

                return await Task.FromResult(Ok(message));
            }
            catch (Exception ex)
            {
                var msg = ex.Message;
                return BadRequest(msg);
            }
        }
    }
}
