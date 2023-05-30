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
    [PrimaryKey("AeId")]
    public class Assignee
    {
        public int AeId { get; set; }

        [ForeignKey("AppUser")]
        public string AeUsId { get; set; }
        public AppUser AeUs { get; set; }
    }
}
