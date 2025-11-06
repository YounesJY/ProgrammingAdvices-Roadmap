#pragma once
#include <iostream>
#include "Person.h";

using namespace std;

class Employee : public Person {
private:
	string _title;
	string _department;
	float _salary;

public:
	Employee(int id, string firstName, string lastName, string email, string phone, string title, string department, float salary)
		: Person(id, firstName, lastName, email, phone) {
		this->_title = title;
		this->_department = department;
		this->_salary = salary;
	}

	// Property Set
	void setTitle(string title) {
		_title = title;
	}

	// Property Get
	string getTitle() {
		return _title;
	}

	// Property Set
	void setDepartment(string department) {
		_department = department;
	}

	// Property Get
	string getDepartment() {
		return _department;
	}

	// Property Set
	void setSalary(float salary) {
		_salary = salary;
	}

	// Property Get
	float getSalary() {
		return _salary;
	}

	void print() {
		cout << "\n-> Info:";
		cout << "\n___________________";
		cout << "\n   ID        : " << getId();
		cout << "\n   FirstName : " << getFirstName();
		cout << "\n   LastName  : " << getLastName();
		cout << "\n   Full Name : " << getFullName();
		cout << "\n   Email     : " << getEmail();
		cout << "\n   Phone     : " << getPhone();
		cout << "\n   Title     : " << _title;
		cout << "\n   Department: " << _department;
		cout << "\n   Salary    : " << _salary;
		cout << "\n___________________\n";
	}
};
