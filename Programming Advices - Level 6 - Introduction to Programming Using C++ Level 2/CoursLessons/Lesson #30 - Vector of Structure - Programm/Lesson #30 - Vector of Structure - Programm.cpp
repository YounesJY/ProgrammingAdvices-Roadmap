// Lesson #30 - Vector of Structure - Programm.cpp : This file contains the 'main' function. Program execution begins and ends there.
//

#include <iostream>
#include <vector>
#include "../MyInputLibrary.h";


using namespace std;
using namespace input;

struct Employee {
	string firstName;
	string lastName;
	float salary;
};

void fillEmployee(Employee& employee) {
	cout << "========== Reading Employee Infos Begins ==========\n";
	cout << "  - First name: ";
	cin >> employee.firstName;
	cout << "  - Last name: ";
	cin >> employee.lastName;
	cout << "  - Salary: ";
	cin >> employee.salary;
	cout << "========== Reading Employee Infos Done ===========\n";
}

void printEmployeeData(Employee& employee) {
	cout << "  ====================================\n";
	cout << "  - Full Name: " << employee.firstName << "\n";
	cout << "  - Last Name: " << employee.lastName << "\n";
	cout << "  - Salary:    " << employee.salary << "\n";
	cout << "  ====================================\n";
}

void readEmployees(vector<Employee>& employees) {
	Employee tempEmployee;

	while (true)
	{
		cout << " => Please enter employee data: \n";
		fillEmployee(tempEmployee);
		employees.push_back(tempEmployee);

		if (getChoice("  - Do you want to add more employees ? \n") == no)
			break;
	}

}

void printEmployees(vector<Employee>& employees) {
	cout << "========== Employees Infos ==========\n";
	for (Employee& employee : employees)
		printEmployeeData(employee);
	cout << "=====================================\n";
	employees.clear();
	cout << " -> Vector Size: " << employees.size() << endl;
	cout << " -> Vector Capacity: " << employees.capacity() << endl;
}

int main()
{/*
	vector<Employee> employees;

	readEmployees(employees);
	printEmployees(employees);*/

	int a = 10;
	int& ref = a;


	cout << a << endl;
	cout << ref << endl;

	cout << &a << endl;

	cout << &ref << endl;



}
