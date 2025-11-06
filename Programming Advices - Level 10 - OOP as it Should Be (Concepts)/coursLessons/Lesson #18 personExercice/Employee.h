#pragma once

#include <iostream>
#include "Person.h";


using namespace std;

class Employee : public Person {
public:
	Employee(short ID, string firstName, string lastName, string email, string phoneNumber)
		: Person::Person(ID, firstName, lastName, email, phoneNumber) {
	}
};


