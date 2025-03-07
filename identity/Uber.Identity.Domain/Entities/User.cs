using BCrypt.Net;
using Uber.Common.Entities;

namespace Uber.Identity.Domain.Entities
{
    public class User : TrackableEntity<Guid>
    {
        public string Name { get; private set; }
        public string Email { get; private set; }
        public string PasswordHash { get; private set; }
        public string Role { get; private set; }
        public DateTime CreatedAt { get; private set; }

        private User() { }

        public User(string name, string email, string password, string role)
        {
            Id = Guid.NewGuid();
            Name = name;
            Email = email;
            PasswordHash = HashPassword(password);
            Role = role;
            CreatedAt = DateTime.UtcNow;
        }

        private string HashPassword(string password) => BCrypt.Net.BCrypt.HashPassword(password);
    }
}

