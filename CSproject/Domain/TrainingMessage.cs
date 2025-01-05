namespace CSproject.Domain
{
    //Entity ?
    public class TrainingMessage : Message
    {
        public string TrainingName { get; set; }
        public string TrainingDescription { get; set; }
        public string TrainingDuration { get; set; }
        public double Callories { get; set; }
    }
}