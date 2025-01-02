using System.Reflection;

namespace CSproject.Domain
{
    //Entity ?
    public class EventMessage : BaseMessage
    {
        public string EventName { get; set; }
        public string EventDescription { get; set; }
        public DateTime EventDate { get; set; }
        public string Location { get; set; }

        public EventMessage(User sender /* ... */) : base(sender, DateTime.Now /* ... */)
        {
            /* ... */
        }
    }
}