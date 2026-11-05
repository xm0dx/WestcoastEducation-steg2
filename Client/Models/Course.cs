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

    // texter som listan visar, så vyn inte behöver räkna ut något själv
    public string Form
    {
        get
        {
            if (IsOnline)
            {
                return "distans";
            }
            else
            {
                return "klassrum";
            }
        }
    }

    public string DurationText
    {
        get { return $"{Duration} {DurationUnit}"; }
    }

    // yyyy-MM-dd så att datumet ser likadant ut på alla datorer
    public string StartDateText
    {
        get { return $"{StartDate:yyyy-MM-dd}"; }
    }

    public string EndDateText
    {
        get { return $"{EndDate:yyyy-MM-dd}"; }
    }
}

// samma som Course men med restdb:s eget _id
public class CourseRestDb : Course
{
    public string _id { get; set; } = string.Empty;
}
