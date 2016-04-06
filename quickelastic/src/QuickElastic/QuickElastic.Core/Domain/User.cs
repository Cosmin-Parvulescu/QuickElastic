using System;
using Bogus;

namespace QuickElastic.Core.Domain
{
    public class User
    {
        public int Id { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }
        
        public string Username { get; set; }

        public string AvatarUrl { get; set; }

        public string Email { get; set; }

        public DateTime DateOfBirth { get; set; }

        public string Phone { get; set; }

        public string Website { get; set; }
    }
}
