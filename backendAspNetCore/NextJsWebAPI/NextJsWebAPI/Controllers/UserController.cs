using Microsoft.AspNetCore.Mvc;
using NextJsWebAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NextJsWebAPI.Controllers
{
    //[Route("api/user")]
    //[ApiController]
    public class UserController : ControllerBase
    {
        private DataContext _dataContext;

        public UserController(DataContext context)
        {
            _dataContext = context;
        }

        [HttpGet]
        [Route("api/profile")]
        public async Task<IActionResult> Read()
        {
            try
            {
                string msg = "message";
                return await Task.FromResult(Ok(msg));
            }
            catch (Exception ex)
            {
                var msg = ex.Message;
                return BadRequest(msg);
            }
        }
    }
}
