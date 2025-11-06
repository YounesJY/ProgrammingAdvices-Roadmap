// Lesson #30 - Virtual Functions.cpp : This file contains the 'main' function. Program execution begins and ends there.
//

#include <iostream>

using namespace std;

class Person
{

public:

    virtual  void Print()

    {
        cout << "Hi, i'm a person!\n ";

    }

    virtual void any() {}

};

class Employee : public Person
{
public:
    void Print()
    {
        cout << "Hi, I'm an Employee\n";
    }
};

class Student : public Person
{
public:
    void Print()
    {
        cout << "Hi, I'm a student\n";
    }
};


int main()

{

    Employee employee1;
    Student  student1;

    employee1.Print();
    student1.Print();



    Person* person1 = &employee1;
    Person* person2 = &student1;

    person1->Print();
    person2->Print();

    person1 = NULL;
    person2 = NULL;


    Person* persons[] = { &employee1, &student1 };

    persons[0]->Print();
    persons[1]->Print();

    persons[0] = NULL;
    persons[1] = NULL;
    
    system("pause>0");
    return 0;
}