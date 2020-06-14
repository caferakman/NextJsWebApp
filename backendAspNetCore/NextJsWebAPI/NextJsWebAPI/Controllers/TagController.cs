using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NextJsWebAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NextJsWebAPI.Controllers
{
    public class TagController : ControllerBase
    {
        private DataContext _dataContext;

        public TagController(DataContext context)
        {
            _dataContext = context;
        }

        [Route("api/tag")]
        [Authorize]
        [HttpPost]
        public async Task<IActionResult> CreateTag([FromBody] Tag model)
        {
            try
            {
                var tag = new Tag();
                tag.TagName = model.TagName;
                _dataContext.Add(tag);
                _dataContext.SaveChanges();
                return await Task.FromResult(Ok(tag));
            }
            catch (Exception ex)
            {
                var msg = ex.Message;
                return BadRequest(msg);
            }
        }

        [Route("api/tags")]
        [HttpGet]
        public async Task<IActionResult> GetTags()
        {
            try
            {
                List<Tag> tags = new List<Tag>();

                tags = _dataContext.Tags.ToList();

                return await Task.FromResult(Ok(tags));
            }
            catch (Exception ex)
            {
                var msg = ex.Message;
                return BadRequest(msg);
            }
        }

        [Route("api/tag/{id}")]
        [HttpGet]
        public async Task<IActionResult> GetTag(int id)
        {
            try
            {
                Tag _tag = new Tag();

                _tag = _dataContext.Tags.Where(x => x.Id == id).FirstOrDefault();

                return await Task.FromResult(Ok(_tag));
            }
            catch (Exception ex)
            {
                var msg = ex.Message;
                return BadRequest(msg);
            }
        }

        [Route("api/tag/{id}")]
        [Authorize]
        [HttpDelete]
        public async Task<IActionResult> DeleteTag(int id)
        {
            string message = "success";

            try
            {
                Tag _tag = new Tag();
                var tagid = _dataContext.Tags.Where(x => x.Id == id).FirstOrDefault();
                _dataContext.Tags.Remove(tagid);
                _dataContext.SaveChanges();

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
