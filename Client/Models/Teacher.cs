namespace Client.Models;

public class Teacher : Person
{
    public string KnowledgeArea { get; set; } = string.Empty;
}

// samma som Teacher men med restdb:s eget _id
public class TeacherRestDb : Teacher
{
    public string _id { get; set; } = string.Empty;
}
