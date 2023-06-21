using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Domain
{
    public class Deal
    {
        public int Id { get; set; }
        public Guid Code { get;set; }
        public string Text { get; set; }
        public string Description { get; set; }
        public string ReccurenceRule { get; set; }
        public string RecurrenceException { get; set; }
        public DateTime DlStartDate { get; set; }
        public DateTime DlEndDate { get; set; }
        public AppUser AppUser { get; set; }
        public Priority Priority { get; set; }
        public Headquarter Headquarter { get; set; }
        public ICollection<Assignee> Assignees { get; set; }
        public ICollection<FileItem> Attachments { get; set; }
    }
}