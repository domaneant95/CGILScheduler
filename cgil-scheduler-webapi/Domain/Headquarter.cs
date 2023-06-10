using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain
{
    public class Headquarter
    {
        public Headquarter()
        {
            Deals = new HashSet<Deal>();
            AppUsers = new HashSet<AppUser>();
        }

        public int Id { get; set; }
        public string HdName { get; set; }
        public string HdColor { get; set; }
        public string HdRegion { get; set; }
        public string HdProvince { get; set; }
        public string HdCity { get; set; }
        public string HdAddress { get; set; }
        public int HdZipCode { get; set; }
        public string StreetNumber { get; set; }
        public ICollection<Deal> Deals { get; set; }
        public ICollection<AppUser> AppUsers { get; set; }
    }
}
