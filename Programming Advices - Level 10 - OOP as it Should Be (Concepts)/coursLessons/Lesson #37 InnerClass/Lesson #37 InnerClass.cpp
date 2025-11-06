// Lesson #37 InnerClass.cpp : This file contains the 'main' function. Program execution begins and ends there.
//ProgrammingAdivces.com
//Mohammed Abu-Hadhoud

#include<iostream>
using namespace std;

class Person {
private:
	class Address {
	public:
		string addressLine1;
		string addressLine2;
		string city;
		string country;

		void print() {
			cout << "\nAddress:\n";
			cout << addressLine1 << endl;
			cout << addressLine2 << endl;
			cout << city << endl;
			cout << country << endl;
		}
	};

public:
	string fullName;
	Address address;

	Person() {
		fullName = "Mohammed Abu-Hadhoud";
		address.addressLine1 = "Building 10";
		address.addressLine2 = "Queen Rania Street";
		address.city = "Amman";
		address.country = "Jordan";
	}
};

int main() {
	Person person;

	person.address.print();

	system("pause>0");
	return 0;
}