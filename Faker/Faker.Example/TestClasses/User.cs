namespace Faker.Example.TestClasses;

public class User
{
    public string Name { get; set; }
    
    public int Age { get; set; }

    public Phone Phone { get; }

    public User(Phone phone)
    {
        this.Phone = phone;
    }
}