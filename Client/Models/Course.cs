using System;

namespace Client.Models;

public class Course
{
    public required string CourseNumber { get; set; }
    public required string Title { get; set; }
    public int Duration { get; set; }
    public string DurationUnit { get; set; } = "veckor"; // veckor eller dagar
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
    public bool IsOnline { get; set; } // true = distans, false = klassrum

    // namnet på läraren som har kursen, tom om ingen är vald än
    public string TeacherName { get; set; } = string.Empty;

    // för listan: visar "distans" eller "klassrum" i stället för true/false
    public string Form => IsOnline ? "distans" : "klassrum";
}

// samma som Course men med restdb:s eget _id
public class CourseRestDb : Course
{
    public string _id { get; set; } = string.Empty;
}
