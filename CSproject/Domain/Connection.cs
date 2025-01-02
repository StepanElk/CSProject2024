using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CSproject.Domain
{
    public class Connection
    {
        [Key]
        public string Uuid { get; set; }

        public User? User { get; set; }

        public string? Chatroom { get; set; }

        public string ContextId { get; set; }

    }
}
