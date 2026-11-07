using System;

namespace Client.Models;

public record TeacherRestDb : Teacher
{
    public string _id { get; set; } = string.Empty;
}
