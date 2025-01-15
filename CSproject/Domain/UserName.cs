using CSproject.Infrastructure;

namespace CSproject.Domain
{
    public class UserName
    {
        public UserName(string firstName, string lastName)
        {
            FirstName = firstName;
            LastName = lastName;
        }

        protected UserName() { }
        public string FirstName { get; }

        public string LastName { get; }

        public string GetFullName() => $"{FirstName} {LastName}";
    }
}
