using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using domain.exceptions;

namespace domain.entities
{
    public enum Role { Customer, Admin};
    public enum Status { Active, Banned};
    public class User
    {
        public Guid UserId { get; private set; }
        public string Name { get; private set; } = string.Empty;
        public string? FullName {get; private set; }
        public string Email { get; private set; } = string.Empty;
        public string Password { get; private set; } = string.Empty;
        public string? Avatar { get; private set; }
        public Role Role { get; private set; } = Role.Customer;
        public Status Status { get; private set; } = Status.Active;
        public DateTime? Created_At { get; private set; } = DateTime.Now;
        public DateTime? Updated_At { get; private set; } = DateTime.Now;
        private User () {}
        public User(string name, string email, string password)
        {
            
            if(string.IsNullOrEmpty(name))
                throw new DomainException("Name is required");
            
            if(string.IsNullOrEmpty(email))
                throw new DomainException("Email is required");
            UserId = Guid.NewGuid();
            Name = name;
            // FullName = fullName;
            Email = email;
            Password = password;
            Avatar = string.Empty;
            Role = Role.Customer;
            Status = Status.Active;
            Created_At = DateTime.Now;
        }
        public User(Guid Id, string username, string fullname, string email, DateTime created, DateTime? updated)
        {
            UserId = Id;
            Name = username;
            Email = email;
            FullName = fullname;
            Email = email;
            Created_At = created;
            Updated_At = updated;
        }
        public static User Create(string name, string email, string password)
        {

            return new User
            {
                UserId = Guid.NewGuid(),
                Name = name,
                Email = email,
                Password = password,
                Role = Role.Customer,   
                Status = Status.Active,
                Avatar = string.Empty,
                Created_At = DateTime.UtcNow,
                Updated_At = DateTime.UtcNow,
            };
        }
        public void Band()
        {
            if(Status == Status.Banned) 
                throw new DomainException("User already banned");
            
            Status = Status.Banned;
            Updated_At = DateTime.UtcNow;
        }


        public void UnBan()
        {
            if(Status == Status.Active)
            {
                throw new DomainException("User already active");
            }

            Status = Status.Active;
            Updated_At = DateTime.UtcNow;
        }

        public bool CanLogin()
         => Status == Status.Active;

    }
}
