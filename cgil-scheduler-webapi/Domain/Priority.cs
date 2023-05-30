using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain
{
    [PrimaryKey("PrId")]
    public class Priority
    {
        public int PrId { get; set; }
        public int Code { get; set; }
        public string Text { get; set; }
        public string Color { get; set; }
        public Deal Deal { get; set; }
    }
}