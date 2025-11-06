
using System;

class Person
{

    private short _Id;
     string FirstName { get; set; }
    string LastName { get; set; }
    string Email { get; set; }
    string PhoneNumber { get; set; }

    public Person(short id, string firstName, string lastName, string email, string phoneNumber)
    {
        _Id = id;
        FirstName = firstName;
        LastName = lastName;
        Email = email;
        PhoneNumber = phoneNumber;
    }

    public short Id
    {
        get { return _Id; }
    }
    public string getFullName() { return $"{FirstName} {LastName}"; }

    public void print()
    {
        Console.WriteLine($"-------- Person Details --------");
        Console.WriteLine($"--------------------------------");
        Console.WriteLine($" ID         : {Id}              ");
        Console.WriteLine($" First name : {FirstName}       ");
        Console.WriteLine($" Last name  : {LastName}        ");
        Console.WriteLine($" Full name  : {getFullName()}   ");
        Console.WriteLine($" Email      : {Email}           ");
        Console.WriteLine($" Phone      : {PhoneNumber}     ");
        Console.WriteLine($"--------------------------------");
    }

    protected void sendEmail(string subject, string body)
    {
        Console.WriteLine($" - The following email sent successfuly the to [{Email}]");
        Console.WriteLine($" - [Subject] : [{subject}                               ");
        Console.WriteLine($" - [Body]    : [{body}                                  ");
    }
    protected void sendSMS(string body)
    {
        Console.WriteLine($" - The following email sent successfuly the to [{PhoneNumber}]");
        Console.WriteLine($" - [Subject] : [{body}                                        ");
    }

};

class Employee : Person
{
    public Employee(short id, string firstName, string lastName, string email, string phoneNumber)
    : base(id, firstName, lastName, email, phoneNumber)
    { }
};



namespace personExercice
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Employee employee = new Employee(912, "Jy", "Hydra", "hyadra@hotmail", "12424124124");
            employee.print();

        }
    }
}
