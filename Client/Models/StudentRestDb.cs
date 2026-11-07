using System;

namespace Client.Models;

public record StudentRestDb : Student
{
    public string _id { get; set; } = string.Empty;
}
