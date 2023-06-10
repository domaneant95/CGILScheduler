using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain
{
    public class Assignee
    {
        public Assignee()
        {
            Deals = new HashSet<Deal>();
        }

        public int Id { get; set; }
        public AppUser AppUser { get; set; }
        public ICollection<Deal> Deals { get; set; }
    }
}
