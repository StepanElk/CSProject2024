namespace CSproject.Domain
{
    //Entity ?
    public class EventMessage : Message
    {
        public string EventName { get; set; }
        public string EventDescription { get; set; }
        public DateTime EventDate { get; set; }
        public string? Location { get; set; }

    }
}