namespace Domain.Dto
{
    public class DealDto
    {
        public string Text { get; set; }
        public string Description { get; set; }
        public string ReccurenceRule { get; set; }
        public string RecurrenceException { get; set; }
        public int AssigneeId { get; set; }
        public int RoomId { get; set; }
        public int PriorityId { get; set; }
        public object StartDate { get; set; }
        public object EndDate { get; set; }
        public bool AllDay { get; set; }
    }
}
