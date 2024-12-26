﻿using CSproject.Infrastructure;

namespace CSproject.Domain
{
    public class PersonName : ValueType<PersonName>
    {
        public PersonName(string firstName, string lastName)
        {
            FirstName = firstName;
            LastName = lastName;
        }

        public string FirstName { get; }
        public string LastName { get; }
    }
}
