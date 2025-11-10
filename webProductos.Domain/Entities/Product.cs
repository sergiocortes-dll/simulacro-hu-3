using System.ComponentModel.DataAnnotations;
using webProductos.Domain.Common;

namespace webProductos.Domain.Entities;

public class Product
{
    public int Id { get; private set; }

    // EF needs to map this. non-nullable with initializer to satisfy compiler.
    public string Name { get; private set; } = string.Empty;

    private Product() { } // para EF

    public Product(string name)
    {
        SetName(name);
    }

    public void SetName(string name)
    {
        if (string.IsNullOrWhiteSpace(name))
            throw new DomainException("Name required");

        Name = name.Trim();
    }

    public void UpdatePartial(string? name)
    {
        if (name is null) return;
        if (name != Name) SetName(name);
    }
}