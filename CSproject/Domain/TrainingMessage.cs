namespace CSproject.Domain
{
    //Entity ?
    public class TrainingMessage : Message
    {
        public string TrainingName { get; set; }
        public string TrainingDescription { get; set; }
        public int TrainingDuration { get; set; }
        public int Calories { get; set; }
    }
}