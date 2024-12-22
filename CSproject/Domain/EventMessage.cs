using System.Reflection;

namespace CSproject.Domain
{
    public class EventMessage : BaseMessage
    {
        public string EventName { get; set; }
        public string EventDescription { get; set; }
        public DateTime EventDate { get; set; }
        public string Location { get; set; }

        public EventMessage(Person sender /* ... */) : base(sender, DateTime.Now /* ... */)
        {
            /* ... */
        }
    }
}