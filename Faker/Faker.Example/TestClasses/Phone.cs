namespace Faker.Example.TestClasses;

public class Phone
{
    public string PhoneNumber { get; }
    
    
    public Phone(string code, string number)
    {
        PhoneNumber = $"{code}{number}";
    }
}