namespace Client.Models;

// det som alla personer har gemensamt, samma som i inlämning 1.
// skrivs en gång här, Student och Teacher ärver det
public class Person
{
    public required string FirstName { get; set; }
    public required string LastName { get; set; }
    public string Phone { get; set; } = string.Empty;
    public required string PersonalNumber { get; set; }
    public string Address { get; set; } = string.Empty;
    public string PostalCode { get; set; } = string.Empty;
    public string City { get; set; } = string.Empty;
}
