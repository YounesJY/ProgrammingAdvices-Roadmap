// Lesson #38 - Separate Classes In Libs.cpp : This file contains the 'main' function. Program execution begins and ends there.
#include <iostream>
#include "../../HeaderFiles/Employee.h";

using namespace std;

int main() {
	Employee employee(10, "Mohammed", "Abu-Hadhoud", "A@a.com", "8298982", "CEO", "ProgrammingAdvices", 5000);

	employee.print();

	system("pause>0");
	return 0;
}