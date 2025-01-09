using System.Text.Json.Serialization;

namespace CSproject.Domain
{
    //Entity ?
    public class Message
    {
        public int Id { get; set; }
        [JsonIgnore]
        public User User { get;  set; }

        public string? UserName { get; set; }
        public bool Status { get;  set; }
        public DateTime SendDate { get;  set; }
        public string? UserPhoto { get; set; }
        public string? Photo { get; set; }
        public string Type { get; set; }
        public bool? IsMine { get; set; }
        public string? Content { get; set; }

        //public Dictionary<string, bool> Reactions { get; private set; }
    }
}