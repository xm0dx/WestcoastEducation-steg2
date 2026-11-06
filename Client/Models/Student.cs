namespace Client.Models;

public class Student : Person
{
}

// samma som Student men med restdb:s eget _id.
public class StudentRestDb : Student
{
    public string _id { get; set; } = string.Empty;
}
