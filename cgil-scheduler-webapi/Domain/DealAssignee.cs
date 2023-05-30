using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain
{
    [PrimaryKey("DaId")]
    public class DealAssigne
    {
        public int DaId { get; set; }

        [ForeignKey("Deal")]
        public int DaDlId { get; set; }
        public Deal DaDl { get; set; }

        [ForeignKey("Assignee")]
        public int DaAeId { get; set; }      
        public Assignee DaAe { get; set; }
    }
}
