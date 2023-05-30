using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Domain
{
    [PrimaryKey("DlId")]
    public class Deal
    {
        public int DlId { get; set; }

        [ForeignKey("AppUser")]
        public string DlUserId { get; set; }
        public AppUser DlUser { get; set; }

        [ForeignKey("Priority")]
        public int DlPriorityId { get; set; }
        public Priority DlPriority { get; set; }

        [ForeignKey("Headquarter")]
        public int DlHdId { get; set; }
        public Headquarter DlHd { get; set; }

        public string DlText { get; set; }
        public DateTime DlStartDate { get; set; }
        public DateTime DlEndDate { get; set; }
    }
}