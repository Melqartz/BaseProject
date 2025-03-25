using Domain.Entities.Base;

namespace Domain.Entities;

public class Role(string name, string description) : BaseEntity {
    public string Name { get; set; } = name;
    public string Description { get; set; } = description;
    public bool Active { get; set; } = true;
}