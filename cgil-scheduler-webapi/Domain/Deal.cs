using Microsoft.AspNetCore.Identity;
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
        public string AppUserId { get; set; }
        public int DlPriorityId { get; set; }
        public int DlHdId { get; set; }
        public string DlText { get; set; }
        public DateTime DlStartDate { get; set; }
        public DateTime DlEndDate { get; set; }
        public virtual AppUser AppUser { get; set; }
    }
}