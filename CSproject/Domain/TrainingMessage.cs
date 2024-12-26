namespace CSproject
{
    public class TrainingMessage
    {
        public string Sender { get; set; }
        public bool Status { get; set; }
        public DateTime SendDate { get; set; }
        public string Photo { get; set; }
        public Dictionary<string, bool> Reactions { get; set; }
        public string TrainingName { get; set; }
        public string TrainingDescription { get; set; }
        public string TrainingDuration { get; set; }
        public double Callories { get; set; }

        public TrainingMessage(string senderName /*, ... */)
        {
            Sender = senderName;
            SendDate = DateTime.Now;
        }
    }
}