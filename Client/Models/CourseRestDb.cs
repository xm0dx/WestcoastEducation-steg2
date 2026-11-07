using System;

namespace Client.Models;

public record CourseRestDb : Course
{
    public string _id { get; set; } = string.Empty;
}
