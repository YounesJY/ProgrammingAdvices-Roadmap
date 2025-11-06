// Lesson #36 InnerStruct.cpp : This file contains the 'main' function. Program execution begins and ends there.
//ProgrammingAdivces.com
//Mohammed Abu-Hadhoud

#include<iostream>
using namespace std;

class Person {
private:
    struct Address
    {
        string addressLine1;
        string addressLine2;
        string city;
        string country;
    };

public:
    string fullName;
    Address address;

    Person()
    {
        fullName = "Mohammed Abu-Hadhoud";
        address.addressLine1 = "Building 10";
        address.addressLine2 = "Queen Rania Street";
        address.city = "Amman";
        address.country = "Jordan";
    }

    void printAddress()
    {
        cout << "\nAddress:\n";
        cout << address.addressLine1 << endl;
        cout << address.addressLine2 << endl;
        cout << address.city << endl;
        cout << address.country << endl;
    }

};

int main()
{
    Person person;
    person.printAddress();

    system("pause>0");
    
    return 0;
}