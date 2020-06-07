using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NextJsWebAPI.Models
{
    public class AccountModel
    {
        public int Id { get; set; } //?? to be removed
        public string UserName { get; set; }
        public string Name { get; set; }
        //public string Token { get; set; } //?? to be added
        public string Email { get; set; }
        public string Profile { get; set; }
        public string Password { get; set; }        
        public string About { get; set; }
        public int Role { get; set; }
        public string Photo { get; set; }
        public string ResetPasswordLink { get; set; }
    }
}
