// Lesson #39 - This pointer.cpp : This file contains the 'main' function. Program execution begins and ends there.
//ProgrammingAdvices.com
//Mohammed Abu-Hadhoud

#include <iostream>  
using namespace std;

class Employee {
public:
	int ID;
	string name;
	float salary;

	Employee(int ID, string name, float salary) {
		this->ID = ID;
		this->name = name;
		this->salary = salary;
	}

	static void function1(Employee employee) {
		employee.print();
	}

	void function2() {
		function1(*this);
	}

	void print() {
		cout << ID << "  " << name << "  " << salary << endl;
		// cout << this->ID << "  " << this->name << "  " << this->salary << endl;
	}
};

int main(void) {
	Employee employee(101, "Ali", 5000);

	employee.print();
	employee.function2();

	return 0;
}
