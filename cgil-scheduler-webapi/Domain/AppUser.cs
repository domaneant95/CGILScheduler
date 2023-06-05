using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Domain
{
    [PrimaryKey("Id")]
    public class AppUser : IdentityUser
    {
        public string AppUserId { get => Id; set => Id = value; }
        public string DisplayName { get; set; }
        public string Bio { get; set; }
        public ICollection<Deal> Deals { get; set; }
    }
}
