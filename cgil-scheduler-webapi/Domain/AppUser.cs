using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Domain
{
    public class AppUser : IdentityUser
    {
        public AppUser() 
        {
            Deals = new HashSet<Deal>();
        }

        public string DisplayName { get; set; }
        public string Bio { get; set; }
        //un utente puo' creare tante trattative
        public ICollection<Deal> Deals { get; set; } 
        //un utente puo' essere anche un partecipante
        public Headquarter? Headquarter { get; set; }
    }
}
