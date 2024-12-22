using CSproject.Infrastructure;

namespace CSproject.Domain
{
    public class Person
    {
        public PersonName Name { get; private set; }
        public string Gender { get; private set; }
        public string Login { get; private set; }
        public string Password { get; private set; }

        public Person(PersonName name, string gender, string login, string password)
        {
            Name = name;
            Gender = gender;
            Login = login;
            Password = password;
        }
    }
}


