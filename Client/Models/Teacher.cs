using System;

namespace Client.Models;

public record Teacher : Person
{
    public string? KnowledgeArea { get; set; }
}
