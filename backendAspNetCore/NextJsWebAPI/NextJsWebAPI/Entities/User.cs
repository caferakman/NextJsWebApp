using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Identity;
using NextJsWebAPI.Models;

namespace NextJsWebAPI.Entities
{
    public class User : IdentityUser
    {
        //Entities identity
        public int Id { get; set; } //?? to be removed
        public string Name { get; set; }
        public string Profile { get; set; }
        //public virtual ICollection<Category> Categories { get; set; } //to be added
        public int Role { get; set; }
        [NotMapped]
        public byte[] Salt { get; set; }
        

    }
}
