using System;

namespace Client.Models;

// det som alla personer har gemensamt, samma som i inlämning 1.
public record Person
{
    public string Id { get; set; } = Guid.NewGuid().ToString();
    public required string FirstName { get; set; }
    public required string LastName { get; set; }
    public required string PersonalNumber { get; set; }
    public string? Phone { get; set; }
    public string? Address { get; set; }
    public string? PostalCode { get; set; }
    public string? City { get; set; }

    public string FullName
    {
        get { return FirstName + " " + LastName; }
    }
}
