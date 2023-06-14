namespace Domain.Dto
{
    public class DealDto
    {
        public string code { get; set; }
        public string text { get; set; }
        public string description { get; set; }
        public string reccurenceRule { get; set; }
        public string recurrenceException { get; set; }
        public int[] assigneeId { get; set; }
        public int roomId { get; set; }
        public int[] priorityId { get; set; }
        public object startDate { get; set; }
        public object endDate { get; set; }
        public bool allDay { get; set; }
        public List<Attachment> attachment { get; set; }
    }
}
