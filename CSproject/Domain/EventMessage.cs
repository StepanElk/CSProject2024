namespace CSproject
{
    public class EventMessage
    {
        public string Sender { get; set; }
        public bool Status { get; set; }
        public DateTime SendDate { get; set; }
        public string Photo { get; set; }
        public Dictionary<string, bool> Reactions { get; set; }
        public string EventName { get; set; }
        public string EventDescription { get; set; }
        public DateTime EventDate { get; set; }
        public string Location { get; set; }

        public EventMessage(string senderName /*, ... */)
        {
            Sender = senderName;
            SendDate = DateTime.Now;
        }
    }
}