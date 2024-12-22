namespace CSproject.Domain
{
    public class TrainingMessage : BaseMessage
    {
        public string TrainingName { get; set; }
        public string TrainingDescription { get; set; }
        public string TrainingDuration { get; set; }
        public double Callories { get; set; }

        public TrainingMessage(Person sender /* ... */) : base(sender, DateTime.Now /* ... */)
        {
            /* ... */
        }
    }
}