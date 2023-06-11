using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.DTOs
{
    public class FormDataCollectionDto
    {
        public bool allDay { get; set; }
        public string startDate { get; set; }
        public string endDate { get; set; }
        public string text { get; set; }
        public string description { get; set; }
        public int[] roomId { get; set; }
        public int[] assigneeId { get; set; }
    }
}
