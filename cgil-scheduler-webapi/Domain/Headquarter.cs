using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain
{
    [PrimaryKey("HdId")]
    public class Headquarter
    {
        public int HdId { get; set; }
        public string HdName { get; set; }
        public string HdColor { get; set; }
        public string HdRegion { get; set; }
        public string HdProvince { get; set; }
        public string HdCity { get; set; }
        public string HdAddress { get; set; }
        public int HdZipCode { get; set; }
        public int StreetNumber { get; set; }
        public int DlId { get; set; }
    }
}
