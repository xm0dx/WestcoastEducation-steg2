namespace Client.Models;

// en studerande har samma uppgifter som en person, inget extra just nu
public class Student : Person
{
}

// samma som Student men med restdb:s eget _id.
// _id behövs i adressen när vi ska ta bort eller uppdatera
public class StudentRestDb : Student
{
    public string _id { get; set; } = string.Empty;
}
