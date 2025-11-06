using System;

class Person
{
    public virtual void Print()
    {
        Console.WriteLine("Hi, i'm a person!\n ");
    }
    public static void any() { }
};

class Employee : Person
{
    public override void Print()
    {
        Console.WriteLine("Hi, I'm an Employee\n");
    }
    public static void any()
    {
        Console.WriteLine(" any ovveriden ?");
    }
};

class Student : Person
{
    public override void Print()
    {
        Console.WriteLine("Hi, I'm a student\n");
    }
    public override static void any()
    {
        Console.WriteLine(" any ovveriden ?");
    }
};


namespace Virtual_Functions
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Employee employee = new Employee();
            Student student = new Student();

            employee.Print();
            student.Print();


            Person person1 = employee;
            Person person2 = student;

            person1.Print();
            person2.Print();

            person1 = null;
            person2 = null;


            Person[] persons = { employee, student };

            foreach (Person person in persons)
            {
                person.Print();
                person.any();
            }

            persons = null;
        }
    }
}
