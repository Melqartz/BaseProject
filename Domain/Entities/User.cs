using Domain.Entities.Base;

namespace Domain.Entities;

public class User(string name, string email, string password, string role, bool active)
    : BaseEntity {
    public string Name { get; set; } = name;
    public string Email { get; set; } = email;
    public string Password { get; set; } = password;
    public string Role { get; set; } = role;
    public bool Active { get; set; } = active;
}