namespace CalendarNET.Controlers.Requests
{
    public class TaskRequest
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public DateTime DueOn { get; set; }
        public bool shared { get; set; }
    }
}
