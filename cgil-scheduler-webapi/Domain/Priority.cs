using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain
{
    public class Priority
    {
        public Priority()
        {
            Deals = new HashSet<Deal>();    
        }

        public int Id { get; set; }
        public int Code { get; set; }
        public string Text { get; set; }
        public string Color { get; set; }
        public ICollection<Deal> Deals { get; set; }
    }
}