using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CSproject.Domain
{
    //Entity ?
    public class User
    {
        [Key]
        public string Login { get;  set; }
        public string Name { get;  set; }
        public string Sex { get;  set; }
        public string Password { get;  set; }

    }
}



