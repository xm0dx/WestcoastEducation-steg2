using System;

namespace Client.Models;

public record Course
{
    public string Id { get; set; } = Guid.NewGuid().ToString();
    public required string CourseNumber { get; set; }
    public required string Title { get; set; }
    public int Duration { get; set; }
    public string DurationUnit { get; set; } = "veckor";
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
    public bool IsOnline { get; set; }
    public string? TeacherName { get; set; }

    public string Form
    {
        get
        {
            if (IsOnline)
            {
                return "distans";
            }
            return "klassrum";
        }
    }

    public string DurationText
    {
        get { return Duration + " " + DurationUnit; }
    }

    public string StartDateText
    {
        get { return StartDate.ToString("yyyy-MM-dd"); }
    }

    public string EndDateText
    {
        get { return EndDate.ToString("yyyy-MM-dd"); }
    }
}
