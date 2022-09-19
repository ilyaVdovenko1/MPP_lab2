namespace Faker.Example.TestClasses;

public class CustomUser
{
    public int Age { get; set; }
    
    public int Id { get; }

    public CustomUser(int id)
    {
        this.Id = id;
    }
}