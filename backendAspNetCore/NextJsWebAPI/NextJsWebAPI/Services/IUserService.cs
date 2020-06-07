using NextJsWebAPI.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NextJsWebAPI.Services
{
    public interface IUserService
    {
        Task<User> Register(User user, string password);
        Task<User> Login(string username, string password);
    }
}
